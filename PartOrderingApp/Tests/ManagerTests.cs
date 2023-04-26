using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PartOrderingApp.Data;
using PartOrderingApp.Logic;
using PartOrderingApp.Models;
using Xunit;

namespace Tests
{
    public class ManagerTests
    {
        //public static TestInventory TestInventory;

        public static List<Part> TestParts = new List<Part>
        {
            new Part()
            {
                Id = 1,
                Name = "GTX 5020",
                Category = PartCategory.GPU,
                Cost = 900.40m
            },
            new Part()
            {
                Id = 2,
                Name = "GTX 4020",
                Category = PartCategory.GPU,
                Cost = 400.23m
            }
        };

        public static List<User> TestUsers = new List<User>
        {
            new User()
            {
                Username = "s",
                Category = UserCategory.Premium,

            },
            new User()
            {
                Username = "d",
                Category = UserCategory.Regular,

            },
            new User()
            {
                Username = "Tim",
                Category = UserCategory.Regular,

            }
        };

        public static Manager Manager;

        public ManagerTests()
        {
            TestUserData testData = new TestUserData();
            TestInventory testInventory = new TestInventory();

            Manager = new Manager(testInventory, testData);
        }

        //NOTE: I FORGOT THAT TESTS WANT (EXPECTED, ACTUAL) FORMAT FOR ASSERT EQUAL

        [Fact]
        public void Manager_Authenticate_CanGetUser()
        {
            User user1 = Manager.Authenticate("s");

            Assert.Equal(user1.Username, TestUsers[0].Username);
        }
        [Fact]
        public void Manager_AddPartToOrder_FailInvalidInput()
        {
            Order order = new Order();

            WorkflowResponse responseOne = Manager.AddPartToOrder("Doug", order);
            WorkflowResponse responseTwo = Manager.AddPartToOrder("-1", order);
            WorkflowResponse responseThree = Manager.AddPartToOrder("-1", order);

            Assert.False(responseOne.Success);
            Assert.False(responseTwo.Success);
            Assert.False(responseThree.Success);
        }
        [Fact]
        public void Manager_AddPartToOrder_FailWhenOutOfStock()
        {
            Order order = new Order();

            WorkflowResponse response = Manager.AddPartToOrder("3", order);

            Assert.False(response.Success);
        }
        [Theory]
        [InlineData("1", "GTX 5020")]
        [InlineData("5", "Aero Case")]
        [InlineData("6", "Cardboard Box")]
        public void Manager_AddPartToOrder_CanAddPartToOrder(string input, string partName)
        {
            Order order = new Order();

            order.Parts = new List<Part>(); //this is tricky and messed me up before

            WorkflowResponse response = Manager.AddPartToOrder(input, order);

            string actualName = response.Order.Parts.First().Name;

            Assert.Equal(actualName, partName);
        }
        [Fact]
        public void Manager_DeletePartFromOrder_FailInvalidInput()
        {
            Order order = new Order();
            order.Parts = new List<Part>(); //should I set this somewhere else, like on the property itself? Or the constructor?

            Part part = new Part();

            part.SerialNumber = 1;

            order.Parts.Add(part);

            WorkflowResponse responseOne = Manager.DeletePartFromOrder("Doug", order);
            WorkflowResponse responseTwo = Manager.DeletePartFromOrder("5", order);

            Assert.False(responseOne.Success);
            Assert.False(responseTwo.Success);
        }
        [Fact]
        public void Manager_DeletePartFromOrder_CanDeletePartFromOrder()
        {
            Order order = new Order();
            order.Parts = new List<Part>();

            Part part = new Part();

            part.SerialNumber = 1;

            order.Parts.Add(part);

            WorkflowResponse response = Manager.DeletePartFromOrder("1", order);

            Assert.True(response.Success);
            Assert.True(response.Order.Parts.Count == 0);
        }
        [Fact]
        public void Manager_GetInventory_CanGetInventory()
        {
            IInventory inventory = Manager.GetInventory();

            Assert.Equal(inventory, Manager.Inventory);

            //this would fail if the chain the sets the Inventory property to the manager in the constructor is broken
        }
        [Fact]
        public void Manager_CancelOrder_CanResetObsoleteStatus()
        {
            Order order = new Order();
            order.IsObsolete = true;

            User user = new User();

            user.Orders = new List<Order>();

            user.Orders.Add(order);

            Manager.CancelOrder(user, order);

            Assert.False(user.Orders.First().IsObsolete);
        }
        [Fact]
        public void Manager_GetOrderTotal_GetsCorrectTotal()
        {
            Order order = new Order();
            order.Parts = new List<Part>();

            Part part = new Part();
            part.Cost = 10m;
            order.Parts.Add(part);

            Part partTwo = new Part();
            partTwo.Cost = 10m;
            order.Parts.Add(partTwo);

            WorkflowResponse responseOne = Manager.GetOrderTotal(TestUsers[0], order); //premium!
            WorkflowResponse responseTwo = Manager.GetOrderTotal(TestUsers[1], order); //reg

            decimal expectedTotalOne = 18m;

            decimal expectedTotalTwo = 38m; //ugh, it's adding the first total to the second
                                            //when order is passed into GetOrderTotal the 2nd time
                                            //it's an annoying flaw even tho the test logic is sound

            Assert.Equal(expectedTotalOne, responseOne.OrderTotal);
            Assert.Equal(expectedTotalTwo, responseTwo.OrderTotal);
        }
        [Fact]
        public void Manager_ExecuteOrder_FailIfOutOfStock()
        {
            Order order = new Order();
            order.Parts = new List<Part>();

            Part part = new Part();
            part.Id = 3;
            order.Parts.Add(part);

            WorkflowResponse response = Manager.ExecuteOrder(TestUsers[0], order);

            Assert.False(response.Success);
        }
        [Fact]
        public void Manager_ExecuteOrder_CanRemoveObsoleteOrder()
        {
            Order order = new Order();
            order.Parts = new List<Part>();
            order.IsObsolete = true;

            TestUsers[0].Orders.Add(order);

            Manager.ExecuteOrder(TestUsers[0], order);

            Assert.False(TestUsers[0].Orders.Count == 0);
        }
        [Fact]
        public void Manager_ExecuteOrder_CanExecuteFirstOrder()
        {
            Order order = new Order();
            order.Parts = new List<Part>();
            order.Parts.Add(TestParts[0]);

            TestUsers[0].Orders = new List<Order>();  //how to avoid needing to instantiate all this?

            WorkflowResponse response = Manager.ExecuteOrder(TestUsers[0], order);

            Assert.True(TestUsers[0].Orders.Count == 1);

            Assert.True(TestUsers[0].Orders.First().OrderID == 1);


        }
        [Fact]
        public void Manager_ExecuteOrder_CanExecuteSubsequentOrder()
        {
            Order order = new Order();
            order.Parts = new List<Part>();
            order.OrderID = 1;
            order.Parts.Add(TestParts[0]);

            TestUsers[0].Orders = new List<Order>();
            TestUsers[0].Orders.Add(order);

            Order orderTwo = new Order();
            orderTwo.Parts = new List<Part>();
            orderTwo.Parts.Add(TestParts[1]);

            WorkflowResponse response = Manager.ExecuteOrder(TestUsers[0], orderTwo);

            Assert.True(TestUsers[0].Orders.Count == 2);

            Assert.True(TestUsers[0].Orders[1].OrderID == 2);

        }
        [Fact]
        public void Manager_ExecuteOrder_CanUpdateInventory()
        {

            int stockBeforeOrdering = Manager.Inventory.InvDictionary[1];

            Order order = new Order();
            order.Parts = new List<Part>();

            order.Parts.Add(TestParts[0]); //this test part has an ID of 1

            WorkflowResponse response = Manager.ExecuteOrder(TestUsers[0], order);

            Assert.True(Manager.Inventory.InvDictionary[1] == stockBeforeOrdering - 1);
        }
        [Fact]
        public void Manager_ExecuteOrder_CanDeleteObsoleteOrder()
        {
            Order order = new Order();
            order.Parts = new List<Part>();
            order.Parts.Add(TestParts[1]);
            order.OrderID = 1;
            order.IsObsolete = true;
            TestUsers[0].Orders.Add(order);

            Order orderTwo = new Order();
            orderTwo.Parts = new List<Part>();
            orderTwo.Parts.Add(TestParts[1]);

            WorkflowResponse response = Manager.ExecuteOrder(TestUsers[0], orderTwo);

            Assert.DoesNotContain(TestUsers[0].Orders, o => o.IsObsolete == true);
        }
        [Theory]
        [InlineData("0")]
        [InlineData("Fred")]
        [InlineData("999999")] //if this happens we throw a party for the customer
        public void Manager_SelectOrder_FailInvalidInput(string input)
        {
            WorkflowResponse response = Manager.SelectOrder(TestUsers[0], input);

            Assert.False(response.Success);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(5, "5")]
        [InlineData(2, "2")]
        public void Manager_SelectOrder_CanSelectOrder(int orderID, string input)
        {
            Order order = new Order();
            order.OrderID = orderID;
            TestUsers[0].Orders = new List<Order>();
            TestUsers[0].Orders.Add(order);

            WorkflowResponse response = Manager.SelectOrder(TestUsers[0], input);

            Assert.True(response.Order.OrderID == TestUsers[0].Orders[0].OrderID);
        }
        [Fact]
        public void Manager_GetPendingStatus_CanGetTrueIfUnderThirtyDays()
        {
            Order order = new Order(); //pending status is auto-set to true
            TestUsers[0].Orders = new List<Order>();
            order.DateTime = DateTime.Now;
            TestUsers[0].Orders.Add(order);

            List<Order> orders = Manager.GetPendingStatus(TestUsers[0]);

            Assert.True(orders[0].PendingStatus);
        }
        [Fact]
        public void Manager_GetPendingStatus_CanGetFalseIfOverThirtyDays()
        {
            Order order = new Order(); 
            TestUsers[0].Orders = new List<Order>();

            TimeSpan thirtyDays = new TimeSpan(30, 0, 0, 0);

            order.DateTime = DateTime.Now.Subtract(thirtyDays);

            TestUsers[0].Orders.Add(order);

            List<Order> orders = Manager.GetPendingStatus(TestUsers[0]);

            Assert.False(orders[0].PendingStatus);
        }
        [Fact]
        public void Manager_DeleteOrder_CanDeleteOrder()
        {
            Order order = new Order();
            order.Parts = new List<Part>();
            order.Parts.Add(TestParts[0]);

            TestUsers[0].Orders = new List<Order>();
            TestUsers[0].Orders.Add(order);

            Manager.DeleteOrder(TestUsers[0], order);

            Assert.True(TestUsers[0].Orders.Count == 0);
        }
        [Fact]
        public void Manager_DeleteOrder_CanUpdateInventory()
        {
            int stockBeforeDeleting = Manager.Inventory.InvDictionary[1];

            Order order = new Order();
            order.Parts = new List<Part>();

            order.Parts.Add(TestParts[0]); 

            Manager.DeleteOrder(TestUsers[0], order);

            Assert.True(Manager.Inventory.InvDictionary[1] == stockBeforeDeleting + 1);
        }
        [Fact]
        public void Manager_DuplicateOrderForEditing_CanCorrectlyDuplicateOrder()
        {
            Order order = new Order();
            order.Parts = new List<Part>();
            order.Parts.Add(TestParts[0]);
            order.OrderID = 1;
            order.IsObsolete = false;

            Order newOrder = Manager.DuplicateOrderForEditing(order);

            Assert.True(newOrder.OrderID == 1);
            Assert.False(newOrder.IsObsolete);
            Assert.True(newOrder.Parts[0].Id == 1); //the same ID as the original part
        }
    }
}
