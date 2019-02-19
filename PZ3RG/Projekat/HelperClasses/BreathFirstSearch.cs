using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekat.Models;

namespace Projekat.HelperClasses
{
    public class BreathFirstSearch : IBFS
    {
        public GridCoordinates BFSAlgorithm(GridCoordinates current, string[,] map, string toLook)
        {
            GridCoordinates[] gridCoordinates = new GridCoordinates[] { current };
            bool isFound = false;

            while (!isFound)
            {
                foreach(GridCoordinates gridCoordinate in gridCoordinates)
                {
                    //id-jevi svih objekata koji se nalaze na tom cvoru odnosno na toj poziciji u matrici
                    string[] nodeIDs = map[gridCoordinate.GridRow, gridCoordinate.GridColumn].Split('_');  //pozicija u matrici
                    foreach (var id in nodeIDs)
                    {
                        if (id.Equals(toLook))  //ako je pronadjen nasa krajnji cvor
                        {
                            //ispraviti na casu da budu clanovi ove klase a ne u static
                            Config.visitedMap = Config.ResetVisitedMap();
                            return gridCoordinate;
                        }
                    }
                }
                gridCoordinates = neighbours(gridCoordinates);
            }
            return current;
        }

        public GridCoordinates[] neighbours(GridCoordinates gridCoordinate)
        {
            List<GridCoordinates> gridCoordinates = new List<GridCoordinates>();
            foreach(var i in Config.directions)
            {
                //int row = gridCoordinate.GridRow + i.Key;
                //int col = gridCoordinate.GridColumn + i.Value;
                int row = gridCoordinate.GridRow + i.x;
                int col = gridCoordinate.GridColumn + i.y;
                if (row >= 0 && col >= 0 && row < Config.matrix.GetLength(0) && col < Config.matrix.GetLength(1) && !visited(row, col))
                {
                    Config.visitedMap[row, col] = 1;
                    GridCoordinates grid = new GridCoordinates(
                        row, col, gridCoordinate);
                    gridCoordinates.Add(grid);

                }
            }
            return gridCoordinates.ToArray();
        }

        public GridCoordinates[] neighbours(GridCoordinates[] gridCoordinates)
        {
            List<GridCoordinates> gridCoordinate = new List<GridCoordinates>();
            foreach(var i in gridCoordinates)
            {
                gridCoordinate.AddRange(neighbours(i));
            }
            return gridCoordinate.ToArray();
        }

        public bool visited(int row, int column)
        {
            var map = visitedMap();
            return map[row, column] == 1;
        }

        public int[,] visitedMap()
        {
            return Config.visitedMap;
        }
    }
}
