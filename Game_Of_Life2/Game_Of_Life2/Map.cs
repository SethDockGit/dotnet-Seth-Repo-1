using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Of_Life2
{
    public class Map
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string[] Seeds { get; set; }
        public bool[,] Cells { get; set; }
        public int Length = 50;
        public int Height = 50;
        public int ChanceToLive = 15; 


        private static bool[,] Random(int length, int height, int percentChanceToLive)
        {
            Random rng = new Random();

            bool[,] cells = new bool[length, height];

            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    int randomInt = rng.Next(0, 100);

                    if (randomInt > percentChanceToLive - 1)
                    {
                        cells[x, y] = false;
                    }
                    else
                    {
                        cells[x, y] = true;
                    }
                }
            }
            return cells;
        }
    }
}
