using System;
using System.Collections.Generic;
using System.Text;

namespace DragonWarrior.Models
{
    public class BattleAction
    {
        public PartyMember Acting { get; set; }
        public PartyMember Receiving { get; set; }
        public ActionType ActionType { get; set; }

        public BattleAction(PartyMember acting, PartyMember receiving, ActionType type)
        {
            Acting = acting;
            Receiving = receiving;  
            ActionType = type;
        }
    }
}
