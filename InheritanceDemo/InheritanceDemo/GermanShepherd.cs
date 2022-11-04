﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceDemo
{
    class GermanShepherd : Dog
    {

        public GermanShepherd(bool isAGoodBoy, string name) : base(isAGoodBoy, name) 
        {
            SignatureMove = "goes completely nuts";
        }
        public void DoSignatureMove()
        {
            Console.Write(Name);
            Console.Write(SignatureMove);
            Console.ReadKey();  
        }
    }
}
