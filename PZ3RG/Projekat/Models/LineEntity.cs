using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Models
{
    public class LineEntity
    {

        public LineEntity() { }

        public ulong Id { get; set; }
        public string Name { get; set; }
        public bool IsUnderground { get; set; }
        public double R { get; set; }
        public string ConductorMaterial { get; set; }
        public string LineType { get; set; }
        public double ThermalConstantHeat { get; set; }
        public ulong FirstEnd { get; set; }
        public ulong SecondEnd { get; set; }
        public List<Point> Vertices { get; set; }

        public override string ToString()
        {
            return string.Format("ID:{0}\t Name:{1}\t ISU:{2}\t R:{3}\t CM:{4}\t LT:{5}\t \n" +
                "TC:{6}\t FE:{7}\t SE:{8}\t Vertices:{9}", Id, Name, IsUnderground, R, ConductorMaterial, LineType, ThermalConstantHeat
                , FirstEnd, SecondEnd, Lis(Vertices));
        }

        private string Lis(List<Point> p)
        {
            var str = string.Empty;
            foreach (var i in p)
            {
                str = string.Format("X:{0}|Y:{1}", i.X, i.Y);
            }
            return str;
        }
    }
}
