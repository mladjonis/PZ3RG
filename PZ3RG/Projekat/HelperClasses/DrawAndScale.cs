using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat.HelperClasses
{
    public class DrawAndScale
    {

        public static double ConvertRange(double minimalLatitudeStart, double maximalLatitudeEnd, double canvasStart, double canvasLenght, double valueLatitudeLongitude)
        {
            double scale = (canvasLenght - canvasStart) / (maximalLatitudeEnd - minimalLatitudeStart);
            return (Math.Round((canvasStart + ((valueLatitudeLongitude - minimalLatitudeStart) * scale)) / Config.squareForScaling)) * Config.squareForScaling;
        }


        //odredjivanje centra pravougaonika i dodavanje u dictionary za entitete(nodovi u ovom slucaju predstavljaju sve entitete sem linija)
        public static void EntityOnGrid(double canvasEnd , double convertedBottomRange, double convertedLeftRange, ulong id)
        {
            Point point = new Point();
            point.Y = canvasEnd - convertedBottomRange - Config.rectangleDiametet;
            point.X = convertedLeftRange + Config.rectangleDiametet;
            Config.entityDictionary.Add(id, point);
        }

        //rang za celu matricu
        public static int ConvertRangeToMatrix(double minimalLatitudeStart, double maximalLatitudeEnd, double canvasStart, double canvasLenght, double valueLatitudeLongitude)
        {
            double scale = (canvasLenght - canvasStart) / (maximalLatitudeEnd - minimalLatitudeStart);
            return (int)(Math.Round((canvasStart + ((valueLatitudeLongitude - minimalLatitudeStart) * scale))));
        }

        public static void YXForCanvas(double latitude,double longitude,out double yForCanvas,out double xForCanvas)
        {
            yForCanvas = DrawAndScale.ConvertRange(Config.minimumNodeLatitute, Config.maximumNodeLatitute, Config.canvasStart, Config.canvasEnd, latitude) - Config.rectangleDiametet;
            xForCanvas  = DrawAndScale.ConvertRange(Config.minimumNodeLongitude, Config.maximumNodeLongitude, Config.canvasStart, Config.canvasEnd, longitude) - Config.rectangleDiametet;
        }

        public static void YXForMatrix(double latitude,double longitude,out int y,out int x)
        {
            y = DrawAndScale.ConvertRangeToMatrix(Config.minimumNodeLatitute, Config.maximumNodeLatitute, Config.canvasStart, Config.matrixLength, latitude);
            x = DrawAndScale.ConvertRangeToMatrix(Config.minimumNodeLongitude, Config.maximumNodeLongitude, Config.canvasStart, Config.matrixLength, longitude);
        }
    }
}
