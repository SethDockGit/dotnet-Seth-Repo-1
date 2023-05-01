using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using DragonWarrior.Models;
using DragonWarrior.Models.Monsters;

namespace DragonWarrior.UI
{
    public class UserInterface
    {
        public UserInterface()
        {
            GameStart();
        }
        public void GameStart()
        {
            Party party = GetStartingMonsters();

            Party compParty = GetFirstWildMonsters();

            Battle battle = new Battle(party, compParty);
        }
        public Party GetStartingMonsters()
        {
            Party party = new Party();
            party.Members = new List<PartyMember>();

            Console.WriteLine("\n... ... ");
            Console.WriteLine("... ... ");
            Console.WriteLine("... ... \n\n");
            Console.WriteLine("A Slime wants to join your party. Give it a name...\n");

            string name = Console.ReadLine();
            Console.Clear();

            Slime slime = new Slime(name, 1);

            PartyMember one = new PartyMember(slime);

            party.Members.Add(one);

            Console.WriteLine($"\n{name} joined the party!!");

            Console.WriteLine("\n... ... ");
            Console.WriteLine("... ... ");
            Console.WriteLine("... ... \n\n");
            Console.WriteLine("An Anteater wants to join your party. Give it a name...\n");

            name = Console.ReadLine();
            Console.Clear();

            Anteater anteater = new Anteater(name, 1);

            PartyMember two = new PartyMember(anteater);

            party.Members.Add(two);

            Console.WriteLine($"\n{name} joined the party!!");

            Console.WriteLine("\n... ... ");
            Console.WriteLine("... ... ");
            Console.WriteLine("... ... \n\n");
            Console.WriteLine("A Healer wants to join your party. Give it a name...\n");

            name = Console.ReadLine();
            Console.Clear();

            Healer healer = new Healer(name, 3);

            PartyMember three = new PartyMember(healer);

            party.Members.Add(three);

            Console.WriteLine($"\n{name} joined the party!!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            return party;         
        }
        public Party GetFirstWildMonsters()
        {
            Party party = new Party();
            party.Members = new List<PartyMember>();

            Anteater anteater = new Anteater("C", 1);

            PartyMember one = new PartyMember(anteater);

            party.Members.Add(one);

            Anteater anteaterTwo = new Anteater("C", 1);

            PartyMember two = new PartyMember(anteaterTwo);

            party.Members.Add(two);

            return party;
        }
    }
}
