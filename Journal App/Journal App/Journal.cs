using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal_App
{
    internal class Journal
    {
        private List<Entry> Notes;
        private List<Entry> DailyEntries;


        public Journal()
        {
            List<Entry> notes = new List<Entry>();
            Notes = notes;
            List<Entry> dailyEntries = new List<Entry>();
            DailyEntries = dailyEntries;

            DisplayMenu();
        }

        private void DisplayMenu()
        {
            Console.WriteLine("Hello. Welcome to your journal. Please select an option from the menu");
            Console.WriteLine();
            Console.WriteLine("1. Press n to create a new note in the journal.");
            Console.WriteLine();
            Console.WriteLine("2. Press j to create your daily journal entry."); 
            Console.WriteLine();
            Console.WriteLine("3. Press v to view and edit notes and daily entries.");
            Console.WriteLine();

            ConsoleKeyInfo keyPress = Console.ReadKey(true);
            ConsoleKey input = keyPress.Key;

            switch (input)
            {
                case ConsoleKey.N:
                    CreateNote();
                    break;
                case ConsoleKey.J:
                    CreateDailyEntry();
                    break;
                case ConsoleKey.V:
                    ViewEntry();
                    break;
                default:
                    Console.WriteLine("That is not a valid entry. Press R then Enter to return to main menu");
                    string tryAgain = Console.ReadLine();
                    Console.Clear();
                    ReturnToMainMenu(tryAgain);
                    break;
            }
        }
        public void CreateNote()
        {
            
            Console.Clear();
            Console.WriteLine("Please enter a title and/or date for this entry"); //date will be the title.
            string entryTitle = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Write your entry here. Press enter to save the entry");
            Console.WriteLine();

            int bufSize = 1024;
            Stream inStream = Console.OpenStandardInput(bufSize);
            Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));

            string bodyText = Console.ReadLine();

            Notes.Add(new Entry(entryTitle, bodyText));  //note is created and saved here

            Console.WriteLine("Entry saved. Press R to return to main menu");
            string userInput = Console.ReadLine();
            ReturnToMainMenu(userInput);
        }
        public void CreateDailyEntry()
        {
            Console.Clear();
            Console.WriteLine("Please enter a date for this entry"); //date will be the title.
            string entryTitle = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Write your entry here. Press enter to save the entry");
            Console.WriteLine();

            int bufSize = 1024;
            Stream inStream = Console.OpenStandardInput(bufSize);
            Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));

            string bodyText = Console.ReadLine();

            DailyEntries.Add(new Entry(entryTitle, bodyText));

            Console.WriteLine("Entry saved. Press R to return to main menu");
            string userInput = Console.ReadLine();
            ReturnToMainMenu(userInput);
        }
        public void ViewEntry()
        {
            Console.Clear();
            Console.WriteLine("Please enter \"notes\" to access notes or \"daily\" to access daily entries. Enter \"r\" to return to main menu.");
            string userChoice = Console.ReadLine();
            Console.Clear();
            if (userChoice == "notes")
            { 
                foreach(Entry entry in Notes)
                {
                    Console.WriteLine(entry.Title);
                }
                Console.ReadLine();
            }
            else if (userChoice == "daily")
            {
                foreach (Entry entry in DailyEntries)
                {
                    Console.WriteLine(entry.Title);
                }
                Console.ReadLine();
            }
            else
            {
                ReturnToMainMenu(userChoice);
            }
            Console.WriteLine("If you'd like to edit an entry, enter the title of the entry to edit");
            string title = Console.ReadLine();

            if (userChoice == "notes")
            {
                EditNotesEntry(title);
            }
            if (userChoice == "daily")
            {
                EditDailyEntry(title);
            }
        }
        public void EditNotesEntry(string title)
        {   
            foreach (Entry entry in Notes)
            {
                if (title == entry.Title)
                {
                    Console.Clear();
                    Console.WriteLine("Enter Edit to Edit or Delete to Delete your entry?");
                    string editOrDelete = Console.ReadLine().ToLower();
                    if (editOrDelete == "edit")
                    {
                        entry.Edit(entry);
                    }
                    if (editOrDelete == "delete")
                    {
                        entry.Delete(entry);
                    }
                    else
                    {
                        Console.WriteLine($"Sorry, {editOrDelete} was not an option. Return to main menu");
                        editOrDelete = "r";
                        ReturnToMainMenu(editOrDelete);
                    }
                }
            }
            title = "r";
            ReturnToMainMenu(title);
        }
        public void EditDailyEntry(string title)
        {
            foreach (Entry entry in DailyEntries)
            {
                if (title == entry.Title)
                {
                    entry.Edit(entry);  
                }
            }
            title = "r";
            ReturnToMainMenu(title);
        }
        public void ReturnToMainMenu(string needsAnR)
        {
            if (needsAnR == "r")
            {
                Console.Clear();
                DisplayMenu();
            }
            else
            {
                Console.WriteLine("That is not a valid input. Please press R then Enter to return to main menu");
                string tryAgain = Console.ReadLine().ToLower();
                if (tryAgain == "r")
                {
                    Console.Clear();
                    DisplayMenu();
                }
            }
        }
    }
}
