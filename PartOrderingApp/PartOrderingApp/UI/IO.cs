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
                    Console.WriteLine("\nSorry, user not found. Please press any key to try again.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
        public void AccountMenu(User user)
        {
            Console.Clear();

            Console.WriteLine($"\n\n Hello {user.Username}. Welcome to your account. What would you like to do?\n\n");
            Console.WriteLine("1. Browse the store inventory.\n\n2. See my past orders.\n\nOr Press esc to return to the main menu.");  //there will be an option to edit or delete upon viewing pending orders.

            var cki = Console.ReadKey();

            switch(cki.Key)
            {
                case ConsoleKey.D1:
                    BrowseInventoryWorkFlow(user);
                    break;

                case ConsoleKey.D2:
                    ViewPendingOrdersWorkFlow(user);
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

            DisplayInventory(inventory);

            Console.WriteLine("Would you like to make an order? (y or n)");

            var cki = Console.ReadKey();

            switch (cki.Key)
            {
                case ConsoleKey.Y:
                    Order order = new Order();
                    order.Parts = new List<Part>();
                    EditOrderWorkFlow(inventory, user, order);
                    break;

                case ConsoleKey.N:
                    break;

                default:
                    Console.WriteLine("\n\nSorry, that was not a valid input. Press any key to return to the menu.");
                    Console.ReadKey();
                    break;
            }
        }
        private void DisplayInventory(IInventory inventory)
        {
            Console.Clear();

            List<Part> sorted = inventory.Parts.OrderBy(p => p.Category).ThenBy(p => p.Name).ToList();

            foreach (Part part in inventory.Parts)
            {
                Console.WriteLine($"{part.Id}. {part.Name}\nCost: {part.Cost}");
                Console.WriteLine($"Number in stock: {inventory.InvDictionary[part.Id]}\n\n");

                if (inventory.InvDictionary[part.Id] < 1) 
                {
                    part.IsAvailable = false;
                }
                else
                {
                    part.IsAvailable = true;
                }
            }
        }
        private void EditOrderWorkFlow(IInventory inventory, User user, Order order)
        {
            CheckOutOrCancel = false;

            while (!CheckOutOrCancel)
            {
                Console.WriteLine("\n\nOptions: \n\n1. Add an item to cart.\n\n2. Delete an item from cart.\n\n3. Check out.\n\n4. Cancel and return to the menu.");

                var cki = Console.ReadKey();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        AddPartToOrderWorkflow(inventory, order);
                        break;

                    case ConsoleKey.D2:
                        bool success = CheckCartForZero(order);
                        if (success)
                        {
                            DeletePartFromOrderWorkflow(inventory, order);
                        }
                        else
                        {
                            DisplayInventory(inventory); 
                        }
                        break;

                    case ConsoleKey.D3:
                        success = CheckCartForZero(order);
                        if (success)
                        {
                            success = CheckOutWorkflow(inventory, user, order);
                            if(success)
                            {
                                CheckOutOrCancel = true;
                            }
                        }                                              
                        break;

                    case ConsoleKey.D4:                       
                        success = CancelOrderWorkflow(user, inventory, order);
                        if(success)
                        {
                            CheckOutOrCancel = true;
                        }                       
                        break;

                    default:
                        Console.WriteLine("\n\nInvalid input. Press any key to try again.");
                        Console.ReadKey();
                        Console.Clear();
                        DisplayInventory(inventory);
                        DisplayCart(order);
                        break;
                }
            }
        }
        private bool CheckCartForZero(Order order)
        {
            bool success = true;

            if (order.Parts.Count == 0)
            {
                success = false;
            }

            if (success == false)
            {
                Console.WriteLine("\n\nSorry, your cart is empty. Press any key to try again.");
                Console.ReadKey();
            }
            return success;
        }
        private bool CancelOrderWorkflow(User user, IInventory inventory, Order order)
        {

            Console.WriteLine("\n\nAre you sure you'd like to cancel? (y or n)");

            var cki = Console.ReadKey();

            bool success = false;

            switch (cki.Key)
            {
                case ConsoleKey.Y:
                    Manager.CancelOrder(user, order);
                    Console.WriteLine("\n\nOrder canceled. Press any key to return to the menu.");
                    Console.ReadKey();
                    success = true;
                    break;

                case ConsoleKey.N:
                    break;

                default:
                    Console.WriteLine("\n\nSorry, that was not a valid input. Press any key to return to your cart.");
                    Console.ReadKey();
                    break;
            }

            DisplayInventory(inventory);

            DisplayCart(order);

            return success;
        }
        private bool CheckOutWorkflow(IInventory inventory, User user, Order order)
        {
            WorkflowResponse response = Manager.GetOrderTotal(user, order);

            Console.WriteLine($"\n\n\n{response.Message}\n\nOrder total: {response.OrderTotal}");
            Console.WriteLine("\n\nAre you sure you'd like to confirm your order (y or n)");

            bool success = false;   

            var cki = Console.ReadKey();

            switch (cki.Key)
            {
                case ConsoleKey.Y:
                    response = Manager.ExecuteOrder(user, order);

                    Console.WriteLine($"{response.Message}");
                    Console.ReadKey();
                    if (response.Success == true)
                    {
                        success = true;
                    }
                    else
                    {
                        DisplayInventory(inventory);
                        DisplayCart(order);
                    }
                    break;

                case ConsoleKey.N:
                    Console.WriteLine("\n\nCheckout not completed. Press any key to return to your cart.");
                    Console.ReadKey();
                    Console.Clear();
                    DisplayInventory(inventory);
                    DisplayCart(order);
                    break;

                default:
                    Console.WriteLine("\n\nSorry, that was not a valid input. Press any key to return to your cart.");
                    Console.ReadKey();
                    Console.Clear();
                    DisplayInventory(inventory);
                    DisplayCart(order);
                    break;
            }
            return success;
        }
        private void DeletePartFromOrderWorkflow(IInventory inventory, Order order)
        {
            Console.WriteLine("\n\nEnter the serial number of the item to delete from the cart, then press enter.");

            string input = Console.ReadLine();

            WorkflowResponse response = Manager.DeletePartFromOrder(input, order);

            Console.WriteLine($"\n{response.Message}. Press any key to continue.");
            Console.ReadKey();

            DisplayInventory(inventory);

            DisplayCart(response.Order);
        }
        private void AddPartToOrderWorkflow(IInventory inventory, Order order)
        {
            Console.WriteLine("\n\nEnter the number of the item to add to your order, then press enter.");

            string input = Console.ReadLine();

            WorkflowResponse response = Manager.AddPartToOrder(input, order);

            Console.WriteLine($"\n{response.Message}. Press any key to continue.");
            Console.ReadKey();

            DisplayInventory(inventory); 

            DisplayCart(response.Order);
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

                Console.WriteLine($"{part.SerialNumber}. {part.Name} -- Cost: {part.Cost}"); 
            }
        }
        private void ViewPendingOrdersWorkFlow(User user)
        {
            Console.Clear();

            IInventory inventory = Manager.GetInventory();

            Console.WriteLine("        ---- Your orders ----\n\n");

            if (user.Orders.Count == 0)
            {
                Console.WriteLine("Looks like you don't have any orders yet. Press any key to return to the menu.");
                Console.ReadKey();
            }
            else
            {
                user.Orders = Manager.GetPendingStatus(user);

                foreach (Order order in user.Orders)
                {
                    if (order.PendingStatus == true)
                    {
                        Console.WriteLine($"\n\nOrder ID: {order.OrderID}\n{order.DateTime}.\nOrder total: {order.Total}\n\nParts:");

                        foreach (Part part in order.Parts)
                        {
                            Console.WriteLine($"Category: {part.Category}\n{part.Name} Cost: {part.Cost}");
                        }
                    }
                }

                EditPendingOrderWorkflow(user, inventory);
            }
        }
        private void EditPendingOrderWorkflow(User user, IInventory inventory)
        {
            Console.WriteLine("\nOptions:\n\n1. Edit an order.\n\n2. Cancel an order.\n\nEsc: Return to menu.");

            var cki = Console.ReadKey();

            switch (cki.Key)
            {
                case ConsoleKey.D1:

                    WorkflowResponse responseEdit = SelectOrderWorkflow(user); 

                    if (!responseEdit.Success) 
                    {
                        Console.WriteLine($"{responseEdit.Message}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"{responseEdit.Message}"); 
                        Console.ReadKey();

                        Order newOrder = Manager.DuplicateOrderForEditing(responseEdit.Order);
                        DisplayInventory(inventory);
                        DisplayCart(newOrder);
                        EditOrderWorkFlow(inventory, user, newOrder);
                    }
                    break;

                case ConsoleKey.D2:
                    
                    WorkflowResponse responseDelete = SelectOrderWorkflow(user);

                    if (!responseDelete.Success)
                    {
                        Console.WriteLine($"{responseDelete.Message}");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"{responseDelete.Message}");
                        Console.ReadKey();
                        DeleteOrderWorkflow(user, responseDelete.Order);
                    }
                    break;

                case ConsoleKey.Escape:
                    break;

                default:
                    Console.WriteLine("\n\nInvalid input. Press any key to return to the menu.");
                    Console.ReadKey();
                    break;
            }
        }
        private void DeleteOrderWorkflow(User user, Order order)
        {
            Console.WriteLine("\n\nAre you sure you'd like to delete this order? (y or n)");

            var cki = Console.ReadKey();

            switch(cki.Key)
            {
                case ConsoleKey.Y:
                    Manager.DeleteOrder(user, order);
                    Console.WriteLine("\n\nOrder successfully deleted. Press any key to return to the menu.");
                    Console.ReadKey();
                    break;

                case ConsoleKey.N:
                    break;

                default:
                    Console.WriteLine("\n\nError: Invalid input. Press any key to return to the menu.");
                    Console.ReadKey();
                    break;
            }
        }
        private WorkflowResponse SelectOrderWorkflow(User user)
        {
            Console.WriteLine("\n\nSelect an order by entering the Order ID, then hit enter.");

            string input = Console.ReadLine();

            WorkflowResponse response = Manager.SelectOrder(user, input);

            return response;
        }
    }
}
