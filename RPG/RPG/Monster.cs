using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPG
{
    //    -Make a Monster class with properties: name, health, maxhealth, attack
    //-Make a constructor which assigns values to the property via the constructor's parameters (or methods within the class)
    //-Make a battle class which contains two Monsters as properties
    //-In the battle class, create a method for commencing a battle, where each monster takes turns attacking the other
    //-Once a monster has 0 health, end the battle

    //** avoid any complex logic/processing steps in main method; they should be contained within your classes and the methods therein
    //** when possible, keep as many classes, methods, and properties private, favoring methods which get or set data as needed
    public class Monster
    {
        private string Name;
        private int Attack;
        private int MaxHealth;
        private int CurrentHealth;

        public Monster (int attack, int maxHealth)
        {

            Attack = attack;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;

            SetName();      
        }
        public int GetAttack(Random rng)
        {
            int randomInt = rng.Next(-3, 3);
            int attack = Attack + randomInt;

            return attack;
        }
        public void TakeDamage(int attack)
        {            
            if (attack > 0) 
            {
                CurrentHealth -= attack;
            }
        }
        public string GetName()
        {
            return Name;
        }
        private void SetName()
        {
            Console.Clear();
            Console.WriteLine("Found a new mon! Would you like to give a nickname to your pokemon?");
            string answer = Console.ReadLine();
            if (answer == "yes")
            {
                Console.Clear();
                Console.WriteLine("Cool, you may name your mon");
                Name = Console.ReadLine();
            }
            else
            {
                Console.Clear();
                string defaultName = "brody";
                Name = defaultName;
            }
        }
        public int GetHealth()
        {
            return CurrentHealth;
        }
    }
}
