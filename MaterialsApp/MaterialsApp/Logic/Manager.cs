using MaterialsApp.Data;
using MaterialsApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        public WorkflowResponse CheckResources(string username)
        {
            User user = IDataSource.Authenticate(username);
            WorkflowResponse response = new WorkflowResponse();

            if (user != null)
            {
                User userToCheck = IDataSource.GetUser(user);

                response.Success = true;
                response.User = userToCheck;
            }
            else
            {
                response.Success = false;
                response.Message = $"Error: User {username} was not found. Press any key to return to the main menu.";
            }
            return response;
        }
        public WorkflowResponse DepositResource(User user, ResourceType resource, int depositAmount)
        {
            WorkflowResponse response = new WorkflowResponse();

            if (resource == ResourceType.Invalid)
            {
                response.Success = false;
                response.Message = $"Error: resource type selection was not valid. Press any key to return to the main menu... ";
            }
            else if (depositAmount <= 0)
            {
                response.Success = false;
                response.Message = "Error: Deposit amount must be an integer greater than 0. Press any key to return to the main menu.";
            }
            else
            {
                int newAmount = RouteDeposit(user, resource, depositAmount);
                response.Success = true;
                response.Message = $"\nDeposit successful. {depositAmount} added to {resource}.\n\nNew balance: {newAmount}\n\nPress any key to return to the main menu.";
            }
            return response;
        }
        public WorkflowResponse WithdrawResource(User user, ResourceType resource, int withdrawAmount)
        {
            WorkflowResponse response = new WorkflowResponse();

            if (resource == ResourceType.Invalid)
            {
                response.Success = false;
                response.Message = $"Error: resource type selection was not valid. Press any key to return to the main menu... ";
            }
            else if (withdrawAmount <= 0)
            {
                response.Success = false;
                response.Message = "Error: Withdrawal amount must be an integer greater than 0. Press any key to return to the main menu.";
            }
            else if (GetCurrentUserResourceAmount(resource, user) < withdrawAmount)
            {
                response.Success = false;
                response.Message = $"Error: insufficient balance of {resource}. Press any key to return to the main menu.";
            }
            else
            {
                int newAmount = RouteWithdrawal(user, resource, withdrawAmount);
                response.Success = true;
                response.Message = $"Withdrawal successful.{withdrawAmount} added to {resource}.\\n\\nNew balance: {newAmount}\n\n. Press any key to return to the main menu.";
            }
            return response;
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
            switch (resource)
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
                    throw new Exception("ResourceType case not found");
            }
            return newAmount;
        }
        private int GetCurrentUserResourceAmount(ResourceType resource, User user)
        {
            int amount;

            switch(resource)
            {
                case ResourceType.Wood:
                    amount = user.WoodCount;
                    break;

                case ResourceType.Stone:
                    amount = user.StoneCount;
                    break;

                case ResourceType.Iron:
                    amount = user.IronCount;
                    break;

                case ResourceType.Gold:
                    amount = user.GoldCount;
                    break;

                default:
                    throw new Exception("Error: ResourceType not located");
            }
            return amount;
        }
        internal User Authenticate(string username)
        {
            User user = IDataSource.Authenticate(username);
            return user;
        }
    }
}
