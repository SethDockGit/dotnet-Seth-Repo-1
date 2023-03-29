SELECT * FROM Listing
INNER JOIN Stay on Stay.ListingId = Listing.ListingId
INNER JOIN Review on Review.ReviewId = Stay.StayId
INNER JOIN AmenityListing on AmenityListing.ListingId = Listing.ListingId
WHERE Listing.ListingId=1;