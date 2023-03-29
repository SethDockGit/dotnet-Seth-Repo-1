using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            var a = 1;
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
            var a = 1;
            return amenities;
        }
        public Listing GetListingById(int id)
        {
            Listing listing = new Listing();
            listing.Amenities = new List<string>();
            listing.Stays = new List<Stay>();

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

                        Stay stay = new Stay();

                        stay.Id = (int)dr["StayId"];
                        stay.ListingId = (int)dr["ListingId"];
                        stay.GuestId = (int)dr["GuestId"];
                        stay.StartDate = (DateTime)dr["StartDate"];
                        stay.EndDate = (DateTime)dr["EndDate"];

                        if (dr["ReviewId"] != null)
                        {
                            Review review = new Review();

                            review.StayId = (int)dr["StayId"];
                            review.Rating = (int)dr["Rating"];
                            review.Text = (string)dr["ReviewText"];

                            stay.Review = review;
                        }

                        if(listing.Stays.Any(s => s.Id == (int)dr["StayId"]))
                        {

                        }

                        //listing.Amenities.Add
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

            //this method is superfluous and should be deleted
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
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO Review VALUES (@ReviewId, @Rating, @ReviewText, @Username, @StartDate)"
                };

                cmd.Parameters.AddWithValue("@ReviewId", review.StayId);
                cmd.Parameters.AddWithValue("@Rating", review.Rating);
                cmd.Parameters.AddWithValue("@ReviewText", review.Text);
                cmd.Parameters.AddWithValue("@Username", review.Username);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public bool CheckUsername(string username)
        {
            bool isDuplicate = false;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT Username from UserAccount"
                };

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if ((string)dr["Username"] == username)
                        {
                            isDuplicate = true;
                        }
                    }
                }
            }
            return isDuplicate;
        }
        public List<int> GetUserIds()
        {
            List<int> ids = new List<int>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT UserId from UserAccount"
                };

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ids.Add((int)dr["UserId"]);
                    }
                }
            }
            var a = 1;
            return ids;
        }
        public void AddUser(UserAccount user)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO UserAccount VALUES (@UserId, @Username, @UserPassword, @Email)"
                };

                cmd.Parameters.AddWithValue("@UserId", user.Id);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@UserPassword", user.Password);
                cmd.Parameters.AddWithValue("@Email", user.Email);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public UserAccount GetUserByUsername(string username)
        {
            UserAccount user = new UserAccount();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * from UserAccount WHERE Username = @Username"
                };
                cmd.Parameters.AddWithValue("@Username", username);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        user.Id = (int)dr["UserId"];
                        user.Username = (string)dr["Username"];
                        user.Password = (string)dr["UserPassword"];
                        user.Email = (string)dr["Email"];
                    }
                }

                user.Listings = new List<Listing>();

                SqlCommand cmd2 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"SELECT * FROM Listing WHERE HostId=@HostId"
                };
                cmd.Parameters.AddWithValue("@HostId", user.Id);

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
                cmd.Parameters.AddWithValue("@UserId", user.Id);

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
                cmd.Parameters.AddWithValue("@GuestId", user.Id);

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
                //need the stay review as well
            }
            var a = 1;
            return user;
        }
        public void AddFavorite(UserListing ul)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO UserListing VALUES (@UserId, @ListingId)"
                };

                cmd.Parameters.AddWithValue("@UserId", ul.UserId);
                cmd.Parameters.AddWithValue("@Username", ul.ListingId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }


}
