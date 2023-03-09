using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Game_Of_Life2.Data
{
    public class TxtData : IData
    {
        public List<Map> Maps { get; set; }

        private string SaveLocation { get; set; }
        private string SaveFile { get; set; }

        public TxtData()
        {
            SaveLocation = @"C:\Data\GameOfLife\";
            Maps = new List<Map>();
        }

        public void CreateMap(string name)
        {
            File.Create(SaveLocation + name + ".txt").Close();
            
            SaveFile = SaveLocation + name + ".txt";
        }
        public void SaveMap(string[] design)
        {
            using (StreamWriter sw = File.AppendText(SaveFile))
            {
                for(int i = 0; i < design.Length; i++)
                {
                    sw.Write(design[i]);
                    sw.Write("\n");
                }
            }
        }
        public void ChangeMap()
        {
            throw new NotImplementedException();
        }
        public void DeleteMap()
        {
            throw new NotImplementedException();
        }
        //public List<Map> ReadMaps()
        //{
        //    foreach (File file in (SaveLocation)) //idk how to actually do this
        //    {
        //        Map map = new Map();
        //        using (StreamReader sr = File.OpenText(SaveFile))
        //        {
        //            string line = "";
        //            string[] seeds = new string[];

        //            while((line = sr.ReadLine()) != null)
        //            {
        //                string[] seeds = line;
        //            }

        //        map.Seeds = //title of file
        //        map.ID = //new ID for each title
        //        }
        //        Maps.Add(map);
        //    }
        //    return Maps;
        //}
        public Map GetMap(string id)
        {
            throw new NotImplementedException();
        }
    }
}
