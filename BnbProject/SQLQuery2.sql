SELECT * FROM UserAccount
LEFT JOIN Listing on Listing.HostId = UserAccount.UserId
LEFT JOIN Stay on Stay.GuestId = UserAccount.UserId
LEFT JOIN Review on Review.ReviewId = Stay.StayId
LEFT JOIN UserListing on UserListing.UserId = UserAccount.UserId
WHERE UserAccount.Username='Seth'