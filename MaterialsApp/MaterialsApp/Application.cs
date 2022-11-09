using MaterialsApp.Data;
using MaterialsApp.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsApp
{
    class Application
    {
        private bool Exit { get; set; } = false;
        private Manager Manager { get; set; }

        public Application(IDataSource dataSource)
        {
            Manager = new Manager(dataSource);
        }

        public void Run()
        {
            while (!Exit)
            {
                Menu();
            }
        }

        public void Menu()
        {
            Console.Clear();

            Console.WriteLine("                    ***** Materials App *****\n\n");
            Console.WriteLine("--------------------");
            Console.WriteLine("-  Menu Selection  -");
            Console.WriteLine("--------------------");
            Console.WriteLine("1. Check resources");
            Console.WriteLine("2. Deposit a resource");
            Console.WriteLine("3. Withdraw a resource");
            Console.WriteLine("--------------------\n");
            Console.WriteLine("Press a number to select a menu item, or ESC to quit: ");

            var cki = Console.ReadKey(true);

            switch (cki.Key)
            {
                case ConsoleKey.D1:
                    Manager.CheckResources();

                    break;
                case ConsoleKey.D2:
                    Manager.DepositResource();

                    break;
                case ConsoleKey.D3:
                    Manager.WithdrawResource();

                    break;
                case ConsoleKey.Escape:

                    Exit = true;
                    break;

                default:
                    break;
            }
        }
    }
}
