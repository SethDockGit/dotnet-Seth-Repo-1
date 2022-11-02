using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Battle
    {
        private Monster Monster1;
        private Monster Monster2;
        private Random Rng;

        public Battle (Monster mon1, Monster mon2, Random rng)
        {
            Monster1 = mon1;
            Monster2 = mon2;
            Rng = rng; 
        }
        public void StartBattle()
        {
            string mon1 = Monster1.GetName();
            string mon2 = Monster2.GetName();

            int mon1Health = Monster1.GetHealth();
            int mon2Health = Monster2.GetHealth();

            Console.Clear();
            Console.WriteLine($"OK yall, the beatdown is about to commence. In corner 1 we have {mon1}, and in corner 2 we have {mon2}. Fight!!");
            Console.WriteLine();
            Console.WriteLine($"{mon1}: {mon1Health} --------------- {mon2}: {mon2Health}");
            Console.ReadKey();

            bool isItOver = false;
            bool isItP1Turn = true;


            while (isItOver != true)
            {
                if (isItP1Turn)
                {
                    Console.Clear();
                    int attackNum = Monster1.GetAttack(Rng);
                    Monster2.TakeDamage(attackNum);
                    Console.Write($"{mon1} attacks!! {mon2} took {attackNum} damage from {mon1}!!");
                    Console.WriteLine();
                    mon1Health = Monster1.GetHealth();
                    mon2Health = Monster2.GetHealth();
                    Console.Write($"{mon1}: {mon1Health} --------------- {mon2}: {mon2Health}");

                    Console.WriteLine();
                    isItOver = EndBattle();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();

                    Console.Clear();

                    isItP1Turn = !isItP1Turn;
                }
                else
                {
                    Console.Clear();
                    int attackNum = Monster2.GetAttack(Rng);
                    Monster1.TakeDamage(attackNum);
                    Console.Write($"{mon2} attacks!! {mon1} took {attackNum} damage from {mon2}!!");
                    Console.WriteLine();
                    mon1Health = Monster1.GetHealth();
                    mon2Health = Monster2.GetHealth();

                    Console.Write($"{mon1}: {mon1Health} --------------- {mon2}: {mon2Health}");
                    Console.WriteLine();
                    isItOver = EndBattle();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();

                    Console.Clear();
                    
                    isItP1Turn = !isItP1Turn;
                }

            }

        }
        public bool EndBattle()
        {
            bool isItOver = false;
            int mon1Health = Monster1.GetHealth();
            int mon2Health = Monster2.GetHealth();
            string mon1 = Monster1.GetName();
            string mon2 = Monster2.GetName();   
            if (mon1Health <= 0)
            {
                isItOver = true;
                Console.WriteLine($"{mon1} has been defeated!!");            
                return isItOver;
            }
            else if (mon2Health <= 0)
            { 
                isItOver = true;
                Console.WriteLine($"{mon2} has been defeated!!");
                return isItOver;              
            }
            else
            {
                isItOver = false;
                return isItOver;
            }
            
        }



    }
}
