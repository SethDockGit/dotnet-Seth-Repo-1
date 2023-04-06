SELECT Listing.ListingId, Listing.HostId, Listing.Title, Listing.Description, Listing.Location, Listing.Rate,
Stay.StayId, Stay.GuestId, Stay.ReviewId, Stay.StartDate, Stay.EndDate, Review.Rating, Review.ReviewText, Review.Username,
AmenityListing.AmenityId, Amenity.AmenityName
FROM Listing
LEFT JOIN Stay on Stay.ListingId = Listing.ListingId
LEFT JOIN Review on Review.ReviewId = Stay.StayId
LEFT JOIN AmenityListing on AmenityListing.ListingId = Listing.ListingId
LEFT JOIN Amenity on Amenity.AmenityId = AmenityListing.AmenityId
WHERE Listing.ListingId=1;