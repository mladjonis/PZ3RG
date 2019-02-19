using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Models
{
    public class GridCoordinates
    {
        private int gridRow;
        private int gridColumn;
        private GridCoordinates connected; 

        public int GridRow { get => gridRow; set => gridRow = value; }
        public int GridColumn { get => gridColumn; set => gridColumn = value; }
        public GridCoordinates Connected { get => connected; set => connected = value; }

        public GridCoordinates() { }

        public GridCoordinates(int gr,int gc,GridCoordinates c)
        {
            this.gridRow = gr;
            this.gridColumn = gc;
            this.connected = c;
        }
    }
}
