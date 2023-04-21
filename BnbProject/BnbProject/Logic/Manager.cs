using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BnbProject.Data;
using BnbProject.Models;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
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
        public GetListingsResponse GetListings()
        {
            GetListingsResponse response = new GetListingsResponse();

            try
            {
                List<Listing> listings = IDataSource.GetListings();

                response.Success = true;
                response.Message = "Listings retreived successfully";
                response.Listings = listings;

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }
        public ListingResponse AddListing(AddListingTransfer transfer)
        {
            ListingResponse response = new ListingResponse();

            Listing listing = new Listing
            {
                HostId = transfer.HostId,
                Title = transfer.Title,
                Rate = transfer.Rate,
                Location = transfer.Location,
                Description = transfer.Description,
                Amenities = transfer.Amenities,
                Stays = new List<Stay>()
            };


            try
            {
                var completeListing = IDataSource.AddListing(listing);
                response.Success = true;
                response.Message = $"Listing '{listing.Title}' Added successfully.";
                response.Listing = completeListing;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }
        public EditListingResponse EditListing(EditListingTransfer transfer)
        {
            EditListingResponse response = new EditListingResponse();

            try
            {
                Listing listing = IDataSource.GetListingById(transfer.Id);

                listing.Title = transfer.Title;
                listing.Rate = transfer.Rate;
                listing.Location = transfer.Location;
                listing.Description = transfer.Description;
                listing.Amenities = transfer.Amenities;

                IDataSource.EditListing(listing);

                response.Success = true;
                response.Message = $"Listing '{listing.Title}' ID: {listing.Id} Edited successfully.";

                response.User = IDataSource.GetUserById(transfer.HostId);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }
        public AmenitiesResponse GetAmenities()
        {
            AmenitiesResponse response = new AmenitiesResponse();

            try
            {
                List<string> amenities = IDataSource.GetAmenities();
                response.Success = true;
                response.Message = $"{amenities.Count} amenities successfully retrieved.";
                response.Amenities = amenities;

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;
        }
        public ListingResponse GetListingById(int id)
        {
            ListingResponse response = new ListingResponse();

            try
            {
                Listing listing = IDataSource.GetListingById(id);

                if(listing != null)
                {
                    response.Listing = listing;
                    response.Message = $"Listing {id} retrieved.";
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Listing not found.";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;
        }
        public UserResponse GetUserById(int id)
        {
            UserResponse response = new UserResponse();

            try
            {
                UserAccount user = IDataSource.GetUserById(id);

                if(user != null)
                {
                    response.User = user;
                    response.Message = $"User {id} successfully retrieved.";
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "User not found.";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;
        }
        public BookingResponse AddStay(StayTransfer transfer)
        {
            BookingResponse response = new BookingResponse();

            Stay stay = new Stay()
            {
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
                response.Message = $"Booking confirmed from {stay.StartDate} to {stay.EndDate}";

                response.User = IDataSource.GetUserById(transfer.GuestId);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }
        public AddReviewResponse AddReview(Review review)
        {
            AddReviewResponse response = new AddReviewResponse();

            try
            {
                IDataSource.AddReview(review);
                response.Success = true;
                response.Message = $"Review successfully added to stay {review.StayId}.";

                response.User = IDataSource.GetUserByUsername(review.Username);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }
        public UserResponse CreateAccount(CreateAccountRequest request)
        {
            UserResponse response = new UserResponse();

            bool isDuplicate = IDataSource.CheckUsername(request.Username);

            if (isDuplicate)
            {
                response.Success = false;
                response.Message = "Duplicate username found.";
                return response;
            }

            string hashedPass = BC.HashPassword(request.Password);

            UserAccount user = new UserAccount()
            {
                Username = request.Username,
                Password = hashedPass,
                Email = request.Email,
                Listings = new List<Listing>(),
                Favorites = new List<int>(),
                Stays = new List<Stay>()
            };

            try
            {
                var updatedUser = IDataSource.AddUser(user);
                response.Success = true;
                response.Message = $"User {user.Id} added successfully.";
                response.User = updatedUser;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }
        public UserResponse Authenticate(AuthenticationRequest request)
        {
            UserResponse response = new UserResponse();

            try
            {
                UserAccount user = IDataSource.GetUserByUsername(request.Username);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = $"User {request.Username} not found.";
                }
                else
                {
                    bool verifyPass = BC.Verify(request.Password, user.Password);

                    if (verifyPass)
                    {
                        response.Success = true;
                        response.Message = $"Success. User {request.Username} verified.";
                        response.User = user;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Error: Incorrect Password";
                    }
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
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
        public WorkflowResponse RemoveFavorite(UserListing ul)
        {
            WorkflowResponse response = new WorkflowResponse();

            try
            {
                IDataSource.RemoveFavorite(ul);
                response.Success = true;
                response.Message = $"Listing {ul.ListingId} removed from user {ul.UserId} favorites.";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }
        public DeleteListingResponse DeleteListing(int listingId, int userId)
        {
            DeleteListingResponse response = new DeleteListingResponse();

            try
            {
                IDataSource.DeleteListing(listingId);
                response.Success = true;
                response.Message = $"Listing {listingId} successfully deleted.";

                response.User = IDataSource.GetUserById(userId);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }

            return response;
        }
        public WorkflowResponse AddFileToListing(IFormFile file, int listingId)
        {
            WorkflowResponse response = new WorkflowResponse();

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

                try
                {
                    IDataSource.AddFileToListing(fileBytes, listingId);
                    response.Success = true;
                    response.Message = "Image successfully added to listing.";
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                }
            }

            return response;
        }
        public DeletePicsResponse DeletePicsById(int[] ids)
        {
            DeletePicsResponse response = new DeletePicsResponse();

            try
            {
                IDataSource.DeletePicsById(ids);
                response.Success = true;
                response.Message = $"Pictures successfully deleted.";
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
