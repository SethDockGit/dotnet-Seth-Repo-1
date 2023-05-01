using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DragonWarrior.Models.Monsters;

namespace DragonWarrior.Models
{
    public class Battle
    {
        private bool IsOver;
        private Party PlayerParty;
        private Party ComputerParty;
        private Random Rng;

        public Battle(Party playerParty, Party computerParty)
        {
            PlayerParty = playerParty;
            ComputerParty = computerParty;
            Rng = new Random();

            foreach(var p in computerParty.Members)
            {
                if(p == computerParty.Members[0])
                {
                    p.BattleId = 1;
                }
                else
                {
                    int max = computerParty.Members.Max(p => p.BattleId);
                    p.BattleId = max + 1;
                }
                p.Monster.Name = $"{p.Monster.Name}" + $"{p.BattleId}";
            }

            StartBattle();
        }
        public void StartBattle()
        {
            Console.Clear();

            string encounterText = GetEncounterText();

            Console.WriteLine(encounterText);
            Console.ReadKey();

            IsOver = false;

            while(!IsOver)
            {
                ChooseStrategy();

                //IF STRATEGY ISN'T TOO ATTACK, DON'T BATTLE. DO SOMETHING ELSE.
                ExecuteBattleRound();
            }
        }
        private void ExecuteBattleRound()
        {
            List<PartyMember> sortedBySpeed = GetBattleOrder();

            GetComputerActions();

            foreach(var p in sortedBySpeed)
            {
                if(p.Status != Status.Fainted)
                {
                    ExecuteBattleAction(p);
                }
            }
        }
        private void GetComputerActions()
        {
            foreach(var p in ComputerParty.Members)
            {
                int upperRange = PlayerParty.Members.Count - 1;

                int index = Rng.Next(0, upperRange);

                PartyMember target = PlayerParty.Members[index];


                //UPDATE AS DEFEND AND SPELLS ARE IMPLEMENTED.
                BattleAction action = new BattleAction(p, target, ActionType.Attack);

                p.Action = action;
            }
        }
        private void ExecuteBattleAction(PartyMember p)
        {
            switch(p.Action.ActionType)
            {
                case ActionType.Attack:
                    ExecuteAttack(p.Action);
                    break;
                case ActionType.Defend:
                    ExecuteDefend(p.Action);
                    break;
                case ActionType.Ability:
                    ExecuteAbility(p.Action);
                    break;
            }
        }
        private void ExecuteAbility(BattleAction action)
        {
            throw new NotImplementedException();
        }
        private void ExecuteDefend(BattleAction action)
        {
            throw new NotImplementedException();
        }
        private List<PartyMember> GetBattleOrder()
        {
            List<PartyMember> tempParty = new List<PartyMember>();

            foreach(var p in PlayerParty.Members)
            {
                tempParty.Add(p);
            }
            foreach (var p in ComputerParty.Members)
            {
                tempParty.Add(p);
            }

            PartyMember[] combined = tempParty.ToArray();

            for (int j = 0; j < combined.Length - 1; j++)
            {

                if (combined[j].Monster.Agil < combined[j + 1].Monster.Agil)
                {
                    PartyMember temp = combined[j];
                    combined[j] = combined[j + 1];
                    combined[j + 1] = temp;

                    j = -1;
                }
            }
            return combined.ToList();
        }
        private string GetEncounterText()
        {
            string monsterName = ComputerParty.Members[0].Monster.Name;

            string encounterText;

            if(ComputerParty.Members.Count > 1)
            {
                encounterText = $"Oh no! A pack of wild {monsterName} appeared!";
            }
            else
            {
                encounterText = $"Oh no! A wild {monsterName} appeared!";
            }

            return encounterText;
        }
        private void ChooseStrategy()
        {
            bool fail = true;

            while(fail)
            {
                ShowPartyAndFoe();

                Console.WriteLine("\n\nWhat will you do?\n\n");

                Console.WriteLine("1. Attack\n");
                Console.WriteLine("2. Use Item\n");
                Console.WriteLine("3. Run\n\n");

                var cki = Console.ReadKey();

                switch(cki.Key)
                {
                    case ConsoleKey.D1: PlayerAttack();
                        fail = false;
                        break;

                    case ConsoleKey.D2: UseItem();
                        fail = false;
                        break;

                    case ConsoleKey.D3: Run();
                        fail = false;
                        break;

                    default:
                        Console.WriteLine("\n\nPlease select a valid option.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
        private void PlayerAttack()
        {
            List<PartyMember> currentParty = PlayerParty.Members.Where(p => p.Status != Status.Fainted).ToList();

            foreach (var m in currentParty)
            {
                bool turnOver = false;

                while(!turnOver)
                {
                    Console.Clear();
                    ShowPartyAndFoe();

                    Console.WriteLine($"\n\nWhat will {m.Monster.Name} do?\n\n");
                    Console.WriteLine("1. Attack       2. Defend       3. Use ability");

                    var cki = Console.ReadKey();

                    switch (cki.Key)
                    {
                        case ConsoleKey.D1:
                            MonsterAttack(m);
                            turnOver = true;
                            break;

                        case ConsoleKey.D2:
                            MonsterDefend();
                            turnOver = true;
                            break;

                        case ConsoleKey.D3:
                            MonsterUseAbility(m);
                            turnOver = true;
                            break;

                        default:
                            Console.WriteLine("\n\nPlease select a valid option. Press any key to continue.\n\n");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }
        private void MonsterUseAbility(PartyMember m)
        {
            ShowPartyAndFoe();

            Console.WriteLine("\n\nWhich ability will you use?\n\n");

            int selector = 1;

            //while fail... copy monsterattack flow...

            foreach(var a in m.Monster.Abilities)
            {
                Console.WriteLine($"{selector}. {a}");
                selector++;
            }
            var cki = Console.ReadKey();

        }
        private void MonsterAttack(PartyMember m)
        {
            ShowPartyAndFoe();

            Console.WriteLine($"\n\nWho will {m.Monster.Name} attack?\n\n");

            ShowAttackOptions();

            var cki = Console.ReadKey();

            Console.WriteLine();

            bool fail = true;

            while(fail)
            {
                if (!char.IsDigit(cki.KeyChar))
                {
                    ShowPartyAndFoe();
                    Console.WriteLine($"\n\nTry again. Who will {m.Monster.Name} attack?\n\n");
                    ShowAttackOptions();
                    cki = Console.ReadKey();
                }
                else
                {
                    int selection = int.Parse(cki.KeyChar.ToString());

                    //handle out of range

                    if (ComputerParty.Members[selection - 1] == null)
                    {
                        ShowPartyAndFoe();
                        Console.WriteLine($"\n\nTry again. Who will {m.Monster.Name} attack?\n\n");
                        ShowAttackOptions();
                        cki = Console.ReadKey();
                    }
                    else
                    {
                        fail = false;

                        List<PartyMember> attackOptions = ComputerParty.Members.Where(m => m.Status != Status.Fainted).ToList();

                        PartyMember toAttack = attackOptions[selection - 1];

                        BattleAction action = new BattleAction(m, toAttack, ActionType.Attack);
                        m.Action = action;
                    }
                }
            }
        }
        private void ShowAttackOptions()
        {
            var attackOptions = ComputerParty.Members.Where(m => m.Status != Status.Fainted).ToList();

            int selector = 1;

            foreach (var o in attackOptions)
            {
                Console.Write($"{selector}. {o.Monster.Name}\n");
                selector++;
            }
        }
        private void ExecuteAttack(BattleAction action)
        {
            ShowPartyAndFoe();

            int attackPower = action.Acting.Monster.Atk;

            int defendPower = action.Receiving.Monster.Def;

            int lowerLimit = attackPower - defendPower;

            if(lowerLimit < 0)
            {
                lowerLimit = 0;
            }

            int upperLimit = lowerLimit + 10;   //do some more elegant calculating for this.

            int attack = Rng.Next(lowerLimit, upperLimit);

            if(action.Receiving.Status == Status.Fainted)
            {
                Console.WriteLine($"\n\n{action.Acting.Monster.Name} tried to attack {action.Receiving.Monster.Name}... but nothing happened!!");
            }
            else
            {
                action.Receiving.Health -= attack;

                Console.WriteLine($"\n\n{action.Acting.Monster.Name} hit {action.Receiving.Monster.Name} for {attack} damage!!!");
                Console.ReadKey();

                if (action.Receiving.Health < 0)
                {
                    action.Receiving.Status = Status.Fainted;
                    Console.WriteLine($"\n\n{action.Receiving.Monster.Name} fainted!!\n\n");
                    Console.ReadKey();
                }
            }

            CheckEndBattle();
        }
        private void CheckEndBattle()
        {
            if (!ComputerParty.Members.Any(m => m.Status != Status.Fainted))
            {
                EndBattle("win");
                IsOver = true;
            }
            else if (!PlayerParty.Members.Any(m => m.Status != Status.Fainted))
            {
                EndBattle("lose");
                IsOver = true;
            }
        }
        private void EndBattle(string winOrLose)
        {
            Console.Clear();

            if(winOrLose == "win")
            {
                string monsterName = ComputerParty.Members[0].Monster.Name;

                Console.WriteLine($"\n\nVictory! Defeated {monsterName}!!");
                Console.ReadKey();

                DoleOutExperiencePoints();
            }
            else if(winOrLose == "lose")
            {
                Console.WriteLine($"\n\nYou are out of usable monsters... You ran from the battle!!\n\n");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private void DoleOutExperiencePoints()
        {
            int experience = 0;

            foreach(var p in ComputerParty.Members)
            {
                int baseLine = 15;

                int levelScaler = p.Monster.Level * 5;

                experience += (levelScaler + baseLine);
            }

            foreach(var p in PlayerParty.Members)
            {
                p.Monster.GainExperience(experience);
            }
        }
        private void MonsterDefend()
        {
            throw new NotImplementedException();
        }
        private void UseItem()
        {
            Console.Clear();

        }
        private void Run()
        {
            Console.Clear();
        }
        private void ShowPartyAndFoe()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------\n");

            foreach (var m in ComputerParty.Members)
            {
                if(m.Status == Status.Fainted)
                {
                    Console.Write($"--XXX--");
                }
                else
                {
                    Console.Write($"--{m.Monster.Name}--");
                }
            }

            Console.WriteLine("\n\n----------------------------------\n\n");

            foreach (var p in PlayerParty.Members)
            {
                if(p.Status == Status.Fainted)
                {
                    Console.Write("XXX\n");
                }
                else
                {
                    Console.Write($"{p.Monster.Name} -- Health: {p.Health}  Mana: {p.Mana}\n");
                }
            }
        }
    }
}
