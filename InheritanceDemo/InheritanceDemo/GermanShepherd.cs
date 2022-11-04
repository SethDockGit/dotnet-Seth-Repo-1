using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceDemo
{
    class GermanShepherd : Dog
    {
        public string Message { get; set; } = "I'm a goblin!";

        public GermanShepherd(bool isAGoodBoy, string name) : base(isAGoodBoy, name) 
        {

        }
    }
}
