using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.HelperClasses
{
    public interface IBFS
    {
        GridCoordinates BFSAlgorithm(GridCoordinates current, string[,] map, string toLook);

        int[,] visitedMap();

        bool visited(int row, int column);

        GridCoordinates[] neighbours(GridCoordinates gridCoordinate);

        GridCoordinates[] neighbours(GridCoordinates[] gridCoordinates);

    }
}
