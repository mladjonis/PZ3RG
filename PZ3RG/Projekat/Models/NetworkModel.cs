using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Models
{
    public class NetworkModel
    {
        public List<SubstationEntity> Substations { get; set; }
        public List<NodeEntity> Nodes { get; set; }
        public List<SwitchEntity> Switches { get; set; }
        public List<LineEntity> Lines { get; set; }

        public NetworkModel()
        {
            Substations = new List<SubstationEntity>();
            Nodes = new List<NodeEntity>();
            Switches = new List<SwitchEntity>();
            Lines = new List<LineEntity>();
        }
    }
}
