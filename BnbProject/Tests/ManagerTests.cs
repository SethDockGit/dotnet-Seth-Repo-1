using System;
using System.Collections.Generic;
using Xunit;
using BnbProject.Logic;
using BnbProject.Models;
using System.Security.AccessControl;
using BnbProject.Data;
using System.Data;
using BC = BCrypt.Net.BCrypt;

namespace Tests
{
    public class ManagerTests
    {
        //public static List<string> TestAmenities { get; set; } = new List<string>()
        //{
        //    "Hot Tub", "Fireplace", "Pool Table", "Wifi", "Dishwasher", "Gas range", "Oven", "Lake access", "Watercraft", "Air conditioning", "Heat"
        //};
        //public static List<UserAccount> TestUsers { get; set; } = new List<UserAccount>()
        //{
        //    new UserAccount()
        //    {
        //        Id = 1,
        //        Username = "Seth",
        //        Password = BC.HashPassword("Password1!"),
        //        Email = "seth@gmail.com",
        //        Listings = new List<Listing>(),
        //        Favorites = new List<int>(),
        //        Stays = new List<Stay>()
        //    },
        //    new UserAccount()
        //    {
        //        Id = 2,
        //        Username = "Bob",
        //        Password = BC.HashPassword("Bobby1!"),
        //        Email = "bob@gmail.com",
        //        Listings = new List<Listing>(),
        //        Favorites = new List<int>(),
        //        Stays = new List<Stay>()
        //    },
        //};
        //public static List<Listing> TestListings { get; set; } = new List<Listing>()
        //{
        //    new Listing()
        //    {
        //        Id = 1,
        //        HostId = 1,
        //        Title = "Cozy 2BR Cabin",
        //        Rate = 150,
        //        Location = "Redwing, MN",
        //        Description = "Come stay at our gorgeous cabin by the river. Close access to downtown.",
        //        Amenities = new List<string>(),
        //        Stays = new List<Stay>()
        //    },
        //    new Listing()
        //    {
        //        Id = 2,
        //        HostId = 2,
        //        Title = "Downtown loft",
        //        Rate = 180,
        //        Location = "Minneapolis, MN",
        //        Description = "Great place to stay to catch a game. Lots of great restaurants nearby.",
        //        Amenities = new List<string>(),
        //        Stays = new List<Stay>()
        //    }
        //};
        //public static List<Stay> TestStays { get; set; } = new List<Stay>()
        //{
        //    new Stay()
        //    {
        //        Id = 1,
        //        GuestId = 1,
        //        HostId = 2,
        //        ListingId = 2,
        //        //Review = new Review(),
        //        StartDate = new DateTime(2023, 3, 10),
        //        EndDate = new DateTime(2023, 3, 12)
        //    },
        //    new Stay()
        //    {
        //        Id = 2,
        //        GuestId = 2,
        //        HostId = 1,
        //        ListingId = 1,
        //        //Review = new Review(),
        //        StartDate = new DateTime(2023, 3, 25),
        //        EndDate = new DateTime(2023, 3, 30)
        //    }
        //};
        //public static List<Review> TestReviews { get; set; } = new List<Review>()
        //{
        //    new Review()
        //    {
        //        StayId = 1,
        //        Rating = 5,
        //        Text = "We had a wonderful stay.",
        //        Username = "Seth"
        //    },
        //    new Review()
        //    {
        //        StayId = 2,
        //        Rating = 4,
        //        Text = "We enjoyed our stay here. Some problems with the wi-fi",
        //        Username = "Bob"
        //    },
        //};
        //[Fact]
        //public void BuildRelationships()
        //{
        //    foreach (Listing l in TestListings)
        //    {
        //        l.Amenities = TestAmenities;
        //    }
        //
        //    TestStays[0].HostId = TestUsers[1].Id;
        //    TestStays[0].GuestId = TestUsers[0].Id;
        //    TestStays[0].Review = TestReviews[0];  
        //    TestListings[1].Stays.Add(TestStays[0]);
        //    TestUsers[0].Stays.Add(TestStays[0]);
        //    TestUsers[0].Listings.Add(TestListings[0]);
        //    TestUsers[0].Favorites.Add(TestListings[1].Id);
        //
        //    TestStays[1].Review = TestReviews[1];
        //    TestStays[1].HostId = TestUsers[0].Id;
        //    TestStays[1].GuestId = TestUsers[0].Id;
        //    TestListings[0].Stays.Add(TestStays[1]);
        //    TestUsers[1].Listings.Add(TestListings[1]);
        //    TestUsers[1].Stays.Add(TestStays[1]);
        //    TestUsers[1].Favorites.Add(TestListings[0].Id);
        //
        //}
        [Fact]
        public void Manager_GetListings_CanGetListings()
        {

            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            GetListingsResponse response = manager.GetListings();

            Assert.Equal(response.Listings, dataSource.TestListings);
        }
    }
}
