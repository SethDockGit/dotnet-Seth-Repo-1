using System;
using System.Collections.Generic;
using Xunit;
using BnbProject.Logic;
using BnbProject.Models;
using System.Security.AccessControl;
using BnbProject.Data;
using System.Data;
using BC = BCrypt.Net.BCrypt;
using System.Reflection;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Tests
{
    public class ManagerTests
    {
        [Fact]
        public void Manager_GetListings_CanGetListings()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            GetListingsResponse response = manager.GetListings();

            Assert.Equal(response.Listings, dataSource.TestListings);
        }
        [Fact]
        public void Manager_GetListings_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            GetListingsResponse response = manager.GetListings();

            string expected = "Listings retreived successfully";

            Assert.Equal(expected, response.Message);

            Assert.True(response.Success);
        }
        [Fact]
        public void Manager_AddListing_CanAddListing()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            AddListingTransfer transfer = new AddListingTransfer()
            {
                HostId = 1,
                Title = "House",
                Rate = 10,
                Location = "Minnesota",
                Description = "Great",
                Amenities = new List<string>()
            };

            ListingResponse response = manager.AddListing(transfer);

            Assert.Equal(response.Listing.HostId, transfer.HostId);
            Assert.Equal(response.Listing.Title, transfer.Title);
            Assert.Equal(response.Listing.Rate, transfer.Rate);
            Assert.Equal(response.Listing.Location, transfer.Location);
            Assert.Equal(response.Listing.Description, transfer.Description);
            Assert.Equal(response.Listing.Amenities, transfer.Amenities);
        }
        [Fact]
        public void Manager_AddListing_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            AddListingTransfer transfer = new AddListingTransfer()
            {
                HostId = 1,
                Title = "House",
                Rate = 10,
                Location = "Minnesota",
                Description = "Great",
                Amenities = new List<string>()
            };

            ListingResponse response = manager.AddListing(transfer);

            string expected = $"Listing '{transfer.Title}' Added successfully.";

            Assert.Equal(expected, response.Message);
            Assert.True(response.Success);
        }
        [Fact]
        public void Manager_EditListing_CanEditListing()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            EditListingTransfer transfer = new EditListingTransfer()
            {
                Id = 2,
                HostId = 2,
                Title = "House",
                Rate = 10,
                Location = "Minnesota",
                Description = "Great",
                Amenities = new List<string>()
            };

            manager.EditListing(transfer);

            Listing listing = dataSource.TestListings.SingleOrDefault(l => l.Id == transfer.Id);

            Assert.Equal(listing.HostId, transfer.HostId);
            Assert.Equal(listing.Title, transfer.Title);
            Assert.Equal(listing.Rate, transfer.Rate);
            Assert.Equal(listing.Location, transfer.Location);
            Assert.Equal(listing.Description, transfer.Description);
            Assert.Equal(listing.Amenities, transfer.Amenities);
        }
        [Fact]
        public void Manager_EditListing_CanReturnUser()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            EditListingTransfer transfer = new EditListingTransfer()
            {
                Id = 2,
                HostId = 2,
                Title = "House",
                Rate = 10,
                Location = "Minnesota",
                Description = "Great",
                Amenities = new List<string>()
            };

            EditListingResponse response = manager.EditListing(transfer);

            UserAccount user = dataSource.TestUsers.SingleOrDefault(u => u.Id == transfer.HostId);

            Assert.Equal(user, response.User);
        }
        [Fact]
        public void Manager_EditListing_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            EditListingTransfer transfer = new EditListingTransfer()
            {
                Id = 2,
                HostId = 2,
                Title = "House",
                Rate = 10,
                Location = "Minnesota",
                Description = "Great",
                Amenities = new List<string>()
            };

            EditListingResponse response = manager.EditListing(transfer);

            string expected = $"Listing '{transfer.Title}' ID: {transfer.Id} Edited successfully.";

            Assert.Equal(expected, response.Message);
            Assert.True(response.Success);
        }
        [Fact]
        public void Manager_GetAmenities_CanGetAmenities()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            AmenitiesResponse response = manager.GetAmenities();

            Assert.Equal(response.Amenities, dataSource.TestAmenities);
        }
        [Fact]
        public void Manager_GetAmenities_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            AmenitiesResponse response = manager.GetAmenities();

            string expected = $"{response.Amenities.Count} amenities successfully retrieved.";

            Assert.Equal(expected, response.Message);
            Assert.True(response.Success);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Manager_GetListingById_CanGetListing(int id)
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            ListingResponse response = manager.GetListingById(id);

            Listing listing = dataSource.TestListings.SingleOrDefault(l => l.Id == id);

            Assert.Equal(listing, response.Listing);
        }
        [Fact]
        public void Manager_GetListingById_FailsWithIncorrectId()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            var id = 3;

            ListingResponse response = manager.GetListingById(id);

            string expected = "Listing not found.";

            Assert.False(response.Success);
            Assert.Equal(expected, response.Message);
        }
        [Fact]
        public void Manager_GetListingById_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            var id = 1;

            ListingResponse response = manager.GetListingById(id);

            string expected = $"Listing {id} retrieved.";

            Assert.Equal(expected, response.Message);
            Assert.True(response.Success);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Manager_GetUserById_CanGetUser(int id)
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            UserResponse response = manager.GetUserById(id);

            UserAccount user = dataSource.TestUsers.SingleOrDefault(l => l.Id == id);

            Assert.Equal(user, response.User);
        }
        [Fact]
        public void Manager_GetUserById_FailsWithIncorrectId()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            var id = 3;

            UserResponse response = manager.GetUserById(id);

            string expected = "User not found.";

            Assert.False(response.Success);
            Assert.Equal(expected, response.Message);
        }
        [Fact]
        public void Manager_GetUserById_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            var id = 1;

            UserResponse response = manager.GetUserById(id);

            string expected = $"User {id} successfully retrieved.";

            Assert.Equal(expected, response.Message);
            Assert.True(response.Success);
        }
        [Fact]
        public void Manager_AddStay_CanAddStay()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            StayTransfer transfer = new StayTransfer()
            {
                GuestId = 1,
                HostId = 2,
                ListingId = 2,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
            };

            BookingResponse response = manager.AddStay(transfer);

            Stay stay = response.User.Stays.Last();

            Assert.Equal(stay.GuestId, transfer.GuestId);
            Assert.Equal(stay.HostId, transfer.HostId);
            Assert.Equal(stay.ListingId, transfer.ListingId);
            Assert.Equal(stay.StartDate, transfer.StartDate);
            Assert.Equal(stay.EndDate, transfer.EndDate);
        }
        [Fact]
        public void Manager_AddStay_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            StayTransfer transfer = new StayTransfer()
            {
                GuestId = 1,
                HostId = 2,
                ListingId = 2,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
            };

            BookingResponse response = manager.AddStay(transfer);

            string expected = $"Booking confirmed from {transfer.StartDate} to {transfer.EndDate}";

            Assert.Equal(expected, response.Message);
            Assert.True(response.Success);
        }
        [Fact]
        public void Manager_AddStay_CanReturnUser()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            StayTransfer transfer = new StayTransfer()
            {
                GuestId = 1,
                HostId = 2,
                ListingId = 2,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
            };

            BookingResponse response = manager.AddStay(transfer);

            UserAccount user = dataSource.TestUsers.SingleOrDefault(u => u.Id == transfer.GuestId);

            Assert.Equal(user, response.User);
        }
        [Fact]
        public void Manager_AddStay_AddsToListing()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            StayTransfer transfer = new StayTransfer()
            {
                GuestId = 1,
                HostId = 2,
                ListingId = 2,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
            };

            manager.AddStay(transfer);

            Listing listing = dataSource.TestListings.SingleOrDefault(l => l.Id == transfer.ListingId);

            Stay stay = listing.Stays.Last();

            Assert.Equal(stay.GuestId, transfer.GuestId);
            Assert.Equal(stay.HostId, transfer.HostId);
            Assert.Equal(stay.ListingId, transfer.ListingId);
            Assert.Equal(stay.StartDate, transfer.StartDate);
            Assert.Equal(stay.EndDate, transfer.EndDate);
        }
        [Fact]
        public void Manager_AddStay_AddsToUser()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            StayTransfer transfer = new StayTransfer()
            {
                GuestId = 1,
                HostId = 2,
                ListingId = 2,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
            };

            manager.AddStay(transfer);

            UserAccount user = dataSource.TestUsers.SingleOrDefault(l => l.Id == transfer.GuestId);

            Stay stay = user.Stays.Last();

            Assert.Equal(stay.GuestId, transfer.GuestId);
            Assert.Equal(stay.HostId, transfer.HostId);
            Assert.Equal(stay.ListingId, transfer.ListingId);
            Assert.Equal(stay.StartDate, transfer.StartDate);
            Assert.Equal(stay.EndDate, transfer.EndDate);
        }
        [Fact]
        public void Manager_AddStay_FailsWithIncorrectGuestId()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            StayTransfer transfer = new StayTransfer()
            {
                GuestId = 99,
                HostId = 2,
                ListingId = 2,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
            };

            BookingResponse response = manager.AddStay(transfer);

            Assert.False(response.Success);
        }
        [Fact]
        public void Manager_AddStay_FailsWithIncorrectListingId()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            StayTransfer transfer = new StayTransfer()
            {
                GuestId = 1,
                HostId = 2,
                ListingId = 99,
                StartDate = new DateTime(),
                EndDate = new DateTime(),
            };

            BookingResponse response = manager.AddStay(transfer);

            Assert.False(response.Success);
        }
        [Fact]
        public void Manager_AddReview_CanAddReview()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            Review expected = new Review()
            {
                StayId = 1,
                Rating = 4,
                Text = "Great",
                Username = "Seth"
            };

            manager.AddReview(expected);

            var stay = dataSource.TestStays.SingleOrDefault(s => s.Id == expected.StayId);

            var actual = stay.Review;

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Manager_AddReview_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            Review review = new Review()
            {
                StayId = 1,
                Rating = 4,
                Text = "Great",
                Username = "Seth"
            };

            AddReviewResponse response = manager.AddReview(review);

            string expected = $"Review successfully added to stay {review.StayId}.";

            Assert.Equal(expected, response.Message);
            Assert.True(response.Success);
        }
        [Fact]
        public void Manager_AddReview_CanReturnUser()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            Review review = new Review()
            {
                StayId = 1,
                Rating = 4,
                Text = "Great",
                Username = "Seth"
            };

            AddReviewResponse response = manager.AddReview(review);

            Stay stay = dataSource.TestStays.SingleOrDefault(s => s.Id == review.StayId);

            UserAccount user = dataSource.TestUsers.SingleOrDefault(u => u.Id == stay.GuestId);

            Assert.Equal(user, response.User);
        }
        [Fact]
        public void Manager_AddReview_FailsWithIncorrectStayId()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            Review expected = new Review()
            {
                StayId = 99,
                Rating = 4,
                Text = "Great",
                Username = "Seth"
            };

            AddReviewResponse response = manager.AddReview(expected);

            Assert.False(response.Success);
        }
        [Fact]
        public void Manager_CreateAccount_CanCreateUserAccount()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            CreateAccountRequest request = new CreateAccountRequest()
            {
                Username = "Dale",
                Email = "Dale@dale.com",
                Password = "Dale123!"
            };

            var highest = dataSource.TestUsers.Max(u => u.Id);

            highest = highest++;

            UserResponse response = manager.CreateAccount(request);

            Assert.Equal(response.User.Username, request.Username);
            Assert.Equal(response.User.Email, request.Email);
            Assert.Equal(response.User.Id, highest);
        }
        [Fact]
        public void Manager_CreateAccount_HashesPassword()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            CreateAccountRequest request = new CreateAccountRequest()
            {
                Username = "Dale",
                Email = "Dale@dale.com",
                Password = "Dale123!"
            };

            UserResponse response = manager.CreateAccount(request);

            Assert.True(BC.Verify(request.Password, response.User.Password));
        }
        [Fact]
        public void Manager_CreateAccount_FailsIfDuplicateUserFound()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            CreateAccountRequest request = new CreateAccountRequest()
            {
                Username = "Seth",
                Email = "Seth@Seth.com",
                Password = "Seth123!"
            };

            UserResponse response = manager.CreateAccount(request);

            string expected = "Duplicate username found.";

            Assert.False(response.Success);
            Assert.Equal(expected, response.Message);
        }
        [Fact]
        public void Manager_CreateAccount_GetsCorrectMessage()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            CreateAccountRequest request = new CreateAccountRequest()
            {
                Username = "Dale",
                Email = "Dale@dale.com",
                Password = "Dale123!"
            };

            UserResponse response = manager.CreateAccount(request);

            string expected = $"User {response.User.Id} added successfully.";

            Assert.Equal(expected, response.Message);
        }
        [Fact]
        public void Manager_Authenticate_CanAuthenticateUser()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            AuthenticationRequest request = new AuthenticationRequest()
            {
                Password = "Password1!",
                Username = "Seth"
            };

            UserResponse response = manager.Authenticate(request);

            UserAccount user = dataSource.TestUsers.SingleOrDefault(u => u.Username == request.Username);

            Assert.Equal(user, response.User);
        }
        [Fact]
        public void Manager_Authenticate_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            AuthenticationRequest request = new AuthenticationRequest()
            {
                Password = "Password1!",
                Username = "Seth"
            };

            UserResponse response = manager.Authenticate(request);

            string expected = $"Success. User {request.Username} verified.";

            Assert.True(response.Success);
            Assert.Equal(expected, response.Message);
        }
        [Fact]
        public void Manager_Authenticate_FailIfIncorrectUsername()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            AuthenticationRequest request = new AuthenticationRequest()
            {
                Password = "Password1!",
                Username = "Dave"
            };

            UserResponse response = manager.Authenticate(request);

            string expected = $"User {request.Username} not found.";

            Assert.False(response.Success);
            Assert.Equal(expected, response.Message);
        }
        [Fact]
        public void Manager_Authenticate_FailIfIncorrectPassword()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            AuthenticationRequest request = new AuthenticationRequest()
            {
                Password = "sbsdfbsfb",
                Username = "Seth"
            };

            UserResponse response = manager.Authenticate(request);

            string expected = "Error: Incorrect Password";

            Assert.False(response.Success);
            Assert.Equal(expected, response.Message);
        }
        [Fact]
        public void Manager_AddFavorite_CanAddFavorite()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            UserListing ul = new UserListing()
            {
                UserId = 1,
                ListingId = 1
            };

            manager.AddFavorite(ul);

            UserAccount user = dataSource.TestUsers.SingleOrDefault(u => u.Id == ul.UserId);

            var favorite = user.Favorites.Last();

            Assert.Equal(ul.ListingId, favorite);   
        }
        [Fact]
        public void Manager_AddFavorite_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            UserListing ul = new UserListing()
            {
                UserId = 1,
                ListingId = 1
            };

            WorkflowResponse response = manager.AddFavorite(ul);

            string expected = $"Listing {ul.ListingId} added to user {ul.UserId} favorites.";

            Assert.True(response.Success);
            Assert.Equal(expected, response.Message);
        }
        [Fact]
        public void Manager_RemoveFavorite_CanRemoveFavorite()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            UserListing ul = new UserListing()
            {
                UserId = 1,
                ListingId = 2
            };

            manager.RemoveFavorite(ul);

            UserAccount user = dataSource.TestUsers.SingleOrDefault(u => u.Id == ul.UserId);

            Assert.DoesNotContain(ul.ListingId, user.Favorites);
        }
        [Fact]
        public void Manager_RemoveFavorite_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            UserListing ul = new UserListing()
            {
                UserId = 1,
                ListingId = 2
            };

            WorkflowResponse response = manager.RemoveFavorite(ul);

            string expected = $"Listing {ul.ListingId} removed from user {ul.UserId} favorites.";

            Assert.Equal(expected, response.Message);
            Assert.True(response.Success);
        }
        [Fact]
        public void Manager_DeleteListing_CanDeleteListing()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            UserListing ul = new UserListing()
            {
                UserId = 1,
                ListingId = 1
            };

            var listing = dataSource.TestListings.SingleOrDefault(l => l.Id == ul.ListingId);

            var user = dataSource.TestUsers.SingleOrDefault(u => u.Id == ul.UserId);

            manager.DeleteListing(ul.ListingId, ul.UserId);

            Assert.DoesNotContain(listing, dataSource.TestListings);
            Assert.DoesNotContain(listing, user.Listings);
        }
        [Fact]
        public void Manager_DeleteListing_DeletesStaysOnListing()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            UserListing ul = new UserListing()
            {
                UserId = 1,
                ListingId = 1
            };

            var listing = dataSource.TestListings.SingleOrDefault(l => l.Id == ul.ListingId);

            var stay = listing.Stays[0]; //single stay is retrieved because TestData only has one stay on this listing.

            var user = dataSource.TestUsers.SingleOrDefault(u => u.Id == ul.UserId);

            manager.DeleteListing(ul.ListingId, ul.UserId);

            Assert.DoesNotContain(stay, dataSource.TestStays);
            Assert.DoesNotContain(stay, user.Stays);
        }
        [Fact]
        public void Manager_DeleteListing_DeletesReviewFromStays()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            UserListing ul = new UserListing()
            {
                UserId = 1,
                ListingId = 1
            };

            var listing = dataSource.TestListings.SingleOrDefault(l => l.Id == ul.ListingId);

            var stay = listing.Stays[0];

            var review = stay.Review;

            manager.DeleteListing(ul.ListingId, ul.UserId);

            Assert.DoesNotContain(review, dataSource.TestReviews);
        }
        [Fact]
        public void Manager_DeleteListing_GetsCorrectResponse()
        {
            TestDataSource dataSource = new TestDataSource();

            Manager manager = new Manager(dataSource);

            UserListing ul = new UserListing()
            {
                UserId = 1,
                ListingId = 1
            };

            DeleteListingResponse response = manager.DeleteListing(ul.ListingId, ul.UserId);

            string expected = $"Listing {ul.ListingId} successfully deleted.";

            Assert.Equal(expected, response.Message);
            Assert.True(response.Success);
        }
    }
}
