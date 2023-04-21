using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BnbProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using static System.Net.Mime.MediaTypeNames;

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
                    CommandText = "SELECT * FROM Listing LEFT JOIN ListingImage on ListingImage.ListingshownId=Listing.ListingId " +
                    "LEFT JOIN Stay on Stay.PropertyId=Listing.ListingId " +
                    "LEFT JOIN AmenityListing on AmenityListing.ListingId=Listing.ListingId " +
                    "LEFT JOIN Amenity on Amenity.AmenityId = AmenityListing.AmenityId;"
                };

                conn.Open();
                using SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!listings.Any(l => l.Id == (int)dr["ListingId"]))
                    {
                        var listing = new Listing
                        {
                            Id = (int)dr["ListingId"],
                            HostId = (int)dr["HostId"],
                            Title = (string)dr["Title"],
                            Description = (string)dr["Description"],
                            Location = (string)dr["Location"],
                            Rate = (decimal)dr["Rate"],
                            Pictures = new List<Picture>(),
                            Stays = new List<Stay>(),
                            Amenities = new List<string>()
                        };
                        listings.Add(listing);
                    }

                    var listingToAmend = listings.Last();

                    if (!Convert.IsDBNull(dr["ImageId"]) && !listingToAmend.Pictures.Any(p => p.Id == (int)dr["ImageId"]))
                    {
                        var picture = new Picture()
                        {
                            Data = (byte[])dr["Picture"],
                            Id = (int)dr["ImageId"]
                        };

                        listingToAmend.Pictures.Add(picture);
                    }

                    if (!listingToAmend.Stays.Any(s => (int)dr["StayId"] == s.Id) && !Convert.IsDBNull(dr["StayId"]))
                    {
                        Stay stay = new Stay
                        {
                            Id = (int)dr["StayId"],
                            ListingId = (int)dr["PropertyId"],
                            GuestId = (int)dr["GuestId"],
                            StartDate = (DateTime)dr["StartDate"],
                            EndDate = (DateTime)dr["EndDate"]
                        };

                        listingToAmend.Stays.Add(stay);
                    }
                    if (!listingToAmend.Amenities.Any(a => a == (string)dr["AmenityName"]) && !Convert.IsDBNull(dr["AmenityName"]))
                    {
                        listingToAmend.Amenities.Add((string)dr["AmenityName"]);
                    }
                }
            }

            return listings;
        }
        public Listing AddListing(Listing listing)
        {

            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "INSERT INTO Listing VALUES (@HostId, @Title, @Description, @Location, @Rate); " +
                "SELECT @@Identity as 'ListingId';"
            };

            cmd.Parameters.AddWithValue("@HostId", listing.HostId);
            cmd.Parameters.AddWithValue("@Title", listing.Title);
            cmd.Parameters.AddWithValue("@Description", listing.Description);
            cmd.Parameters.AddWithValue("@Location", listing.Location);
            cmd.Parameters.AddWithValue("@Rate", listing.Rate);

            conn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                listing.Id = Convert.ToInt32(dr["ListingId"]);
            }
            conn.Close();

            if (listing.Amenities != null)
            {
                List<int> amenityIds = GetAmenityIdsByName(listing.Amenities);

                foreach (var a in amenityIds)
                {
                    SqlCommand cmd2 = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "INSERT INTO AmenityListing VALUES (@AmenityId, @ListingId)"
                    };

                    cmd2.Parameters.AddWithValue("@AmenityId", a);
                    cmd2.Parameters.AddWithValue("@ListingId", listing.Id);

                    conn.Open();
                    cmd2.ExecuteNonQuery();
                }
            }

            return listing;
        }
        public List<int> GetAmenityIdsByName(List<string> amenities)
        {
            List<int> amenityIds = new List<int>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                foreach (var a in amenities)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = conn,
                        CommandText = "SELECT AmenityId FROM Amenity WHERE AmenityName=@AmenityName"
                    };
                    cmd.Parameters.AddWithValue("AmenityName", a);

                    conn.Open();
                    using SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        int id = (int)dr["AmenityId"];
                        amenityIds.Add(id);
                    }
                }
            }

            return amenityIds;
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
                using SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var amenity = (string)dr["AmenityName"];

                    amenities.Add(amenity);
                }
            }

            return amenities;
        }
        public Listing GetListingById(int id)
        {
            Listing listing = new Listing
            {
                Amenities = new List<string>(),
                Stays = new List<Stay>(),
                Pictures = new List<Picture>()
            };

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConnectionString;

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT Listing.ListingId, Listing.HostId, Listing.Title, " +
                    "Listing.Description, Listing.Location, Listing.Rate, " +
                    "Stay.StayId, Stay.PropertyId, Stay.GuestId, Stay.ReviewId, Stay.StartDate, " +
                    "Stay.EndDate, Review.Rating, Review.ReviewText, Review.Username, " +
                    "Amenity.AmenityName, ListingImage.ImageId, ListingImage.Picture FROM Listing " +
                    "LEFT JOIN Stay on Stay.PropertyId = Listing.ListingId " +
                    "LEFT JOIN Review on Review.ReviewId = Stay.ReviewId " +
                    "LEFT JOIN AmenityListing on AmenityListing.ListingId = Listing.ListingId " +
                    "LEFT JOIN Amenity on Amenity.AmenityId = AmenityListing.AmenityId " +
                    "LEFT JOIN ListingImage on ListingImage.ListingshownId=Listing.ListingId " +
                    "WHERE Listing.ListingId=@ListingId"

                };

                cmd.Parameters.AddWithValue("@ListingId", id);

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

                        if (!listing.Stays.Any(s => (int)dr["StayId"] == s.Id) && !Convert.IsDBNull(dr["StayId"]))
                        {
                            Stay stay = new Stay
                            {
                                Id = (int)dr["StayId"],
                                ListingId = (int)dr["PropertyId"],
                                GuestId = (int)dr["GuestId"],
                                StartDate = (DateTime)dr["StartDate"],
                                EndDate = (DateTime)dr["EndDate"]
                            };

                            if (!Convert.IsDBNull(dr["ReviewId"]))
                            {
                                Review review = new Review
                                {
                                    Rating = (int)dr["Rating"],
                                    Text = (string)dr["ReviewText"],
                                    Username = (string)dr["Username"]
                                };

                                stay.Review = review;
                            }
                            listing.Stays.Add(stay);
                        }

                        if (!listing.Amenities.Any(a => a == (string)dr["AmenityName"]) && !Convert.IsDBNull(dr["AmenityName"]))
                        {
                            listing.Amenities.Add((string)dr["AmenityName"]);
                        }

                        if (!Convert.IsDBNull(dr["ImageId"]) && !listing.Pictures.Any(p => p.Id == (int)dr["ImageId"]))
                        {
                            var picture = new Picture()
                            {
                                Data = (byte[])dr["Picture"],
                                Id = (int)dr["ImageId"]
                            };

                            listing.Pictures.Add(picture);
                        }
                    }
                }
                return listing;
            }
        }
        public UserAccount GetUserById(int id)
        {

            UserAccount user = new UserAccount
            {
                Listings = new List<Listing>(),
                Stays = new List<Stay>(),
                Favorites = new List<int>()
            };

            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "SELECT UserAccount.Username, UserAccount.UserPassword, UserAccount.Email," +
                "Listing.ListingId, Listing.HostId, Listing.Title, Listing.Description, Listing.Location," +
                "Listing.Rate, ListingImage.ImageId, ListingImage.Picture, Stay.StayId, Stay.PropertyId, Stay.ReviewId, Stay.GuestId, Stay.StartDate, Stay.EndDate," +
                "Review.Rating, Review.ReviewText, Review.Username, UserListing.FavoriteId FROM UserAccount " +
                "LEFT JOIN Listing on Listing.HostId = UserAccount.UserId " +
                "LEFT JOIN ListingImage on ListingImage.ListingshownId=Listing.ListingId " +
                "LEFT JOIN Stay on Stay.GuestId = UserAccount.UserId " +
                "LEFT JOIN Review on Review.ReviewId = Stay.ReviewId " +
                "LEFT JOIN UserListing on UserListing.UserId = UserAccount.UserId " +
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
                            Rate = (decimal)dr["Rate"],
                            Pictures = new List<Picture>()
                        };

                        if (!Convert.IsDBNull(dr["ImageId"]) && !listing.Pictures.Any(p => p.Id == (int)dr["ImageId"]))
                        {
                            var picture = new Picture()
                            {
                                Data = (byte[])dr["Picture"],
                                Id = (int)dr["ImageId"]
                            };

                            listing.Pictures.Add(picture);
                        }

                        user.Listings.Add(listing);

                    }
                    if (!user.Stays.Any(s => s.Id == (int)dr["StayId"]) && !Convert.IsDBNull(dr["StayId"]))
                    {
                        var stay = new Stay
                        {
                            Id = (int)dr["StayId"],
                            ListingId = (int)dr["PropertyId"],
                            GuestId = (int)dr["GuestId"],
                            StartDate = (DateTime)dr["StartDate"],
                            EndDate = (DateTime)dr["EndDate"]
                        };

                        if (!Convert.IsDBNull(dr["ReviewId"]))
                        {

                            Review review = new Review
                            {
                                Rating = (int)dr["Rating"],
                                Text = (string)dr["ReviewText"],
                                Username = (string)dr["Username"]
                            };

                            stay.Review = review;
                        }
                        user.Stays.Add(stay);
                    }

                    if (!user.Favorites.Any(f => f == (int)dr["FavoriteId"]) && !Convert.IsDBNull(dr["FavoriteId"]))
                    {
                        user.Favorites.Add((int)dr["FavoriteId"]);
                    }
                }
            }

            return user;
        }
        public void EditListing(Listing listing)
        {

            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "UPDATE Listing SET Title=@Title, Description=@Description, " +
                "Location=@Location, Rate=@Rate WHERE ListingId=@ListingId;" +
                "DELETE FROM AmenityListing WHERE ListingId=@ListingId;"
            };
            cmd.Parameters.AddWithValue("@ListingId", listing.Id);
            cmd.Parameters.AddWithValue("@Title", listing.Title);
            cmd.Parameters.AddWithValue("@Description", listing.Description);
            cmd.Parameters.AddWithValue("@Location", listing.Location);
            cmd.Parameters.AddWithValue("@Rate", listing.Rate);

            conn.Open();
            cmd.ExecuteNonQuery();

            List<int> amenityIds = GetAmenityIdsByName(listing.Amenities);

            foreach (var a in amenityIds)
            {
                SqlCommand cmd2 = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "INSERT INTO AmenityListing VALUES (@AmenityId, @ListingId)"
                };

                cmd2.Parameters.AddWithValue("@AmenityId", a);
                cmd2.Parameters.AddWithValue("@ListingId", listing.Id);

                cmd2.ExecuteNonQuery();
            }
        }
        public void DeleteListing(int listingId)
        {
            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "DELETE FROM Listing WHERE ListingId=@ListingId; " +
                "DELETE FROM Review WHERE ReviewId not IN (SELECT ReviewId FROM Stay WHERE ReviewId is not null);"
            };
            cmd.Parameters.AddWithValue("@ListingId", listingId);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public void AddStay(Stay stay)
        {
            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "INSERT INTO Stay (PropertyId, GuestId, StartDate, EndDate) " +
                "VALUES (@ListingId, @GuestId, @StartDate, @EndDate);"
            };

            cmd.Parameters.AddWithValue("@ListingId", stay.ListingId);
            cmd.Parameters.AddWithValue("@GuestId", stay.GuestId);
            cmd.Parameters.AddWithValue("@StartDate", stay.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", stay.EndDate);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public void AddReview(Review review)
        {
            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "INSERT INTO Review VALUES (@Rating, @ReviewText, @Username);" +
                "UPDATE Stay SET ReviewId=IDENT_CURRENT('Review') WHERE StayId=@StayId;"
            };


            cmd.Parameters.AddWithValue("@Rating", review.Rating);
            cmd.Parameters.AddWithValue("@ReviewText", review.Text);
            cmd.Parameters.AddWithValue("@Username", review.Username);
            cmd.Parameters.AddWithValue("@StayId", review.StayId);

            conn.Open();
            cmd.ExecuteNonQuery();
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
                using SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if ((string)dr["Username"] == username)
                    {
                        isDuplicate = true;
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
                using SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ids.Add((int)dr["UserId"]);
                }
            }

            return ids;
        }
        public UserAccount AddUser(UserAccount user)
        {
            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "INSERT INTO UserAccount VALUES (@Username, @UserPassword, @Email); " +
                "SELECT @@Identity as 'UserId';"
            };

            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@UserPassword", user.Password);
            cmd.Parameters.AddWithValue("@Email", user.Email);

            conn.Open();
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                user.Id = Convert.ToInt32(dr["UserId"]);
            }
            conn.Close();

            return user;
        }
        public UserAccount GetUserByUsername(string username)
        {
            UserAccount user = new UserAccount
            {
                Listings = new List<Listing>(),
                Stays = new List<Stay>(),
                Favorites = new List<int>()
            };

            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "SELECT UserAccount.UserId, UserAccount.Email, UserAccount.Username, UserAccount.UserPassword," +
                "Listing.ListingId, Listing.HostId, Listing.Title, Listing.Description, Listing.Location," +
                "Listing.Rate, ListingImage.ImageId, ListingImage.Picture, Stay.StayId, Stay.PropertyId, Stay.GuestId, Stay.StartDate, Stay.EndDate," +
                "Review.ReviewId, Review.Rating, Review.ReviewText, Review.Username, UserListing.FavoriteId FROM UserAccount " +
                "LEFT JOIN Listing on Listing.HostId = UserAccount.UserId " +
                "LEFT JOIN ListingImage on ListingImage.ListingshownId=Listing.ListingId " +
                "LEFT JOIN Stay on Stay.GuestId = UserAccount.UserId " +
                "LEFT JOIN Review on Review.ReviewId = Stay.ReviewId " +
                "LEFT JOIN UserListing on UserListing.UserId = UserAccount.UserId " +
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

                    if (!user.Listings.Any(l => l.Id == (int)dr["ListingId"]) && !Convert.IsDBNull(dr["ListingId"]))
                    {
                        var listing = new Listing
                        {
                            Id = (int)dr["ListingId"],
                            HostId = (int)dr["HostId"],
                            Title = (string)dr["Title"],
                            Description = (string)dr["Description"],
                            Location = (string)dr["Location"],
                            Rate = (decimal)dr["Rate"],
                            Pictures = new List<Picture>()
                        };

                        if (!Convert.IsDBNull(dr["ImageId"]) && !listing.Pictures.Any(p => p.Id == (int)dr["ImageId"]))
                        {
                            var picture = new Picture()
                            {
                                Data = (byte[])dr["Picture"],
                                Id = (int)dr["ImageId"]
                            };

                            listing.Pictures.Add(picture);
                        }

                        user.Listings.Add(listing);

                    }
                    if (!user.Stays.Any(s => s.Id == (int)dr["StayId"]) && !Convert.IsDBNull(dr["StayId"]))
                    {
                        var stay = new Stay
                        {
                            Id = (int)dr["StayId"],
                            ListingId = (int)dr["PropertyId"],
                            GuestId = (int)dr["GuestId"],
                            StartDate = (DateTime)dr["StartDate"],
                            EndDate = (DateTime)dr["EndDate"]
                        };

                        if (!Convert.IsDBNull(dr["ReviewId"]) && !Convert.IsDBNull(dr["ReviewId"]))
                        {

                            Review review = new Review
                            {
                                Rating = (int)dr["Rating"],
                                Text = (string)dr["ReviewText"],
                                Username = (string)dr["Username"]
                            };

                            stay.Review = review;
                        }
                        user.Stays.Add(stay);
                    }

                    if (!user.Favorites.Any(f => f == (int)dr["FavoriteId"]) && !Convert.IsDBNull(dr["FavoriteId"]))
                    {
                        user.Favorites.Add((int)dr["FavoriteId"]);
                    }

                }
            }
            return user;
        }
        public void AddFavorite(UserListing ul)
        {
            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "INSERT INTO UserListing VALUES (@UserId, @ListingId);"
            };

            cmd.Parameters.AddWithValue("@UserId", ul.UserId);
            cmd.Parameters.AddWithValue("@ListingId", ul.ListingId);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public void RemoveFavorite(UserListing ul)
        {
            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "DELETE FROM UserListing WHERE (UserId=@UserId AND FavoriteId=@ListingId)"
            };

            cmd.Parameters.AddWithValue("@UserId", ul.UserId);
            cmd.Parameters.AddWithValue("@ListingId", ul.ListingId);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public void AddFileToListing(byte[] file, int listingId)
        {
            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandText = "INSERT INTO ListingImage VALUES (@ListingShownId, @Picture);"
            };

            cmd.Parameters.AddWithValue("@ListingShownId", listingId);
            cmd.Parameters.AddWithValue("@Picture", file);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public void DeletePicsById(int[] ids)
        {
            using SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
           
            foreach(var id in ids)
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "DELETE FROM ListingImage WHERE (ImageId=@Id)"
                };

                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
