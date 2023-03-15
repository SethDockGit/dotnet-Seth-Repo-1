using System;
using System.Collections.Generic;
using System.Text;
using BnbProject.Models;

namespace BnbProject.Data
{
    public interface IDataSource
    {
        public void AddListing(Listing listing);
        List<string> GetAmenities();
        public List<Listing> GetListings();
    }


}
