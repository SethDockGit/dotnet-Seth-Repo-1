using System;
using System.Collections.Generic;
using System.Text;

namespace Game_Of_Life2.Data
{
    public interface IData
    {
        public List<Map> Maps { get; set; }

        public Map GetMap(string id);

        public void ChangeMap();

        public void DeleteMap();

        public void CreateMap(string id);

        public void SaveMap(string[] design);
        public List<Map> ReadMaps();
    }
}
