SELECT UserAccount.UserId, UserAccount.Email, UserAccount.Username, UserAccount.UserPassword,
Listing.ListingId, Listing.HostId, Listing.Title, Listing.Description, Listing.Location,
Listing.Rate, Stay.StayId, Stay.GuestId, Stay.ReviewId, Stay.StartDate, Stay.EndDate,
Review.Rating, Review.ReviewText, Review.Username
FROM UserAccount
LEFt JOIN Listing on Listing.HostId = UserAccount.UserId
LEFT JOIN Stay on Stay.GuestId = UserAccount.UserId
LEFT JOIN Review on Review.ReviewId = Stay.StayId
WHERE UserAccount.Username='Seth'