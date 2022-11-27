using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game_Of_Life2.Data;

namespace Game_Of_Life2
{
    public class Manager
    {
        private IData DataSource;
        public Manager(IData dataSource)
        {
            DataSource = dataSource;
        }
        private static bool[,] ConvertSeed(int length, int height, string[] seed)
        {
            bool[,] newState = new bool[length, height];

            for (int y = 0; y < seed.Length; y++)
            {
                for (int x = 0; x < seed[y].Length; x++)
                {
                    if (seed[y][x] == 'O')
                    {
                        newState[x, y] = true;
                    }
                    else
                    {
                        newState[x, y] = false;
                    }
                }
            }
            return newState;
        }
        public Map GetMap(string id) 
        {
            Console.Clear();

            Map map = DataSource.Maps.Single(m => m.ID == id);

            if (map.Cells == null)
            {
                map.Cells = ConvertSeed(map.Length, map.Height, map.Seeds);
            }

            return map;
        }
    }
}
