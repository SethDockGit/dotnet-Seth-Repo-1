using System;
using System.Collections.Generic;
using System.Text;
using BnbProject.Models;

namespace BnbProject.Data
{
    public interface IDataSource
    {
        public void AddListing(Listing listing);
        public void AddStay(Stay stay);
        List<string> GetAmenities();
        public Listing GetListingById(int id);
        public List<Listing> GetListings();
        public UserAccount GetUserById(int id);
        public void RemoveListing(Listing listing);
    }


}
