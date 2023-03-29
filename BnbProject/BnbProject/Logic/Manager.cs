using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BnbProject.Data;
using BnbProject.Models;
using BC = BCrypt.Net.BCrypt;

namespace BnbProject.Logic
{
    public class Manager
    {
        public IDataSource IDataSource { get; set; }

        public Manager(IDataSource datasource)
        {
            IDataSource = datasource;
        }
        public WorkflowResponse GetListings()
        {
            WorkflowResponse response = new WorkflowResponse();

            try
            {
                List<Listing> listings = IDataSource.GetListings();

                response.Success = true;
                response.Listings = listings;

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;
        }

        public WorkflowResponse AddListing(ListingTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Listing> listings = IDataSource.GetListings();

            Listing listing = new Listing();

            if (listings.Count == 0)
            {
                listing.Id = 1;
            }
            else
            {
                var highestID = listings.Max(l => l.Id);

                listing.Id = highestID + 1;
            }

            listing.HostId = transfer.HostId;
            listing.Title = transfer.Title;
            listing.Rate = transfer.Rate;
            listing.Location = transfer.Location;
            listing.Description = transfer.Description;
            listing.Amenities = transfer.Amenities;
            listing.Stays = new List<Stay>();

            try
            {
                IDataSource.AddListing(listing);
                response.Success = true;
                response.Message = $"Listing '{listing.Title}' ID: {listing.Id} Added successfully.";
                response.Listing = listing;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;
        }
        public WorkflowResponse EditListing(ListingTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            try
            {
                Listing listing = IDataSource.GetListingById(transfer.Id);

                IDataSource.RemoveListing(listing);

                listing.Title = transfer.Title;
                listing.Rate = transfer.Rate;
                listing.Location = transfer.Location;
                listing.Description = transfer.Description;
                listing.Amenities = transfer.Amenities;

                IDataSource.AddListing(listing);
                response.Success = true;
                response.Message = $"Listing '{listing.Title}' ID: {listing.Id} Edited successfully.";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;


        }
        public WorkflowResponse GetAmenities()
        {
            WorkflowResponse response = new WorkflowResponse();

            try
            {
                List<string> amenities = IDataSource.GetAmenities();
                response.Success = true;
                response.Amenities = amenities;

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;
        }

        public WorkflowResponse GetListingById(int id)
        {
            WorkflowResponse response = new WorkflowResponse();

            try
            {
                Listing listing = IDataSource.GetListingById(id);
                response.Listing = listing;
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;

        }

        public WorkflowResponse GetUserById(int id)
        {
            WorkflowResponse response = new WorkflowResponse();

            try
            {
                UserAccount user = IDataSource.GetUserById(id);
                response.User = user;
                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;
        }

        public WorkflowResponse AddStay(StayTransfer transfer)
        {
            WorkflowResponse response = new WorkflowResponse();

            Listing listing = IDataSource.GetListingById(transfer.ListingId);

            int newId = 0;

            if (listing.Stays.Count == 0)
            {
                newId = 1;
            }
            else
            {
                var highestID = listing.Stays.Max(s => s.Id);

                newId = highestID + 1;
            }

            Stay stay = new Stay()
            {
                Id = newId,
                GuestId = transfer.GuestId,
                HostId = transfer.HostId,
                ListingId = transfer.ListingId,
                Review = new Review(),
                StartDate = transfer.StartDate,
                EndDate = transfer.EndDate
            };

            try
            {
                IDataSource.AddStay(stay);

                response.Success = true;
                response.Message = $"Booking confirmed for {listing.Title} from {stay.StartDate} to {stay.EndDate}";

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;
        }

        public WorkflowResponse AddReview(Review review)
        {
            WorkflowResponse response = new WorkflowResponse();

            try
            {
                IDataSource.AddReview(review);
                response.Success = true;
                response.Message = $"Review successfully added to stay {review.StayId}.";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;

        }

        public WorkflowResponse CreateAccount(CreateAccountRequest request)
        {
            WorkflowResponse response = new WorkflowResponse();

            bool isDuplicate = IDataSource.CheckUsername(request.Username);

            if(isDuplicate)
            {
                response.Success = false;
                response.Message = "Duplicate username found.";
                return response;
            }

            string hashedPass = BC.HashPassword(request.Password);

            List<int> userIds = IDataSource.GetUserIds();

            int newId = 0;

            if (userIds.Count == 0)
            {
                newId = 1;
            }
            else
            {
                var highestID = userIds.Max(u => u);

                newId = highestID + 1;
            }

            UserAccount user = new UserAccount()
            {
                Id = newId,
                Username = request.Username,
                Password = hashedPass,
                Email = request.Email,
                Listings = new List<Listing>(),
                Favorites = new List<int>(),                                                  
                Stays = new List<Stay>()
            };

            try
            {
                IDataSource.AddUser(user);
                response.Success = true;
                response.Message = $"User {user.Id} added successfully.";
                response.User = user;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }

        public WorkflowResponse Authenticate(AuthenticationRequest request)
        {
            WorkflowResponse response = new WorkflowResponse();

            UserAccount user = IDataSource.GetUserByUsername(request.Username);

            if (user == null)
            {
                response.Success = false;
                response.Message = $"User {request.Username} not found.";
                return response;
            }
            else
            {  
                //fix invalid, need hashed data!!
                bool verifyPass = BC.Verify(request.Password, user.Password);
                response.Success = true;
                response.Message = $"Success. User {request.Username} verified.";
                response.User = user;
                return response;
            }
        }

        public WorkflowResponse AddFavorite(UserListing ul)
        {
            WorkflowResponse response = new WorkflowResponse();

            try
            {
                IDataSource.AddFavorite(ul);
                response.Success = true;
                response.Message = $"Listing {ul.ListingId} added to user {ul.UserId} favorites.";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }
    }
}
