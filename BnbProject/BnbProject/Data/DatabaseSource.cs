using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using BnbProject.Models;

namespace BnbProject.Data
{
    public class DatabaseSource : IDataSource
    {

        private string ConnectionString =
        "Server=localhost;Database=StudentManagement;User Id=sa;Password=L'audace1!";

        public List<Listing> GetListings()
        {
            List<Listing> listings = new List<Listing>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM LISTING";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var listing = new Listing();
                        listing.Id = (int)dr["ListingId"];
                        listing.Title = (string)dr["Title"];
                        listing.Description = (string)dr["Description"];
                        listing.Location = (string)dr["Location"];
                        listing.Rate = (decimal)dr["Rate"];

                        listings.Add(listing);
                    }
                }
            }
            return listings;
        }
        public void AddListing(Listing listing)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Listing VALUES (@ListingId, @HostId, @Title, @Description, @Location, @Rate)";

                cmd.Parameters.AddWithValue("@ListingId", listing.Id);
                cmd.Parameters.AddWithValue("@HostId", listing.HostId);
                cmd.Parameters.AddWithValue("@Title", listing.Title);
                cmd.Parameters.AddWithValue("@Description", listing.Description);
                cmd.Parameters.AddWithValue("@Location", listing.Location);
                cmd.Parameters.AddWithValue("@Rate", listing.Rate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<string> GetAmenities()
        {

            List<string> amenities = new List<string>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Amenity";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var amenity = (string)dr["AmenityName"];

                        amenities.Add(amenity);
                    }
                }
            }
            return amenities;
        }
        public Listing GetListingById(int id)
        {
            Listing listing = new Listing();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"SELECT * FROM Listing WHERE ListingId={id}";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listing.Id = (int)dr["ListingId"];
                        listing.HostId = (int)dr["HostId"];
                        listing.Title = (string)dr["Title"];
                        listing.Description = (string)dr["Description"];
                        listing.Location = (string)dr["Location"];
                        listing.Rate = (decimal)dr["Rate"];
                    }
                }

                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = $"SELECT * FROM Stay WHERE ListingId={listing.Id}";

                conn.Open();
                using (SqlDataReader dr = cmd2.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Stay stay = new Stay();
                        stay.Id = (int)dr["StayId"];
                        stay.ListingId = (int)dr["ListingId"];
                        stay.GuestId = (int)dr["GuestId"];

                        listing.Stays.Add(stay);
                    }
                }

                foreach (Stay s in listing.Stays)
                {
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Connection = conn;
                    cmd3.CommandText = $"SELECT * FROM Review WHERE ReviewId={s.Id}";  //save review accordingly!!!

                    conn.Open();
                
                    using (SqlDataReader dr = cmd3.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Review review = new Review();
                            review.StayId = s.Id;
                            review.Rating = (int)dr["Rating"];
                            review.Text = (string)dr["text"];

                            s.Review = review;
                        }
                    }
                }

                //now you need to do one more with the ListingAmenity bridge table.

            }
            return listing;
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
