using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PartOrderingApp.Data;
using PartOrderingApp.Logic;
using PartOrderingApp.Models;

namespace PartOrderingApp.UI
{
    public class IO
    {
        private Manager Manager { get; set; }
        public bool ExitMain = false;
        public bool ExitAcct = false;
        public bool CheckOutOrCancel = false;

        public IO(IInventory inventory, IUserData userData)
        {
            Manager = new Manager(inventory, userData);
        }

        public void MainMenu()
        {
            while (!ExitMain)
            {
                Console.WriteLine("\n\n     ~~.* @ * PC PARTS STORE * @ *.~~\n\n");

                Console.WriteLine("Welcome to PC Parts. Please enter your username.\n\n");

                string username = Console.ReadLine();

                User user = Manager.Authenticate(username);

                if (user != null)
                {
                    while (!ExitAcct)
                    {
                        AccountMenu(user);
                    }
                    ExitAcct = false;
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
            Console.Clear();

            Console.WriteLine($"\n\n Hello {user.UserName}. Welcome to your account. What would you like to do?\n\n");
            Console.WriteLine("1. Browse the store inventory.\n\n2. See my pending orders.\n\nOr Press esc to return to the main menu.");  //there will be an option to edit or delete upon viewing pending orders.

            var cki = Console.ReadKey();

            switch(cki.Key)
            {
                case ConsoleKey.D1:
                    BrowseInventoryWorkFlow(user);
                    break;

                case ConsoleKey.D2:
                    ViewPendingOrdersWorkFlow();
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
        private void BrowseInventoryWorkFlow(User user)
        {
            IInventory inventory = Manager.GetInventory();

            DisplayInventory(inventory, user);

            Console.WriteLine("Would you like to make an order? (y or n)");

            var cki = Console.ReadKey();

            switch (cki.Key)
            {
                case ConsoleKey.Y:
                    CreateNewOrderWorkFlow(inventory, user);
                    break;

                case ConsoleKey.N:
                    break;

                default:
                    Console.WriteLine("\n\nSorry, that was not a valid input. Press any key to return to the menu.");
                    Console.ReadKey();
                    break;
            }
        }
        private void DisplayInventory(IInventory inventory, User user)
        {
            Console.Clear();

            foreach (Part part in inventory.Parts)
            {
                if (user.Category == UserCategory.Premium)
                {
                    part.Cost = part.Cost * .9m;
                }
                Console.WriteLine($"{part.Id}. {part.Name}\nCost: {part.Cost}");
                Console.WriteLine($"Number in stock: {inventory.Inventory[part.Id]}\n\n");

                if (inventory.Inventory[part.Id] < 1)
                {
                    part.IsAvailable = false;
                }
                else
                {
                    part.IsAvailable = true;
                }
            }
        }
        private void CreateNewOrderWorkFlow(IInventory inventory, User user)
        {

            //at this point the inventory is displayed. I want to give them the option to add an item. 
            //Once they add an item, display the cart, and give them the chance to add another or delete, or check out
            //once the checkout, run check out method to confirm total price.

            Order order = new Order();

            order.Parts = new List<Part>();

            WorkflowResponse response = AddPartToOrderWorkflow(inventory, user, order);

            while (!CheckOutOrCancel)
            {
                Console.WriteLine("\n\nOptions: \n\n1. Add another item to cart.\n\n2. Delete an item from cart.\n\n3. Check out.\n\n4. Cancel order and return to the menu.");

                //both checkout and cancel should ask if they are sure.

                var cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        WorkflowResponse workflowResponse = AddPartToOrderWorkflow(inventory, user, order);
                        break;

                    case ConsoleKey.D2:
                        workflowResponse = DeletePartFromOrderWorkflow(inventory, user, order);
                        break;

                    case ConsoleKey.D3:
                        CheckOutWorflow();
                        CheckOutOrCancel = true;
                        break;

                    case ConsoleKey.D4:
                        CancelOrderWorkflow();
                        break;

                    default:
                        Console.WriteLine("Didn't wanna end up here did ya?");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private void CancelOrderWorkflow()
        {
            //Ask if they are sure they'd like to cancel, then do your thing.
        }
        private void CheckOutWorflow()
        {
            //show the total of their cart. Ask if they are sure they'd like to check out. Then do your thing
        }
        private WorkflowResponse DeletePartFromOrderWorkflow(IInventory inventory, User user, Order order)
        {
            Console.WriteLine("\n\nEnter the number of the item to delete from your order, then press enter.");

            string input = Console.ReadLine();

            WorkflowResponse response = new WorkflowResponse();

            return response;
        }
        private WorkflowResponse AddPartToOrderWorkflow(IInventory inventory, User user, Order order)
        {
            Console.WriteLine("\n\nEnter the number of the item to add to your order, then press enter.");

            string input = Console.ReadLine();

            WorkflowResponse response = Manager.AddPartToOrder(input, order);

            Console.WriteLine($"\n{response.Message}. Press any key to continue.");
            Console.ReadKey();

            DisplayInventory(inventory, user); //refreshes inventory with updated numbers

            DisplayCart(response.Order);

            return response;
        }
        private void DisplayCart(Order order)
        {
            Console.WriteLine("Your current cart:\n");

            if(order.Parts.Count == 0)
            {
                Console.WriteLine("Your cart is currently empty");
            }

            foreach(Part part in order.Parts)
            {
                Console.WriteLine($"{part.PlaceInOrder}. {part.Name} -- Cost: {part.Cost}"); //how do I shave number to hundredths place?
            }
        }
        private void ViewPendingOrdersWorkFlow()
        {
            //this will get a list of orders by date and orderID
        }

    }
}
