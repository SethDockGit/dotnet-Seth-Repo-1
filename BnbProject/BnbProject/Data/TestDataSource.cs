﻿using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using BnbProject.Models;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

namespace BnbProject.Data
{
    public class TestDataSource : IDataSource
    {
        private List<string> TestAmenities = new List<string>()
        {
            "Hot Tub", "Fireplace", "Pool Table", "Wifi", "Dishwasher", "Gas range", "Oven", "Lake access", "Watercraft", "Air conditioning", "Heat"
        };
        private List<UserAccount> TestUsers = new List<UserAccount>()
        {
            new UserAccount()
            {
                Id = 1,
                Username = "Seth",
                Password = BC.HashPassword("Password1!"),
                Email = "seth@gmail.com",
                Listings = new List<Listing>(),
                Favorites = new List<int>(),
                Stays = new List<Stay>()
            },
            new UserAccount()
            {
                Id = 2,
                Username = "Bob",
                Password = BC.HashPassword("Bobby1!"),
                Email = "bob@gmail.com",
                Listings = new List<Listing>(),
                Favorites = new List<int>(),
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
                //Review = new Review(),
                StartDate = new DateTime(2023, 3, 10),
                EndDate = new DateTime(2023, 3, 12)
            },
            new Stay()
            {
                Id = 2,
                GuestId = 2,
                HostId = 1,
                ListingId = 1,
                //Review = new Review(),
                StartDate = new DateTime(2023, 3, 25),
                EndDate = new DateTime(2023, 3, 30)
            }
        };
        private List<Review> TestReviews = new List<Review>()
        {
            new Review()
            {
                StayId = 1,
                Rating = 5,
                Text = "We had a wonderful stay.",
                Username = "Seth"
            },
            new Review()
            {
                StayId = 2, 
                Rating = 4,
                Text = "We enjoyed our stay here. Some problems with the wi-fi",
                Username = "Bob"
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
            //Add sequentially!!
            //seth owns listing 1, and stayed at listing 2 during stay 1
            TestStays[0].HostId = TestUsers[1].Id;
            TestStays[0].GuestId = TestUsers[0].Id;
            //TestStays[0].Review = TestReviews[0];   //comment out to see "leave review" option on MyStuff, uncomment to see "show review" on MyStuff
            TestListings[1].Stays.Add(TestStays[0]);
            TestUsers[0].Stays.Add(TestStays[0]);
            TestUsers[0].Listings.Add(TestListings[0]);
            TestUsers[0].Favorites.Add(TestListings[1].Id);
            //TestStays[0].Listing = TestListings[1];


            //Bob owns listing 2, and stayed at listing 1 during stay 2
            TestStays[1].Review = TestReviews[1];
            TestStays[1].HostId = TestUsers[0].Id;
            TestStays[1].GuestId = TestUsers[0].Id;
            TestListings[0].Stays.Add(TestStays[1]);
            TestUsers[1].Listings.Add(TestListings[1]);
            TestUsers[1].Stays.Add(TestStays[1]);
            TestUsers[1].Favorites.Add(TestListings[0].Id);
            //TestStays[1].Listing = TestListings[0];
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
        public Listing GetListingById(int id)
        {
            return TestListings.SingleOrDefault(l => l.Id == id);
           
        }
        public UserAccount GetUserById(int id)
        {
            return TestUsers.SingleOrDefault(u => u.Id == id);
            
        }
        public void RemoveListing(Listing listing)
        {
            TestListings.Remove(listing);
        }
        public void AddStay(Stay stay)
        {
            TestStays.Add(stay);

            UserAccount user = TestUsers.SingleOrDefault(u => u.Id == stay.GuestId);

            user.Stays.Add(stay);

            Listing listing = TestListings.SingleOrDefault(l => l.Id == stay.ListingId);

            listing.Stays.Add(stay);
        }
        public void AddReview(Review review)
        {
            Stay stay = TestStays.SingleOrDefault(s => s.Id == review.StayId);

            stay.Review = review;
        }
        public bool CheckUsername(string username)
        {
            bool isDuplicate = TestUsers.Any(u => u.Username == username);

            return isDuplicate;
        }
        public List<UserAccount> GetUsers()
        {
            return TestUsers;
        }
        public void AddUser(UserAccount user)
        {
            TestUsers.Add(user);
        }
        public UserAccount GetUserByUsername(string username)
        {
            return TestUsers.SingleOrDefault(u => u.Username == username);
        }
        public void AddFavorite(UserListing ul)
        {
            UserAccount user = TestUsers.SingleOrDefault(u => u.Id == ul.UserId);

            user.Favorites.Add(ul.ListingId);
        }
    }
}