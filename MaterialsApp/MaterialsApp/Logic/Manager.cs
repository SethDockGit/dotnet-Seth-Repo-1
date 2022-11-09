using MaterialsApp.Data;
using MaterialsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsApp.Logic
{
    class Manager
    {
        private IDataSource IDataSource { get; set; }

        public Manager(IDataSource dataSource)
        {
            IDataSource = dataSource;
        }

        public void CheckResources()
        {
            string username = GetUsername();
            User user = IDataSource.Authenticate(username);

            if (user != null)
            {
                User userToCheck = IDataSource.CheckResources(user);
                PrintUserResources(userToCheck);
            }
            else
            {
                Console.WriteLine($"Error: user {username} not found. Press any key to return to the main menu... ");
                Console.ReadKey();
            }
        }

        //if I were to do it differently next time I would pass the user's resource info back to the manager and have the manager
        //do the math. Oh well. It works this way but it's not exemplary. The methods probably shouldn't need 3 parameters
        //But also, I figured maybe the math should be done at the datasource level since it is changing the data?
        public void DepositResource()
        {
            string username = GetUsername();
            User user = IDataSource.Authenticate(username);
           

            if (user != null)
            {
                int key = GetKey();

                if(key < 5 && key > 0)
                {
                    int deposit = GetDeposit();
                    if(deposit > 0)
                    {
                        int newCount = IDataSource.DepositResource(user, key, deposit);
                        Console.Clear();
                        Console.WriteLine($"\nDeposited {deposit}. New count is {newCount}.");
                        Console.WriteLine("\nPress any key to return to the main menu");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: user {username} not found. Press any key to return to the main menu... ");
                Console.ReadKey();
            }
        }

        private int GetKey()
        {
            Console.Clear();
            Console.WriteLine("***Select a resource, then press enter***\n");
            Console.WriteLine("1. Wood\n2. Stone\n3. Iron\n4. Gold\n");
            string toParse = Console.ReadLine();

            int key;
            bool success = int.TryParse(toParse, out key);
            if(success && key < 5 && key >0)
            {
                return key;
            }
            else
            {
                Console.WriteLine("Error: Invalid input. Press any key to return to the main menu");
                Console.ReadKey();
                return key;
            }
        }

        private int GetDeposit()
        {
            Console.Clear();
            Console.WriteLine("Enter the number you will deposit.\n");
            string toParse = Console.ReadLine();

            int deposit;
            bool success = int.TryParse(toParse, out deposit);
            if(success && deposit>0)
            {
                return deposit;
            }
            else
            {
                Console.WriteLine("Error: Invalid input. Press any key to return to the main menu");
                Console.ReadKey();
                return deposit;
            }

        }
        private int GetWithdrawal()
        {
            Console.Clear();
            Console.WriteLine("Enter the number you will withdraw.\n");
            string toParse = Console.ReadLine();

            int withdrawal;
            bool success = int.TryParse(toParse, out withdrawal);
            if (success && withdrawal > 0)
            {
                return withdrawal;
            }
            else
            {
                Console.WriteLine("Error: Invalid input. Press any key to return to the main menu");
                Console.ReadKey();
                return withdrawal;
            }

        }
        public void WithdrawResource()
        {
            string username = GetUsername();
            User user = IDataSource.Authenticate(username);

            if (user != null)
            {
                int key = GetKey();

                if(key < 5 && key > 0)
                {
                    int withdrawal = GetWithdrawal();
                    if(withdrawal > 0)
                    {
                        int newCount = IDataSource.WithdrawResource(user, key, withdrawal);
                        Console.Clear();
                        Console.WriteLine($"\nWithdrew {withdrawal}. New count is {newCount}.");
                        Console.WriteLine("\nPress any key to return to the main menu.");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: user {username} not found. Press any key to return to the main menu... ");
                Console.ReadKey();
            }
        }

        private string GetUsername()
        {
            Console.Clear();

            Console.Write("Please enter your username: ");
            string input = Console.ReadLine();
            
            return input;
        }

        private void PrintUserResources(User user)
        {
            Console.Clear();

            Console.WriteLine($"{user.Username}'s Materials:\n\n");
            Console.WriteLine($"Wood: {user.WoodCount}");
            Console.WriteLine($"Stone: {user.StoneCount}");
            Console.WriteLine($"Iron: {user.IronCount}");
            Console.WriteLine($"Gold: {user.GoldCount}");
            Console.WriteLine("\n\nPress any key to return to the main menu...");

            Console.ReadKey();
        }


    }
}
