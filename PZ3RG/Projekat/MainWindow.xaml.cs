using Projekat.HelperClasses;
using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NetworkModel nm = new NetworkModel();

        public MainWindow()
        {
            InitializeComponent();
            InitializeNetworkModel();
            InitializeAllEntitiesOnGrid();
        }

        private void InitializeNetworkModel()
        {
            DataIO dio = new DataIO();
            nm = dio.DeSerializeObject<NetworkModel>(@"../../XMLData/Geographic.xml");
        }

        private void FillNodes()
        {
           foreach(var node in nm.Nodes)
            {
                ConverterUTMToDecimal.ToLatLon(node.X, node.Y,Config.UTMZone,out double latitude,out double longitude);
                Rectangle rectangle = new Rectangle();
                rectangle.Height = Config.nodesRectangleHeight;
                rectangle.Width = Config.nodesRectangleWidth;
                rectangle.StrokeThickness = Config.nodesRectanglStrokeThickness;
                rectangle.Fill = Config.nodesRectanglFill;


                DrawAndScale.YXForCanvas(latitude, longitude, out double yForCanvas, out double xForCanvas);
                DrawAndScale.YXForMatrix(latitude, longitude, out int y, out int x);
                DrawAndScale.EntityOnGrid(Config.canvasEnd, yForCanvas, xForCanvas, node.Id);

                Canvas.SetBottom(rectangle, yForCanvas);
                Canvas.SetLeft(rectangle, xForCanvas);
                zeusOfCanvases.Children.Add(rectangle);

                rectangle.ToolTip = string.Format("NODE:{0} | ID:{1}\nX:{2} | Y:{3}", node.Name, node.Id,node.X,node.Y);

                Config.matrix[x, y] += node.Id.ToString() + "_";
            }
        }

        private void FillSwitches()
        {
            foreach(var swit in nm.Switches)
            {
                ConverterUTMToDecimal.ToLatLon(swit.X, swit.Y, Config.UTMZone, out double latitude, out double longitude);
                Rectangle rectangle = new Rectangle();
                rectangle.Height = Config.switchRectangleHeight;
                rectangle.Width = Config.switchRectangleWidth;
                rectangle.StrokeThickness = Config.switchRectanglStrokeThickness;
                rectangle.Fill = Config.switchRectanglFill;


                DrawAndScale.YXForCanvas(latitude, longitude, out double yForCanvas, out double xForCanvas);
                DrawAndScale.YXForMatrix(latitude, longitude, out int y, out int x);
                DrawAndScale.EntityOnGrid(Config.canvasEnd, yForCanvas, xForCanvas, swit.Id);

                Canvas.SetBottom(rectangle, yForCanvas);
                Canvas.SetLeft(rectangle, xForCanvas);
                zeusOfCanvases.Children.Add(rectangle);

                rectangle.ToolTip = string.Format("SWITCH:{0} | ID:{1}\nX:{2} | Y:{3}", swit.Name, swit.Id, swit.X, swit.Y);

                Config.matrix[x, y] += swit.Id.ToString() + "_";
            }
        }

        public void FillSubstations()
        {
            foreach (var substation in nm.Substations)
            {
                ConverterUTMToDecimal.ToLatLon(substation.X, substation.Y, Config.UTMZone, out double latitude, out double longitude);
                Rectangle rectangle = new Rectangle();
                rectangle.Height = Config.substationRectangleHeight;
                rectangle.Width = Config.substationRectangleWidth;
                rectangle.StrokeThickness = Config.substationRectanglStrokeThickness;
                rectangle.Fill = Config.substationRectanglFill;


                DrawAndScale.YXForCanvas(latitude, longitude, out double yForCanvas, out double xForCanvas);
                DrawAndScale.YXForMatrix(latitude, longitude, out int y, out int x);
                DrawAndScale.EntityOnGrid(Config.canvasEnd, yForCanvas, xForCanvas, substation.Id);

                Canvas.SetBottom(rectangle, yForCanvas);
                Canvas.SetLeft(rectangle, xForCanvas);
                zeusOfCanvases.Children.Add(rectangle);

                rectangle.ToolTip = string.Format("SUBSTATION:{0} | ID:{1}\nX:{2} | Y:{3}", substation.Name, substation.Id, substation.X, substation.Y);

                Config.matrix[x, y] += substation.Id.ToString() + "_";
            }
        }

        public void FillLines()
        {
            foreach(var line in nm.Lines)
            {
                GridCoordinates gridCoordinates = new GridCoordinates();
                BreathFirstSearch bfs = new BreathFirstSearch();
                bool isFound = false;

                //ako nema pocetak i kraj nije povezana nisakim nego samo nastavi dalje he he 
                if (!Config.entityDictionary.ContainsKey(line.FirstEnd)||!Config.entityDictionary.ContainsKey(line.SecondEnd))
                {
                    continue;
                }

                for(int i = 0; i < Config.matlen; i++)
                {
                    if (!isFound)
                    {
                        for(int j = 0; j < Config.matlen; j++)
                        {
                            if (Config.matrix[i, j].Contains(line.FirstEnd.ToString()))
                            {
                                gridCoordinates.GridRow = i;
                                gridCoordinates.GridColumn = j;
                                gridCoordinates.Connected = null;
                                isFound = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                string endPoint = line.SecondEnd.ToString();
                gridCoordinates = bfs.BFSAlgorithm(gridCoordinates, Config.matrix, endPoint);


                while (gridCoordinates != null)
                {

                    if (gridCoordinates.Connected == null)
                    {
                        break;
                    }
                    var canvEnd = (int)Config.canvasEnd;

                    double x1 = gridCoordinates.GridRow * Config.squareForScaling;
                    double y1 = canvEnd - (gridCoordinates.GridColumn * Config.squareForScaling);
                    double x2 = gridCoordinates.Connected.GridRow * Config.squareForScaling;
                    double y2 = canvEnd - (gridCoordinates.Connected.GridColumn * Config.squareForScaling);

                    bool lineExists = false;

                    foreach (var lineset in Config.linesSet)
                    {
                        if (lineset.X1 == x1 && lineset.Y1 == y1 && lineset.X2 == x2 && lineset.Y2 == y2)
                        {
                            lineExists = true;
                            break;
                        }

                    }

                    if (!lineExists)
                    {
                        Line l = new Line();
                        l.Stroke = Brushes.Black;
                        l.X1 = x1;
                        l.Y1 = y1;

                        l.X2 = x2;
                        l.Y2 = y2;
                        l.StrokeThickness = 2;

                        Config.linesSet.Add(l);
                        zeusOfCanvases.Children.Add(l);

                    }
                    gridCoordinates = gridCoordinates.Connected;
                }

            }
        }

        public void InitializeAllEntitiesOnGrid()
        {
            FillNodes();
            FillSwitches();
            FillSubstations();
        }
    }
}
