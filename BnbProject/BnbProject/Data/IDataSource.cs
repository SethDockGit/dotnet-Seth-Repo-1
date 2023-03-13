using System;
using System.Collections.Generic;
using System.Text;
using BnbProject.Models;

namespace BnbProject.Data
{
    public interface IDataSource
    {
        public void AddListing(Listing listing);
        public List<Listing> GetListings();
    }


}
