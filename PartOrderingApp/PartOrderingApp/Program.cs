using System;
using PartOrderingApp.Data;
using PartOrderingApp.UI;


namespace PartOrderingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestInventory inventory = new TestInventory();

            TestUserData userData = new TestUserData();

            IO io = new IO(inventory, userData);

            io.MainMenu();
        }
    }
}
