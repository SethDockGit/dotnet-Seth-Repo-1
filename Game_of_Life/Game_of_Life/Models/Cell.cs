using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_of_Life.Models
{
    public class Cell
    {

        public bool IsAlive;
        public bool IsNeighbor;
        public int X;
        public int Y;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }





    }
}
