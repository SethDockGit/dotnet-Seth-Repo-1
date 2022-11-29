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
        private IInventory IInventory;
        private IUserData IUserData;
        public Manager(IInventory inventory, IUserData userData)
        {
            IUserData = userData;
            IInventory = inventory; 
        }
        public User Authenticate(string username)
        {
            //this method will acess userdata to return a user, and you can delete this temporary user
            return new User()
            {
                UserName = username,
                Category = UserCategory.Premium
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
            else if (choice > IInventory.Parts.Count)
            {
                response.Success = false;
                response.Message = "Sorry, input not matched to part ID. Part not added to order.";
            }
            else if (IInventory.Inventory[choice] == 0)
            {
                response.Success = false;
                response.Message = "Sorry, that part is out of stock. Part not added to order";
            }
            else
            {
                order.Parts.Add(IInventory.Parts.Single(p => p.Id == choice && p.IsAvailable == true));

                order.Parts.Last().PlaceInOrder = order.Parts.Count();

                if (order.Parts != null)
                {
                    response.Success = true;
                    response.Message = "Part added successfully.";
                    IInventory.Inventory[choice]--; 
                }
            }
            response.Order = order;

            return response;
        }
        public IInventory GetInventory()
        {
            return IInventory; 
        }
    }
}
