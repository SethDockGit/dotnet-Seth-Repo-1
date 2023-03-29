using System;
using System.Collections.Generic;
using System.Text;
using BnbProject.Models;

namespace BnbProject.Data
{
    public interface IDataSource
    {
        public void AddFavorite(UserListing ul);
        public void AddListing(Listing listing);
        public void AddReview(Review review);
        public void AddStay(Stay stay);
        public void AddUser(UserAccount user);
        public bool CheckUsername(string username);
        public List<string> GetAmenities();
        public Listing GetListingById(int id);
        public List<Listing> GetListings();
        public UserAccount GetUserById(int id);
        public UserAccount GetUserByUsername(string username);
        public List<int> GetUserIds();
        public void RemoveListing(Listing listing);
    }


}
