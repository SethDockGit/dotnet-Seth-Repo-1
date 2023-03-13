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

                if (listings.Count == 0)
                {
                    response.Success = true;
                    response.Message = "There are currently 0 listings.";
                }
                else
                {
                    response.Success = true;
                    response.Message = $"{listings.Count} Listings";
                    response.Listings = listings;
                }
                return response;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message + e.StackTrace;
            }
            return response;
        }

        public WorkflowResponse AddListing(Listing listing)
        {
            WorkflowResponse response = new WorkflowResponse();

            List<Listing> listings = IDataSource.GetListings();

            if (listings.Count == 0)
            {
                listing.Id = 1;
            }
            else
            {
                var highestID = listings.Max(l => l.Id);

                listing.Id = highestID + 1;
            }

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
    }
}
