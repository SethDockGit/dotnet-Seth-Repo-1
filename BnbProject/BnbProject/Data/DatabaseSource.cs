using System;
using System.Collections.Generic;
using System.Text;
using BnbProject.Models;

namespace BnbProject.Data
{
    public class DatabaseSource : IDataSource
    {


        public List<Listing> GetListings()
        {
            throw new NotImplementedException();
        }
        public void AddListing(Listing listing)
        {
            throw new NotImplementedException();
        }
        public List<string> GetAmenities()
        {
            throw new NotImplementedException();
        }
        public Listing GetListingById(int id)
        {
            throw new NotImplementedException();
        }
        public UserAccount GetUserById(int id)
        {
            throw new NotImplementedException();
        }
        public void RemoveListing(Listing listing)
        {
            throw new NotImplementedException();
        }
        public void AddStay(Stay stay)
        {
            throw new NotImplementedException();
        }
        public void AddReview(Review review)
        {
            throw new NotImplementedException();
        }
        public bool CheckUsername(string username)
        {
            throw new NotImplementedException();
        }
        public List<UserAccount> GetUsers()
        {
            throw new NotImplementedException();
        }
        public void AddUser(UserAccount user)
        {
            throw new NotImplementedException();
        }
        public UserAccount GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
        public void AddFavorite(UserListing ul)
        {
            throw new NotImplementedException();
        }
    }


}
