using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using DragonWarrior.Models;

namespace DragonWarrior.Models.Monsters
{
    public class Anteater : IMonster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHealth { get; set; }
        public int MaxMana { get; set; }
        public int Agil { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Int { get; set; }
        public int ToNextLvl { get; set; }
        public int Experience { get; set; }
        public List<string> Abilities { get; set; }
        public Anteater(string name, int level)
        {
            Name = name;
            Level = level;
            MaxHealth = 22;
            MaxMana = 6;
            Agil = 5;
            Atk = 9;
            Def = 9;
            Int = 4;
            ToNextLvl = 65;
            Experience = 0;
            Abilities = new List<string>();

            GetInitialAbilities();

            ScaleStatsToLvl();

            if(name == "C")
            {
                Name = "Anteater";
            }
        }

        public void GetInitialAbilities()
        {
            throw new NotImplementedException();
        }
        public void GainExperience(int experience)
        {
            Experience += experience;

            Console.Clear();
            Console.WriteLine($"\n\n{Name} gained {experience} experience!!");
            Console.ReadKey();

            if (Experience > ToNextLvl)
            {
                LevelUp();
            }
        }
        public void LevelUp()
        {
            Level++;
            Experience = 0;

            ScaleStatsToLvl();

            CheckForGainedAbility();

            Console.Clear();
            Console.WriteLine($"\n\n{Name} reached level {Level}!!");
            Console.ReadKey();
        }

        public void CheckForGainedAbility()
        {
            //not implemented
        }
        public void ScaleStatsToLvl()
        {
            if (Level > 1)
            {
                int healthScaler = Level * 3;
                int manaScaler = Level * 1;
                int agilScaler = Level * 1;
                int atkScaler = Level * 3;
                int defScaler = Level * 3;
                int intScaler = Level * 1;
                int lvlScaler = Level * 2;

                MaxHealth += healthScaler;
                MaxMana += manaScaler;
                Agil += agilScaler;
                Atk += atkScaler;
                Def += defScaler;
                Int += intScaler;
                ToNextLvl += lvlScaler;
            }
        }
    }
}
