using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BnbProject.Data;
using BnbProject.Models;

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

        public WorkflowResponse AddListing(NewListingTransfer transfer)
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
    }
}
