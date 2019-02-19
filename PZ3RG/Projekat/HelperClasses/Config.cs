using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Projekat.HelperClasses
{
    public class Config
    {
        public struct direction // smer*
        {
            public int x;
            public int y;
        };

        public static direction[] directions = {
                                       new direction(){x = 1, y = 0}, // desno*
                                       new direction(){x = 0, y = 1}, // gore*
                                       new direction(){x = -1, y = 0}, // levo*
                                       new direction(){x = 0, y = -1} // dole*
        };

        public static string[,] matrix;
        public static int[,] visitedMap;
        //public static Dictionary<int, int> directions;
        public static Dictionary<ulong, Point> entityDictionary;
        public static HashSet<Line> linesSet;
        public static double rectangleDiametet = 5.0;

        //za rectangle parametri
        public static int nodesRectangleHeight = 10;
        public static int nodesRectangleWidth = 10;
        public static double nodesRectanglStrokeThickness = 2.0;
        public static Brush nodesRectanglFill = new SolidColorBrush(Colors.DarkBlue);

        //za rectangle za switch parametri
        public static int switchRectangleHeight = 10;
        public static int switchRectangleWidth = 10;
        public static double switchRectanglStrokeThickness = 2.0;
        public static Brush switchRectanglFill = new SolidColorBrush(Colors.Firebrick);

        //za rectangle za substation parametri
        public static int substationRectangleHeight = 10;
        public static int substationRectangleWidth = 10;
        public static double substationRectanglStrokeThickness = 2.0;
        public static Brush substationRectanglFill = new SolidColorBrush(Colors.Olive);

        //valjda su ove vr. lukina dojava
        public static double minimumNodeLatitute = 45.189725628196662;
        public static double minimumNodeLongitude = 19.727275715138386;

        public static double maximumNodeLatitute = 45.328735805580521;
        public static double maximumNodeLongitude = 19.950944330565722;

        //za skaliranja su ovih sledecih 5 vrednosti
        //velicina matrice ustvari
        public static double matrixStart = 0;
        public static double matrixLength = 150;

        //velicina kanvasa ustvari
        public static double canvasStart = 0;
        public static double canvasEnd = 3000;

        public static int matlen = 151;

        public static int squareForScaling = 20;

        //utm zona,za srbiju je 34
        public static int UTMZone = 34;

        static Config()
        {
            matrix = new string[matlen, matlen];
            visitedMap = new int[matlen, matlen];
            InitializeMatrix();
            linesSet = new HashSet<Line>();
            entityDictionary = new Dictionary<ulong, Point>();
            //directions = new Dictionary<int, int>();
            //directions.Add(1, 0);
            //directions.Add(0, 1);
            //directions.Add(-1, 0);
            //directions.Add(0, -1);
        }

        public static void InitializeMatrix()
        {
            for (int i = 0; i < matlen; i++)
            {
                for (int j = 0; j < matlen; j++)
                {
                    matrix[i, j] = string.Empty;
                }
            }
        }

        public static int[,] ResetVisitedMap()
        {
            int[,] returnArray = new int[matlen, matlen];

            for (int i = 0; i < matlen; i++)
            {
                for (int k = 0; k < matlen; k++)
                {
                    returnArray[i, k] = 0;
                }
            }

            return returnArray;
        }
    }
}
