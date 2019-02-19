using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Models
{
    public class SwitchEntity
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public SwitchEntity() { }
    }
}
