using System;
using System.Collections.Generic;
using System.Text;

namespace DragonWarrior.Models.Monsters
{
    public class Slime : IMonster
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

        public Slime(string name, int level)
        {
            Name = name;
            Level = level;
            MaxHealth = 18;
            MaxMana = 12;
            Agil = 10;
            Atk = 5;
            Def = 5;
            Int = 10;
            ToNextLvl = 35;
            Experience = 0;
            Abilities = new List<string>();

            GetInitialAbilities();

            if (name == "C")
            {
                Name = "Slime";
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
            throw new NotImplementedException();
        }
        public void ScaleStatsToLvl()
        {
            if (Level > 1)
            {
                int healthScaler = Level * 3;
                int manaScaler = Level * 2;
                int agilScaler = Level * 3;
                int atkScaler = Level * 1;
                int defScaler = Level * 1;
                int intScaler = Level * 3;
                int lvlScaler = Level * 1;

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
