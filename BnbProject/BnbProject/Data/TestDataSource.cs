using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using BnbProject.Models;
using System.Linq;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace BnbProject.Data
{
    public class TestDataSource : IDataSource
    {
        public List<string> TestAmenities = new List<string>()
        {
            "Hot Tub", "Fireplace", "Pool Table", "Wifi", "Dishwasher", "Gas range", "Oven", "Lake access", "Watercraft", "Air conditioning", "Heat"
        };
        public List<UserAccount> TestUsers = new List<UserAccount>()
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
        public List<Listing> TestListings = new List<Listing>()
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
                Stays = new List<Stay>(),
                Pictures = new List<Picture>()
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
                Stays = new List<Stay>(),
                Pictures = new List<Picture>()
            }
        };
        public List<Stay> TestStays = new List<Stay>()
        {
            new Stay()
            {
                Id = 1,
                GuestId = 1,
                HostId = 2,
                ListingId = 2,
                StartDate = new DateTime(2023, 3, 10),
                EndDate = new DateTime(2023, 3, 12)
            },
            new Stay()
            {
                Id = 2,
                GuestId = 2,
                HostId = 1,
                ListingId = 1,
                StartDate = new DateTime(2023, 3, 25),
                EndDate = new DateTime(2023, 3, 30)
            }
        };
        public List<Review> TestReviews = new List<Review>()
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
        public void BuildRelationships()
        {
            foreach (Listing l in TestListings)
            {
                l.Amenities = TestAmenities;
            }

            //seth owns listing 1, and stayed at listing 2 during stay 1
            TestStays[0].HostId = TestUsers[1].Id;
            TestStays[0].GuestId = TestUsers[0].Id;
            TestStays[0].Review = TestReviews[0];   //comment out to see "leave review" option on MyStuff, uncomment to see "show review" on MyStuff
            TestListings[1].Stays.Add(TestStays[0]);
            TestUsers[0].Stays.Add(TestStays[0]);
            TestUsers[0].Listings.Add(TestListings[0]);
            TestUsers[0].Favorites.Add(TestListings[1].Id);

            //Bob owns listing 2, and stayed at listing 1 during stay 2
            TestStays[1].Review = TestReviews[1];
            TestStays[1].HostId = TestUsers[0].Id;
            TestStays[1].GuestId = TestUsers[0].Id;
            TestListings[0].Stays.Add(TestStays[1]);
            TestUsers[1].Listings.Add(TestListings[1]);
            TestUsers[1].Stays.Add(TestStays[1]);
            TestUsers[1].Favorites.Add(TestListings[0].Id);

        }
        public List<Listing> GetListings()
        {
            return TestListings;
        }
        public Listing AddListing(Listing listing)
        {
            var highest = TestListings.Max(l => l.Id);

            listing.Id = highest++;

            TestListings.Add(listing);

            var user = TestUsers.SingleOrDefault(u => u.Id == listing.HostId);

            user.Listings.Add(listing);

            return listing;    
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
        public void EditListing(Listing listing)
        {
            Listing toUpdate = TestListings.SingleOrDefault(l => l.Id == listing.Id);

            toUpdate = listing;
        }
        public void DeleteListing(int listingId)
        {
            Listing toRemove = TestListings.SingleOrDefault(l => l.Id == listingId);

            TestListings.Remove(toRemove);

            UserAccount user = TestUsers.SingleOrDefault(u => u.Id == toRemove.HostId);
            
            user.Listings.Remove(toRemove);
            
            List<Stay> stays = TestStays.Where(s => s.ListingId == listingId).ToList();
            
            foreach(var s in stays)
            {
                TestStays.Remove(s);

                s.Review = null;

                var review = TestReviews.SingleOrDefault(r => r.StayId == s.Id);

                TestReviews.Remove(review);
            
                List<UserAccount> users = TestUsers.Where(u => u.Id == s.GuestId).ToList();
            
                foreach(var u in users)
                {
                    u.Stays.Remove(s);
                }
            }
        }
        public void AddStay(Stay stay)
        {

            var highest = TestStays.Max(s => s.Id);

            stay.Id = highest++;

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
        public List<int> GetUserIds()
        {
            List<int> userIds = new List<int>();
            foreach (var user in TestUsers)
            {
                userIds.Add(user.Id);
            }
            return userIds;
        }
        public UserAccount AddUser(UserAccount user)
        {
            var highest = TestUsers.Max(u => u.Id);

            user.Id = highest++;

            TestUsers.Add(user);

            return user;
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
        public void RemoveFavorite(UserListing ul)
        {
            UserAccount user = TestUsers.SingleOrDefault(u => u.Id == ul.UserId);

            user.Favorites.Remove(ul.ListingId);
        }
        public void AddFileToListing(byte[] file, int listingId)
        {
            throw new NotImplementedException();
        }
        public void DeletePicsById(int[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
