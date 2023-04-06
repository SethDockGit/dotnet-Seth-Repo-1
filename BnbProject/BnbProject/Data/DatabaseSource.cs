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
        "Server=localhost;Database=Bnb;User Id=sa;Password=L'audace1!";

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
            var a = 1;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO Listing VALUES (@ListingId, @HostId, @Title, @Description, @Location, @Rate)"
                    //"INSERT INTO ListingImage VALUES (@ListingId, @Picture)"
                };

                //need to add amenities here as well.

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
            listing.Amenities = new List<string>();
            listing.Stays = new List<Stay>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = $"SELECT Listing.ListingId, Listing.HostId, Listing.Title," +
                    $" Listing.Description, Listing.Location, Listing.Rate,\r\n" +
                    $"Stay.StayId, Stay.GuestId, Stay.ReviewId, Stay.StartDate, " +
                    $"Stay.EndDate, Review.Rating, Review.ReviewText, Review.Username,\r\n" +
                    $"AmenityListing.AmenityId, Amenity.AmenityName FROM Listing" +
                    "LEFT JOIN Stay on Stay.ListingId = Listing.ListingId" +
                    "LEFT JOIN Review on Review.ReviewId = Stay.StayId" +
                    "LEFT JOIN AmenityListing on AmenityListing.ListingId = Listing.ListingId" +
                    "LEFT JOIN Amenity on Amenity.AmenityId = AmenityListing.AmenityId" +
                    "WHERE Listing.ListingId=@ListingId;"
                    
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

                        if (!listing.Stays.Any(s => (int)dr["StayId"] == s.Id))
                        {
                            Stay stay = new Stay();

                            //stay.Id = (int)dr["StayId"];
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
                            listing.Stays.Add(stay);
                        }
                    } 

                    if(!listing.Amenities.Any(a => a == (string)dr["AmenityName"]))
                    {
                        listing.Amenities.Add((string)dr["AmenityName"]);
                    }
                }

                //SqlCommand cmd2 = new SqlCommand
                //{
                //    Connection = conn,
                //    CommandText = $"SELECT * FROM Stay WHERE ListingId=@ListingId"
                //};
                //
                //cmd.Parameters.AddWithValue("@ListingId", listing.Id);
                //
                //conn.Open();
                //using (SqlDataReader dr2 = cmd2.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        Stay stay = new Stay
                //        {
                //            Id = (int)dr["StayId"],
                //            ListingId = (int)dr["ListingId"],
                //            GuestId = (int)dr["GuestId"],
                //            StartDate = (DateTime)dr["StartDate"],
                //            EndDate = (DateTime)dr["EndDate"]
                //        };
                //
                //        listing.Stays.Add(stay);
                //    }
                //}
                //
                //foreach (Stay s in listing.Stays)
                //{
                //    SqlCommand cmd3 = new SqlCommand
                //    {
                //        Connection = conn,
                //        CommandText = $"SELECT * FROM Review WHERE ReviewId=@ReviewId"  //save review accordingly!!!
                //    };
                //    cmd.Parameters.AddWithValue("@ReviewId", s.Id);
                //
                //    conn.Open();
                //
                //    using (SqlDataReader dr = cmd3.ExecuteReader())
                //    {
                //        while (dr.Read())
                //        {
                //            Review review = new Review
                //            {
                //                StayId = s.Id,
                //                Rating = (int)dr["Rating"],
                //                Text = (string)dr["text"]
                //            };
                //
                //            s.Review = review;
                //        }
                //    }
                //}
                //
                //List<int> amenityIds = new List<int>();
                //listing.Amenities = new List<string>();
                //
                //SqlCommand cmd4 = new SqlCommand
                //{
                //    Connection = conn,
                //    CommandText = $"SELECT * FROM AmenityListing WHERE ListingId=@ListingId"
                //};
                //cmd.Parameters.AddWithValue("@ListingId", listing.Id);
                //
                //conn.Open();
                //using (SqlDataReader dr3 = cmd4.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        var amenityId = (int)dr["AmenityId"];
                //        amenityIds.Add(amenityId);
                //    }
                //}
                //
                //foreach(var a in amenityIds)
                //{
                //    SqlCommand cmd5 = new SqlCommand
                //    {
                //        Connection = conn,
                //        CommandText = $"SELECT * FROM Amenity WHERE AmenityId=@AmenityId"
                //    };
                //    cmd.Parameters.AddWithValue("@AmenityId", a);
                //
                //    conn.Open();
                //    using (SqlDataReader dr4 = cmd4.ExecuteReader())
                //    {
                //        var amenity = (string)dr["AmenityName"];
                //        listing.Amenities.Add(amenity);
                //    }
                //}
            }
            //can check this after user is supplied.
            var a = 1;
            return listing;

        }
        public UserAccount GetUserById(int id)
        {

            UserAccount user = new UserAccount();
            user.Listings = new List<Listing>();
            user.Stays = new List<Stay>();
            user.Favorites = new List<int>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT UserAccount.Username, UserAccount.UserPassword, UserAccount.Email," +
                    "Listing.ListingId, Listing.HostId, Listing.Title, Listing.Description, Listing.Location," +
                    "Listing.Rate, Stay.StayId, Stay.GuestId, Stay.ReviewId, Stay.StartDate, Stay.EndDate," +
                    "Review.Rating, Review.ReviewText, Review.Username FROM UserAccount " +
                    "LEFT JOIN Listing on Listing.HostId = UserAccount.UserId " +
                    "LEFT JOIN Stay on Stay.GuestId = UserAccount.UserId " +
                    "LEFT JOIN Review on Review.ReviewId = Stay.StayId " +
                    "WHERE UserAccount.UserId=@UserId;"
                };
                cmd.Parameters.AddWithValue("@UserId", id);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        user.Id = id;
                        user.Username = (string)dr["Username"];
                        user.Password = (string)dr["UserPassword"];
                        user.Email = (string)dr["Email"];

                        if (!user.Listings.Any(l => l.Id == (int)dr["ListingId"]) && !Convert.IsDBNull(dr["ListingId"]))
                        {
                            var listing = new Listing
                            {
                                Id = (int)dr["ListingId"],
                                HostId = (int)dr["HostId"],
                                Title = (string)dr["Title"],
                                Description = (string)dr["Description"],
                                Location = (string)dr["Location"],
                                Rate = (decimal)dr["Rate"]
                            };

                            user.Listings.Add(listing);

                        }
                        if (!user.Stays.Any(s => s.Id == (int)dr["StayId"]) && !Convert.IsDBNull(dr["StayId"]))
                        {
                            var stay = new Stay
                            {
                                Id = (int)dr["StayId"],
                                //ListingId = (int)dr["ListingId"], if you made this propertyId you could read it. Each stay on the user needs a listingID for the front end.
                                GuestId = (int)dr["GuestId"],
                                StartDate = (DateTime)dr["StartDate"],
                                EndDate = (DateTime)dr["EndDate"]
                            };

                            if (!Convert.IsDBNull(dr["ReviewId"]))
                            {

                                Review review = new Review();

                                //review.StayId = (int)dr["ReviewId"]; review.reviewId not needed for front end.
                                review.Rating = (int)dr["Rating"];
                                review.Text = (string)dr["ReviewText"];
                                review.Username = (string)dr["Username"];

                                stay.Review = review;
                            }
                            user.Stays.Add(stay);
                        }

                        //new query for favorites.
                        //if (!user.Favorites.Any(f => f == (int)dr["ListingId"]))
                        //{
                        //    user.Favorites.Add((int)dr["ListingId"]);
                        //}
                    }
                }

                return user;
            }
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
            user.Listings = new List<Listing>();
            user.Stays = new List<Stay>();
            user.Favorites = new List<int>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT UserAccount.UserId, UserAccount.Email, UserAccount.Username, UserAccount.UserPassword,\r\n" +
                    "Listing.ListingId, Listing.HostId, Listing.Title, Listing.Description, Listing.Location,\r\n" +
                    "Listing.Rate, Stay.StayId, Stay.GuestId, Stay.ReviewId, Stay.StartDate, Stay.EndDate,\r\n" +
                    "Review.Rating, Review.ReviewText, Review.Username FROM UserAccount " +
                    "LEFT JOIN Listing on Listing.HostId = UserAccount.UserId " +
                    "LEFT JOIN Stay on Stay.GuestId = UserAccount.UserId " +
                    "LEFT JOIN Review on Review.ReviewId = Stay.StayId " +
                    "WHERE UserAccount.Username=@Username;"
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

                        if(!user.Listings.Any(l => l.Id == (int)dr["ListingId"]))
                        {
                            var listing = new Listing
                            {
                                Id = (int)dr["ListingId"],
                                HostId = (int)dr["HostId"],
                                Title = (string)dr["Title"],
                                Description = (string)dr["Description"],
                                Location = (string)dr["Location"],
                                Rate = (decimal)dr["Rate"]
                            };

                            user.Listings.Add(listing);

                        }
                        if (!user.Stays.Any(s => s.Id == (int)dr["StayId"]))
                        {
                            var stay = new Stay
                            {
                                Id = (int)dr["StayId"],
                                ListingId = (int)dr["ListingId"],
                                GuestId = (int)dr["GuestId"],                            
                                StartDate = (DateTime)dr["StartDate"],
                                EndDate = (DateTime)dr["EndDate"]
                            };

                            if (!Convert.IsDBNull(dr["ReviewId"]))
                            {

                                Review review = new Review();
                            
                                review.StayId = (int)dr["ReviewId"];
                                review.Rating = (int)dr["Rating"];
                                review.Text = (string)dr["ReviewText"];
                                review.Username = (string)dr["Username"];
                            
                                stay.Review = review;
                            }
                            user.Stays.Add(stay);
                        }
                        
                        //need new query for favorites.
                        //if (!user.Favorites.Any(f => f == (int)dr["ListingId"]))
                        //{
                        //    user.Favorites.Add((int)dr["ListingId"]);
                        //}
                    }
                }
                
                return user;

                //
                //user.Listings = new List<Listing>();
                //
                //SqlCommand cmd2 = new SqlCommand
                //{
                //    Connection = conn,
                //    CommandText = $"SELECT * FROM Listing WHERE HostId=@HostId"
                //};
                //cmd.Parameters.AddWithValue("@HostId", user.Id);
                //
                //conn.Open();
                //using (SqlDataReader dr = cmd2.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        var listing = new Listing
                //        {
                //            Id = (int)dr["ListingId"],
                //            Title = (string)dr["Title"],
                //            Description = (string)dr["Description"],
                //            Location = (string)dr["Location"],
                //            Rate = (decimal)dr["Rate"]
                //        };
                //
                //        user.Listings.Add(listing);
                //    }
                //}
                //
                //user.Favorites = new List<int>();
                //
                //SqlCommand cmd3 = new SqlCommand
                //{
                //    Connection = conn,
                //    CommandText = $"SELECT * FROM UserListing WHERE UserId=@UserId"
                //};
                //cmd.Parameters.AddWithValue("@UserId", user.Id);
                //
                //conn.Open();
                //using (SqlDataReader dr = cmd3.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        var favorite = (int)dr["ListingId"];
                //        user.Favorites.Add(favorite);
                //    }
                //}
                //
                //SqlCommand cmd4 = new SqlCommand
                //{
                //    Connection = conn,
                //    CommandText = $"SELECT * FROM Stay WHERE GuestId=@GuestId"
                //};
                //cmd.Parameters.AddWithValue("@GuestId", user.Id);
                //
                //conn.Open();
                //using (SqlDataReader dr = cmd4.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        Stay stay = new Stay
                //        {
                //            Id = (int)dr["StayId"],
                //            ListingId = (int)dr["ListingId"],
                //            GuestId = (int)dr["GuestId"],
                //            StartDate = (DateTime)dr["StartDate"],
                //            EndDate = (DateTime)dr["EndDate"]
                //        };
                //
                //        user.Stays.Add(stay);
                //    }
                //}
                //need the stay review as well
            }
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
