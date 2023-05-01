using System;
using System.Collections.Generic;
using System.Text;

namespace DragonWarrior.Models
{
    public interface IMonster
    {
        string Name { get; set; }
        int Level { get; set; }
        int MaxHealth { get; set; }
        int MaxMana { get; set; }
        int Agil { get; set; }
        int Atk { get; set; }
        int Def { get; set; }
        int Int { get; set; }
        int ToNextLvl { get; set; }
        public int Experience { get; set; }
        public List<string> Abilities { get; set; }

        public void GetInitialAbilities();
        public void GainExperience(int experience);
        public void LevelUp();
        public void ScaleStatsToLvl();
        public void CheckForGainedAbility();
    }
}
