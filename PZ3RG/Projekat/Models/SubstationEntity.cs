using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Models
{
    public class SubstationEntity
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public SubstationEntity() { }

        public override string ToString()
        {
            return string.Format("ID:{0}\t  Name:{1}\t  X:{2}\t  Y:{3}\n", Id, Name, X, Y);
        }
    }
}
