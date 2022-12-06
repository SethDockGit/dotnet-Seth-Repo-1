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

            TxtInventory txtInventory = new TxtInventory();

            TestUserData testUserData = new TestUserData();

            TxtUserData txtUserData = new TxtUserData();

            IO io = new IO(txtInventory, txtUserData);

            io.MainMenu();
        }
    }
}
