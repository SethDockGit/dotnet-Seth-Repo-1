using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Logic;
using PartOrderingApp.Models;

namespace PartOrderingApp.UI
{
    public class IO
    {
        private Manager Manager { get; set; }
        public bool ExitMain = false;
        public bool ExitAcct = false;

        public IO()
        {
            Manager = new Manager();
        }

        public void MainMenu()
        {
            while (!ExitMain)
            {
                Console.WriteLine("\n\n     ~~.* @ * PC PARTS STORE * @ *.~~\n\n");

                Console.WriteLine("Welcome to PC Parts. Please enter your username.\n\n");

                string username = Console.ReadLine();

                Console.WriteLine("\n\nPlease enter your Password. \n\n");

                string password = Console.ReadLine();

                User user = Manager.Authenticate(username, password);

                if (user != null)
                {
                    while (!ExitAcct)
                    {
                        AccountMenu(user);
                    }
                    ExitAcct = false; //once I exit the acct I need to flip this back to false so I am able to enter the account again.
                }
                else
                {
                    Console.WriteLine("Sorry, user not found. Please press any key to try again.");
                    Console.ReadKey();
                }
            }
        }
        public void AccountMenu(User user)
        {
            Account account = new Account(user);

            Console.Clear();

            Console.WriteLine($"\n\n Hello {user.UserName}. Welcome to your account. What would you like to do?\n\n");
            Console.WriteLine("1. Browse the store inventory.\n\n2. See my pending orders.\n\n3. Make a new order.\n\nOr Press esc to return to the main menu");  //there will be an option to edit or delete upon viewing pending orders.

            var cki = Console.ReadKey();

            switch(cki.Key)
            {
                case ConsoleKey.D1:
                    BrowseInventoryWorkFlow();
                    break;

                case ConsoleKey.D2:
                    ViewPendingOrdersWorkFlow();
                    break;

                case ConsoleKey.D3:
                    CreateNewOrderWorkFlow();
                    break;

                case ConsoleKey.Escape:
                    ExitAcct = true;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Sorry, invalid input. Please try again.");
                    break;
            }
        }

        private void CreateNewOrderWorkFlow()
        {
            throw new NotImplementedException();
        }

        private void ViewPendingOrdersWorkFlow()
        {
            //this will get a list of orders by date and orderID
        }

        private void BrowseInventoryWorkFlow()
        {
            //this will get a workflow response from the manager method called "BrowseInventory"

            //if successful a workflow response will be printed.
        }
    }
}
