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
                User userToCheck = IDataSource.GetUser(user);
                PrintUserResources(userToCheck);
            }
            else
            {
                Console.WriteLine($"Error: user {username} not found. Press any key to return to the main menu... ");
                Console.ReadKey();
            }
        }
        public void DepositResource()
        {
            string username = GetUsername();
            User user = IDataSource.Authenticate(username);


            if (user == null)
            {
                Console.WriteLine($"Error: user {username} not found. Press any key to return to the main menu... ");
                Console.ReadKey();
            }
            else
            {
                ResourceType resource = GetResourceType(user);

                if(resource == ResourceType.Invalid)
                {
                    Console.WriteLine($"Error: Invalid input entered for resource type. Press any key to return to the main menu... ");
                    Console.ReadKey();
                }
                else
                {
                    int amount = GetIntFromUser($"How much {resource} would you like to deposit?\n");

                    if (amount <= 0)
                    {
                        Console.WriteLine($"Error: Resource amount must be an integer greater than 0. Press any key to return to the main menu... ");
                        Console.ReadKey();
                    }
                    else
                    {
                        int newAmount = RouteDeposit(user, resource, amount);
                        Console.Clear();
                        Console.WriteLine($"Deposit successful. {amount} added to {resource}.\n\nNew balance: {newAmount}");
                        Console.WriteLine("\nPress any key to return to the main menu");
                        Console.ReadKey();

                    }
                }
            }
        }
        private bool CheckForSufficientBalance(User user, ResourceType resource, int amount)
        {
            bool insufficient = false;
            switch (resource)
            {
                case ResourceType.Wood:
                    insufficient = user.WoodCount >= amount;
                    break;

                case ResourceType.Stone:
                    insufficient = user.StoneCount >= amount;
                    break;

                case ResourceType.Iron:
                    insufficient = user.IronCount >= amount;
                    break;

                case ResourceType.Gold:
                    insufficient = user.GoldCount >= amount;
                    break;

                default:
                    throw new Exception("Resource Type not matched");
                    break;
            }
            return insufficient;
        }
        public void WithdrawResource()
        {
            string username = GetUsername();
            User user = IDataSource.Authenticate(username);


            if (user == null)
            {
                Console.WriteLine($"Error: user {username} not found. Press any key to return to the main menu... ");
                Console.ReadKey();
            }
            else
            {
                ResourceType resource = GetResourceType(user);

                if (resource == ResourceType.Invalid)
                {
                    Console.WriteLine($"Error: Invalid input entered for resource type. Press any key to return to the main menu... ");
                    Console.ReadKey();
                }
                else
                {
                    int amount = GetIntFromUser($"How much {resource} would you like to withdraw?\n");

                    if (amount <= 0)
                    {
                        Console.WriteLine($"Error: Resource amount must be an integer greater than 0. Press any key to return to the main menu... ");
                        Console.ReadKey();
                    }
                    else
                    {
                        int newAmount = RouteWithdrawal(user, resource, amount);

                        if (newAmount < 0)
                        {
                            Console.WriteLine("Error: amount withdrawn larger than balance. Withdrawal failed.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Withdrawal successful. {amount} withdrawn from {resource}.\n\nNew balance: {newAmount}");
                            Console.WriteLine("\nPress any key to return to the main menu");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }
        private int RouteWithdrawal(User user, ResourceType resource, int amount)
        {
            int newAmount;
            switch (resource)
            {
                case ResourceType.Wood:
                    newAmount = IDataSource.WithdrawWood(user, amount);
                    break;

                case ResourceType.Stone:
                    newAmount = IDataSource.WithdrawStone(user, amount);
                    break;

                case ResourceType.Iron:
                    newAmount = IDataSource.WithdrawIron(user, amount);
                    break;

                case ResourceType.Gold:
                    newAmount = IDataSource.WithdrawGold(user, amount);
                    break;

                default:
                    throw new Exception("ResourceType parameter not matched to a method");
            }
            return newAmount;
        }
        private int RouteDeposit(User user, ResourceType resource, int amount)
        {
            int newAmount;
            switch(resource)
            {
                case ResourceType.Wood:
                    newAmount = IDataSource.DepositWood(user, amount);
                    break;

                case ResourceType.Stone:
                    newAmount = IDataSource.DepositStone(user, amount);
                    break;

                case ResourceType.Iron:
                    newAmount = IDataSource.DepositIron(user, amount);
                    break;

                case ResourceType.Gold:
                    newAmount = IDataSource.DepositGold(user, amount);
                    break;

                default:
                    throw new Exception("ResourceType parameter not matched to a method");
            }
            return newAmount;
        }
        private ResourceType GetResourceType(User user)
        {
            Console.Clear();
            Console.WriteLine("***Select a resource, then press enter***\n");
            Console.WriteLine("1. Wood\n2. Stone\n3. Iron\n4. Gold\n");
            string userInput = Console.ReadLine();

            ResourceType resource;
        

            switch (userInput)
            {
                case "1":
                    resource = ResourceType.Wood;
                    break;

                case "2":
                    resource = ResourceType.Stone;
                    break;

                case "3":
                    resource = ResourceType.Iron;
                    break;

                case "4":
                    resource = ResourceType.Gold;
                    break;

                default:
                    resource = ResourceType.Invalid;
                    break;
            }
            return resource;  
        }
        private int GetIntFromUser(string prompt)
        {
            Console.Clear();
            Console.WriteLine($"{prompt}");
            string toParse = Console.ReadLine();

            int amount = -1;
            int.TryParse(toParse, out amount);
            return amount;

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
