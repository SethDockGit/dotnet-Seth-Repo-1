using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceDemo
{
    class Terrier : Dog
    {
        public Terrier(bool isGood, string name) : base(isGood, name)
        {
            SignatureMove = "humps a stuffed animal";
        }
        public void DoSignatureMove()
        {
            Console.Write(Name);
            Console.Write(SignatureMove);
            Console.ReadKey();
        }
    }
}
