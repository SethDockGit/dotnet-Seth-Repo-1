using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Game_of_Life.Models
{ 
    public class Map
    {
        public List<Cell> Cells { get; set; }

        public List<Cell> TestMap()
        {
            return new List<Cell>()
            {
                new Cell(1, 1),
                new Cell(2, 2),
                new Cell(3, 3),
                new Cell(4, 4),
                new Cell(2, 1),
                new Cell(3, 2),
                new Cell(4, 3),
                new Cell(5, 4),
                new Cell(6, 4),
                new Cell(6, 5),
                new Cell(6, 3),
                new Cell(7, 2),

            };
        } 
    }
}

