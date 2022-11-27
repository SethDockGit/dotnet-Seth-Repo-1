using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game_Of_Life2.Data
{
    public class InMemory : IData
    {
        public List<Map> Maps { get; set; }

        public InMemory() 
        {
            Maps = new List<Map>
            {
                new Map()
                {
                    Name = "StillLife",
                    ID = "1",
                    Cells = StillLife(),
                },
                new Map()
                {
                    Name = "Gosper",
                    ID = "2",
                    Seeds = GetGosperGliderGun(),
                },
                new Map()
                {
                    Name = "Generic",
                    ID = "3",
                    Seeds = GetGeneric(),
                }
            };
        }
        public Map GetMap(string id)
        {
            Map map = Maps.Single(m => m.ID == id);
            return map;
        }
        public void ChangeMap()
        {

        }
        public void DeleteMap()
        {

        }
        public void CreateMap(string id)
        {
            throw new NotImplementedException();
        }
        public void SaveMap(string[] design)
        {
            throw new NotImplementedException();
        }
        public List<Map> ReadMaps()
        {
            return Maps;
        }
        private static bool[,] StillLife()
        {
            bool[,] cells = new bool[10, 10];


            cells[3, 7] = true;
            cells[3, 8] = true;
            cells[3, 9] = true;
            cells[6, 7] = true;
            cells[6, 8] = true;
            cells[6, 9] = true;

            return cells;
        }
        private static string[] GetGosperGliderGun()
        {

            string[] gosper = new string[]
            {
                "........................O",
                "......................O.O",
                "............OO......OO............OO",
                "...........O...O....OO............OO",
                "OO........O.....O...OO",
                "OO........O...O.OO....O.O",
                "..........O.....O.......O",
                "...........O...O",
                "............OO"
            };


            return gosper;
        }
        private static string[] GetGeneric()
        {
            string[] seeds =
            {
                "*******OOO",
                "*******OOO",
                "*******OOO",
                "*******OOO",
                "*******OOO",
            };

            return seeds;
        }

    }
}
