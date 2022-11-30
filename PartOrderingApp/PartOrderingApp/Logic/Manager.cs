using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PartOrderingApp.Data;
using PartOrderingApp.Models;

namespace PartOrderingApp.Logic
{
    public class Manager
    {
        private IInventory Inventory;
        private IUserData IUserData;
        public Manager(IInventory inventory, IUserData userData)
        {
            IUserData = userData;
            Inventory = inventory; 
        }
        public User Authenticate(string username)
        {
            //this method will acess userdata to return a user, and you can delete this temporary user
            return new User()
            {
                UserName = username,
                Category = UserCategory.Regular,
            };
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
            else if (Inventory.Inventory[choice] == 0)
            {
                response.Success = false;
                response.Message = "Sorry, that part is out of stock. Part not added to order";
            }
            else //do I need a "if success" clause here?
            {
                order.Parts.Add(Inventory.Parts.Single(p => p.Id == choice && p.IsAvailable == true));
                //should I add "if order.Parts != null just in case?)
                response.Success = true;
                response.Message = "Part added successfully.";
                //Inventory.Inventory[choice]--; 
            }
            response.Order = order;

            return response;
        }
        public WorkflowResponse DeletePartFromOrder(string input, Order order)
        {
            WorkflowResponse response = new WorkflowResponse();

            int choice;

            bool success = int.TryParse(input, out choice);

            var a = 1;

            if (!success)
            {
                response.Success = false;
                response.Message = "Sorry, that was an invalid input. Part not deleted from order.";
            }
            else if (choice > order.Parts.Count || choice < 0)
            {
                response.Success = false;
                response.Message = "Sorry, input not matched to a part in your order. Part not deleted from order.";
            }
            else //do all the parts in the order have a place in order still?
            {
                order.Parts.Remove(order.Parts.Single(p => p.CartID == choice));
                response.Success = true;
                response.Message = "Part deleted successfully.";
            }
            response.Order = order;

            return response;
        }
        public IInventory GetInventory(User user)
        {
            return Inventory; 
        }
        public void CancelOrder(Order order)
        {
            order.Parts.Clear();
        }
        public WorkflowResponse GetOrderTotal(User user, Order order)
        {
            WorkflowResponse response = new WorkflowResponse();

            //the adding logic here is bogus
            foreach (Part part in order.Parts)
            {
                order.Total = +part.Cost;
            }

            if(user.Category == UserCategory.Premium)
            {
                order.Total = order.Total * .9m;
                response.Message = "\n\nThanks for choosing a premium account. A 10% discount was applied to your order.";
            }

            response.OrderTotal = order.Total;

            return response;
        }
        public WorkflowResponse ExecuteOrder(User user, Order order)
        {
            WorkflowResponse response = new WorkflowResponse();

            order.Parts.GroupBy(p => p.Id);

            //am I going to need to make a list of lists here?


            foreach (Part part in order.Parts)
            {
                Inventory.Inventory[part.Id]--;
            }

            user.Orders.Add(order);

            response.Success = true;
            response.Message = "\n\nOrder confirmation successful. Thank you for your purchase!";

            //response.Success = false;
            //response.Message = "\n\nSorry, one of the items in your cart exceeds our number in stock. Please edit your cart and try again.";
            
            //DATETIME NEEDS TO BE SET HERE AS WELL!!!
            return response;
        }
    }
}
