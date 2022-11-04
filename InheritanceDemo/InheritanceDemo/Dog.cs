using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceDemo
{
    class Dog
    {
        public bool IsAGoodBoy { get; set; }
        public string Name { get; set; }
        public string SignatureMove { get; set; }

        public Dog(bool isGood, string name)
        {
            IsAGoodBoy = isGood;
            Name = name;
        }


    }
}
