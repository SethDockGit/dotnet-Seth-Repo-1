using System;
using Microsoft.Extensions.Configuration;
using PartOrderingApp.Data;
using PartOrderingApp.UI;


namespace PartOrderingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            string invMode = configuration.GetSection("Settings:InvMode").Value;
            string userMode = configuration.GetSection("Settings:UserMode").Value;

            IInventory inventory = ConfigureInventoryDataMode(invMode);
            IUserData userData = ConfigureUserDataMode(userMode);

            IO io = new IO(inventory, userData);

            io.MainMenu();
        }
        static IUserData ConfigureUserDataMode(string mode)
        {
            switch (mode)
            {
                case "InMemory":
                    return new TestUserData();
                case "TxtData":
                    return new TxtUserData();
                default:
                    throw new Exception("Data mode could not be configured");
            }
        }
        static IInventory ConfigureInventoryDataMode(string mode)
        {
            switch (mode)
            {
                case "InMemory":
                    return new TestInventory();
                case "TxtData":
                    return new TxtInventory();
                default:
                    throw new Exception("Data mode could not be configured");
            }
        }
    }
}
