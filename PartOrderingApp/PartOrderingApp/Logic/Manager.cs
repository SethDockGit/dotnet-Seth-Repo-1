using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PartOrderingApp.Data;
using PartOrderingApp.Models;

namespace PartOrderingApp.Logic
{
    public class Manager
    {
        public IInventory Inventory;
        public IUserData IUserData; 

        public Manager(IInventory inventory, IUserData userData)
        {
            IUserData = userData;
            Inventory = inventory;

            foreach (Part part in inventory.Parts)
            {
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
        public User Authenticate(string username)
        {
            User user = IUserData.GetUser(username);
            return user;
        }
        public WorkflowResponse AddPartToOrder(string input, Order order)
        {
            WorkflowResponse response = new WorkflowResponse();

            int choice;

            bool success = int.TryParse(input, out choice);

            if (!success)
            {
                response.Success = false;
                response.Message = "Sorry, that was an invalid input. Part not added to order.";
            }
            else if (choice > Inventory.Parts.Count || choice < 0)
            {
                response.Success = false;
                response.Message = "Sorry, input not matched to part ID. Part not added to order.";
            }
            else if (Inventory.InvDictionary[choice] == 0)
            {
                response.Success = false;
                response.Message = "Sorry, that part is out of stock. Part not added to order";
            }
            else 
            {

                Part newPart = new Part();

                newPart.Cost = Inventory.Parts.Single(p => p.Id == choice).Cost;
                newPart.Category = Inventory.Parts.Single(p => p.Id == choice).Category;
                newPart.Name = Inventory.Parts.Single(p => p.Id == choice).Name;
                newPart.Id = Inventory.Parts.Single(p => p.Id == choice).Id;

                order.Parts.Add(newPart);

                newPart.SerialNumber = order.Parts.Count;

                response.Success = true;
                response.Message = "Part added successfully.";

            }
            response.Order = order;

            return response;
        }
        public WorkflowResponse DeletePartFromOrder(string input, Order order)
        {
            WorkflowResponse response = new WorkflowResponse();

            int choice;

            bool success = int.TryParse(input, out choice);


            if (!success)
            {
                response.Success = false;
                response.Message = "Sorry, that was an invalid input. Part not deleted from order.";
            }
            else if (!order.Parts.Any(p => choice == p.SerialNumber))
            {
                response.Success = false;
                response.Message = "Sorry, input not matched to a part in your order. Part not deleted from order.";
            }
            else 
            {
                order.Parts.Remove(order.Parts.Single(p => p.SerialNumber == choice));
                response.Success = true;
                response.Message = "Part deleted successfully.";
            }
            response.Order = order;

            return response;
        }
        public IInventory GetInventory()
        {
            return Inventory; 
        }
        public void CancelOrder(User user, Order order)
        {
            foreach (Order o in user.Orders)
            {
                o.IsObsolete = false; //if they were working on a pending order, this resets
                                      //the status of the original
            }
        }
        public WorkflowResponse GetOrderTotal(User user, Order order)
        {
            WorkflowResponse response = new WorkflowResponse();

            foreach (Part part in order.Parts)
            {
                order.Total += part.Cost;
            }

            if(user.Category == UserCategory.Premium)
            {
                decimal total = order.Total * .9m;
                order.Total = decimal.Round(total, 2);
                response.Message = "\n\nThanks for choosing a premium account. A 10% discount was applied to your order.";
            }

            response.OrderTotal = order.Total;

            return response;
        }
        public WorkflowResponse ExecuteOrder(User user, Order order)
        {

            WorkflowResponse response = new WorkflowResponse();

            bool success = CheckInventory(order, response); 

            if (success)
            {
                response.Success = true;
                response.Message = "\n\nOrder confirmation successful. Thank you for your purchase!";

                foreach (Part part in order.Parts)
                {
                    Inventory.InvDictionary[part.Id]--; 
                }

                if (user.Orders.Count == 0)
                {
                    user.Orders.Add(order);
                    order.OrderID = 1;
                    order.DateTime = DateTime.Now;
                }
                else
                {
                    DeleteObsoleteOrder(user);

                    user.Orders.Add(order);
                    Order highestID = user.Orders.OrderByDescending(o => o.OrderID).First();
                    order.OrderID = highestID.OrderID + 1; 
                    order.DateTime = DateTime.Now;

                    IUserData.ReWriteFile();
                    
                    Inventory.ReWriteFile();
                }
            }
            else
            {
                response.Success = false;
                response.Message = "\n\nSorry, one of the items in your cart exceeds our number in stock. Please edit your cart and try again.";
            }

            return response;
        }
        private void DeleteObsoleteOrder(User user)
        {
            if(user.Orders.Any(o => o.IsObsolete == true))
            {
                Order toDelete = user.Orders.Single(o => o.IsObsolete == true);

                foreach (Part part in toDelete.Parts)
                {
                    Inventory.InvDictionary[part.Id]++;
                }

                user.Orders.Remove(toDelete);
            }
        }
        private bool CheckInventory(Order order, WorkflowResponse response)
        {
            //checks number of parts of each ID in the order against inventory 

            var groupedBy = order.Parts.GroupBy(p => p.Id).ToList();

            bool success = false;

            for(int i = 0; i < groupedBy.Count; i++)
            {
                if (groupedBy[i].ToList().Count > Inventory.InvDictionary[groupedBy[i].First().Id])
                {
                    success = false;
                }
                else
                {
                    success = true;
                }
            
            }
            return success;
        }
        public WorkflowResponse SelectOrder(User user, string input)
        {
            int result;

            WorkflowResponse response = new WorkflowResponse();

            Order order = new Order();

            bool success = int.TryParse(input, out result);

            if (!success)
            {
                response.Success = false;
                response.Message = "\nSorry, that was an invalid input. No order selected.";
            }
            else if (!user.Orders.Any(o => result == o.OrderID)) 
            {
                response.Success = false;
                response.Message = "\nSorry, input not matched to order ID. No order selected.";
            }
            else
            {
                response.Success = true;
                response.Message = "\nOrder selected. Press any key to continue.";
                order = user.Orders.Single(o => o.OrderID == result);
            }
            response.Order = order;

            return response;
        }
        public List<Order> GetPendingStatus(User user)
        {
            TimeSpan thirtyDays = new TimeSpan(30, 0, 0, 0);

            foreach(Order order in user.Orders)
            {
                if(DateTime.Now > order.DateTime.Add(thirtyDays))
                {
                    order.PendingStatus = false;
                }
                else if(DateTime.Now < order.DateTime.Add(thirtyDays))
                {
                    order.PendingStatus = true;
                }
                else
                {
                    throw new Exception("Error: Order DateTime not set");
                }
            }
            return user.Orders;
        }
        public void DeleteOrder(User user, Order order)
        {
            user.Orders.Remove(order);

            IUserData.ReWriteFile();

            foreach (Part part in order.Parts)
            {
                Inventory.InvDictionary[part.Id]++;
            }

            Inventory.ReWriteFile();     
        }
        public Order DuplicateOrderForEditing(Order order) 
        {
            Order newOrder = new Order();

            order.IsObsolete = true;
            newOrder.Parts = order.Parts;
            newOrder.OrderID = order.OrderID; 

            return newOrder;
        }
    }
}
