using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace DragonWarrior.Models.Monsters
{
    public class Healer : IMonster
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

        public Healer(string name, int level)
        {
            Name = name;
            Level = level;
            MaxHealth = 28;
            MaxMana = 22;
            Agil = 9;
            Atk = 4;
            Def = 8;
            Int = 8;
            ToNextLvl = 100;
            Experience = 0;
            Abilities = new List<string>();

            GetInitialAbilities();

            ScaleStatsToLvl();

            if (name == "C")
            {
                Name = "Healer";
            }
        }

        public void GetInitialAbilities()
        {
            Abilities.Add("Heal");

            if (Level > 8)
            {
                Abilities.Remove("Heal");
                Abilities.Add("HealMore");
            }
            if (Level > 15)
            {
                Abilities.Remove("HealMore");
                Abilities.Add("HealAll");
            }
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
            if (Level == 8)
            {
                Abilities.Remove("Heal");
                Abilities.Add("HealMore");

                Console.WriteLine($"{Name}'s Heal became HealMore!");
            }
            if (Level == 15)
            {
                Abilities.Remove("HealMore");
                Abilities.Add("HealAll");

                Console.WriteLine($"{Name}'s HealMore became HealAll!");
            }
        }
        public void ScaleStatsToLvl()
        {
            if (Level > 1)
            {
                int healthScaler = Level * 3;
                int manaScaler = Level * 3;
                int agilScaler = Level * 3;
                int atkScaler = Level * 1;
                int defScaler = Level * 1;
                int intScaler = Level * 4;
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
        public void UseAbility(string abilityName, PartyMember target)
        {
            switch(abilityName)
            {
                case "Heal":
                    Heal(target);
                    break;
            }
        }
        public void Heal(PartyMember target)
        {
            int upperHeal = Int + Level;
            int lowerHeal = Int - 3;

            Random rng = new Random();

            int healAmount = rng.Next(lowerHeal, upperHeal);

            target.Health += healAmount;
        }
        public void HealMore(PartyMember target)
        {
            //not implemented
        }
        public void HealAll(PartyMember target)
        {
            //not implemented
        }
    }
}
