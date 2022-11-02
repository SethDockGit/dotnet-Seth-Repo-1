using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rng = new Random();
            Monster mon1 = new Monster(7, 50);
            Monster mon2 = new Monster(11, 42);

            Battle battle = new Battle(mon1, mon2, rng);

            battle.StartBattle();

            Console.ReadKey();
        }
    }
}
