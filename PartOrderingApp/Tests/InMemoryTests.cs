using System;
using System.Collections.Generic;
using PartOrderingApp.Data;
using PartOrderingApp.Models;
using Xunit;

namespace Tests
{
    public class InMemoryTests
    {

        public static List<Part> TestParts = new List<Part>
        {

            //add dictionary and then adjust manager tests so they are testable at the data level
            //(inventory adjustments...) Should the manager defer to data layer to adjust user information
            //as well? You gotta ask about this stuff.

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
                },
                new Part()
                {
                    Id = 3,
                    Name = "Horizon 3050",
                    Category = PartCategory.CPU,
                    Cost = 500.53m
                },
                new Part()
                {
                    Id = 4,
                    Name = "Horizon 2050",
                    Category = PartCategory.CPU,
                    Cost = 200.74m
                },
                new Part()
                {
                    Id = 5,
                    Name = "Aero Case",
                    Category = PartCategory.Case,
                    Cost = 152.81m
                },
                new Part()
                {
                    Id = 6,
                    Name = "Cardboard Box",
                    Category = PartCategory.Case,
                    Cost = 3.33m
                },
                new Part()
                {
                    Id = 7,
                    Name = "AMX 1050",
                    Category = PartCategory.Motherboard,
                    Cost = 329.35m
                },
                new Part()
                {
                    Id = 8,
                    Name = "AMX 2040",
                    Category = PartCategory.Motherboard,
                    Cost = 632.05m
                },
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

        [Fact]
        public void TestUserData_GetUser_CanGetUserOrNull()
        {
            var data = new TestUserData();

            User user = data.GetUser(TestUsers[0].Username);

            User user2 = data.GetUser(TestUsers[1].Username);

            User user3 = data.GetUser(TestUsers[2].Username);

            Assert.Equal(user.Username, TestUsers[0].Username);
            Assert.Equal(user2.Username, TestUsers[1].Username);
            Assert.Null(user3);
        }
    }
}
