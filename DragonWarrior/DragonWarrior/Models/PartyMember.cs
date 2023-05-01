using System;
using System.Collections.Generic;
using System.Text;
using DragonWarrior.Models;

namespace DragonWarrior.Models
{
    public class PartyMember
    {
        public IMonster Monster;
        public int Health;
        public int Mana;
        public Status Status;
        public BattleAction Action;
        public int BattleId;
        public bool isComputer;

        public PartyMember(IMonster monster)
        {
            Monster = monster;
            Health = monster.MaxHealth;
            Mana = monster.MaxMana;
            Status = Status.Normal;
        }
    }
}
