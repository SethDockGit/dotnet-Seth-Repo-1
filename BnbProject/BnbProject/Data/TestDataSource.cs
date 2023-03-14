using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using BnbProject.Models;

namespace BnbProject.Data
{
    public class TestDataSource : IDataSource
    {
        private List<string> TestAmenities = new List<string>()
        {
            "hot tub", "fireplace", "pool table", "wifi"
        };
        private List<User> TestUsers = new List<User>()
        {
            new User()
            {
                Id = 1,
                Username = "Seth",
                Password = "Password",
                Email = "seth@gmail.com",
                Listings = new List<Listing>(),
                Stays = new List<Stay>()
            },
            new User()
            {
                Id = 2,
                Username = "Bob",
                Password = "Password",
                Email = "brib@gmail.com",
                Listings = new List<Listing>(),
                Stays = new List<Stay>()
            },
        };

        private List<Listing> TestListings = new List<Listing>()
        {
            new Listing()
            {
                Id = 1,
                HostId = 1,
                Title = "Cozy 2BR Cabin",
                Rate = 150,
                Location = "Redwing, MN",
                Description = "Come stay at our gorgeous cabin by the river. Close access to downtown.",
                Amenities = new List<string>(),
                Stays = new List<Stay>()
            },
            new Listing()
            {
                Id = 2,
                HostId = 2,
                Title = "Downtown loft",
                Rate = 180,
                Location = "Minneapolis, MN",
                Description = "Great place to stay to catch a game. Lots of great restaurants nearby.",
                Amenities = new List<string>(),
                Stays = new List<Stay>()
            }
        };
        private List<Stay> TestStays = new List<Stay>()
        {
            new Stay()
            {
                Id = 1,
                GuestId = 1,
                HostId = 2,
                ListingId = 2,
                Review = new Review(),
                StartDate = new DateTime(2023, 3, 25),
                EndDate = new DateTime(2023, 3, 30)
            },
            new Stay()
            {
                Id = 2,
                GuestId = 2,
                HostId = 1,
                ListingId = 1,
                Review = new Review(),
                StartDate = new DateTime(2023, 3, 16),
                EndDate = new DateTime(2023, 3, 18)
            }
        };
        private List<Review> TestReviews = new List<Review>()
        {
            new Review()
            {
                Id = 1,
                Rating = 5,
                Text = "We had a wonderful stay."
            },
            new Review()
            {
                Id = 2,
                Rating = 4,
                Text = "We enjoyed our stay here. Some problems with the wi-fi"
            },
        };
        public TestDataSource()
        {
            BuildRelationships();
        }
        private void BuildRelationships()
        {
            foreach (Listing l in TestListings)
            {
                l.Amenities = TestAmenities;
            }
            //seth owns listing 1, and stayed at listing 2 during stay 1
            TestUsers[0].Listings.Add(TestListings[0]);
            TestUsers[0].Stays.Add(TestStays[0]);
            //TestStays[0].Listing = TestListings[1];
            TestListings[1].Stays.Add(TestStays[0]);
            TestStays[0].HostId = TestUsers[1].Id;
            TestStays[0].GuestId = TestUsers[0].Id;
            TestStays[0].Review = TestReviews[0];

            //Bob owns listing 2, and stayed at listing 1 during stay 2
            TestUsers[1].Listings.Add(TestListings[1]);
            TestUsers[1].Stays.Add(TestStays[1]);
            //TestStays[1].Listing = TestListings[0];
            TestListings[0].Stays.Add(TestStays[1]);
            TestStays[1].HostId = TestUsers[0].Id;
            TestStays[1].GuestId = TestUsers[0].Id;
            TestStays[0].Review = TestReviews[1];
        }
        public List<Listing> GetListings()
        {
            return TestListings;
        }
        public void AddListing(Listing listing)
        {
            TestListings.Add(listing);
        }
        public List<string> GetAmenities()
        {
            return TestAmenities;
        }

    }
}
