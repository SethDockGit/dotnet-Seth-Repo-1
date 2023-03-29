using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
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

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM Listing"
                };

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var listing = new Listing
                        {
                            Id = (int)dr["ListingId"],
                            Title = (string)dr["Title"],
                            Description = (string)dr["Description"],
                            Location = (string)dr["Location"],
                            Rate = (decimal)dr["Rate"]
                        };

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

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO Listing VALUES (@ListingId, @HostId, @Title, @Description, @Location, @Rate)"
                };

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

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM Amenity"
                };

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

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"SELECT * FROM Listing WHERE ListingId=@ListingId" +
                    $"INNER JOIN Stay on Stay.ListingId = Listing.ListingId" +
                    $"INNER JOIN Review on Review.ReviewId = Stay.StayId" +
                    $"INNER JOIN ListingAmenity on ListingAmenity.ListingId = Listing.ListingId;"
                };

                cmd.Parameters.AddWithValue("@ListingId", listing.Id);

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

                SqlCommand cmd2 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"SELECT * FROM Stay WHERE ListingId=@ListingId"
                };

                cmd.Parameters.AddWithValue("@ListingId", listing.Id);

                conn.Open();
                using (SqlDataReader dr = cmd2.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Stay stay = new Stay
                        {
                            Id = (int)dr["StayId"],
                            ListingId = (int)dr["ListingId"],
                            GuestId = (int)dr["GuestId"],
                            StartDate = (DateTime)dr["StartDate"],
                            EndDate = (DateTime)dr["EndDate"]
                        };

                        listing.Stays.Add(stay);
                    }
                }

                foreach (Stay s in listing.Stays)
                {
                    SqlCommand cmd3 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = $"SELECT * FROM Review WHERE ReviewId=@ReviewId"  //save review accordingly!!!
                    };
                    cmd.Parameters.AddWithValue("@ReviewId", s.Id);

                    conn.Open();
                
                    using (SqlDataReader dr = cmd3.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Review review = new Review
                            {
                                StayId = s.Id,
                                Rating = (int)dr["Rating"],
                                Text = (string)dr["text"]
                            };

                            s.Review = review;
                        }
                    }
                }

                List<int> amenityIds = new List<int>();
                listing.Amenities = new List<string>();

                SqlCommand cmd4 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"SELECT * FROM AmenityListing WHERE ListingId=@ListingId"
                };
                cmd.Parameters.AddWithValue("@ListingId", listing.Id);

                conn.Open();
                using (SqlDataReader dr = cmd4.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var amenityId = (int)dr["AmenityId"];
                        amenityIds.Add(amenityId);
                    }
                }

                foreach(var a in amenityIds)
                {
                    SqlCommand cmd5 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = $"SELECT * FROM Amenity WHERE AmenityId=@AmenityId"
                    };
                    cmd.Parameters.AddWithValue("@AmenityId", a);

                    conn.Open();
                    using (SqlDataReader dr = cmd4.ExecuteReader())
                    {
                        var amenity = (string)dr["AmenityName"];
                        listing.Amenities.Add(amenity);
                    }
                }
            }

            return listing;
        }
        public UserAccount GetUserById(int id)
        {
            UserAccount user = new UserAccount();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"SELECT * FROM UserAccount WHERE UserId=@UserId"
                };
                cmd.Parameters.AddWithValue("@UserId", id);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        user.Id = (int)dr["UserId"];
                        user.Email = (string)dr["Email"];
                        user.Password = (string)dr["Password"];
                    }
                }

                user.Listings = new List<Listing>();

                SqlCommand cmd2 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"SELECT * FROM Listing WHERE HostId=@HostId"
                };
                cmd.Parameters.AddWithValue("@HostId", id);

                conn.Open();
                using (SqlDataReader dr = cmd2.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var listing = new Listing
                        {
                            Id = (int)dr["ListingId"],
                            Title = (string)dr["Title"],
                            Description = (string)dr["Description"],
                            Location = (string)dr["Location"],
                            Rate = (decimal)dr["Rate"]
                        };

                        user.Listings.Add(listing);
                    }
                }

                user.Favorites = new List<int>();

                SqlCommand cmd3 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"SELECT * FROM UserListing WHERE UserId=@UserId"
                };
                cmd.Parameters.AddWithValue("@UserId", id);

                conn.Open();
                using (SqlDataReader dr = cmd3.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var favorite = (int)dr["ListingId"];
                        user.Favorites.Add(favorite);
                    }
                }

                SqlCommand cmd4 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"SELECT * FROM Stay WHERE GuestId=@GuestId"
                };
                cmd.Parameters.AddWithValue("@GuestId", id);

                conn.Open();
                using (SqlDataReader dr = cmd4.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Stay stay = new Stay
                        {
                            Id = (int)dr["StayId"],
                            ListingId = (int)dr["ListingId"],
                            GuestId = (int)dr["GuestId"],
                            StartDate = (DateTime)dr["StartDate"],
                            EndDate = (DateTime)dr["EndDate"]
                        };

                        user.Stays.Add(stay);
                    }
                }
                //NEED TO ADD REVIEWS AS WELL.
            }

            return user;
        }
        public void RemoveListing(Listing listing)
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"DELETE FROM AmenityListing WHERE ListingId=@ListingId; " +
                $"DELETE FROM UserListing WHERE ListingId=@ListingId; DELETE FROM Listing WHERE ListingId=@ListingId"
                };
                cmd.Parameters.AddWithValue("@ListingId", listing.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void AddStay(Stay stay)
        {
            //simplify using?? Might wanna test before and after...

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO Stay VALUES (@StayId, @ListingId, @GuestId, @ReviewId, @StartDate, @EndDate)"
                };

                cmd.Parameters.AddWithValue("@StayId", stay.Id);
                cmd.Parameters.AddWithValue("@ListingId", stay.ListingId);
                cmd.Parameters.AddWithValue("@GuestId", stay.GuestId);
                cmd.Parameters.AddWithValue("@ReviewId", null);
                cmd.Parameters.AddWithValue("@StartDate", stay.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", stay.EndDate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void AddReview(Review review)
        {
            throw new NotImplementedException();
            //review id will be same as stay id, which is what this object has
        }
        public bool CheckUsername(string username)
        {
            throw new NotImplementedException();
        }
        public List<int> GetUserIds()
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
