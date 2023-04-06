USE master
IF EXISTS(select * from sys.databases where name='Bnb')
DROP DATABASE Bnb

CREATE DATABASE Bnb
GO

USE Bnb;
GO


CREATE TABLE Review (

	ReviewId INT PRIMARY KEY,  
	Rating INT NOT NULL,
	ReviewText VARCHAR (500) NOT NULL,
	Username VARCHAR (100) NOT NULL
)
GO

CREATE TABLE UserAccount (
	UserId INT PRIMARY KEY,
	Username VARCHAR (100) NOT NULL,
	UserPassword VARCHAR (100) NOT NULL,
	Email VARCHAR (100) 
)
GO

CREATE TABLE Listing (

	ListingId INT PRIMARY KEY,
	HostId INT FOREIGN KEY REFERENCES UserAccount(UserId) NOT NULL,
	Title VARCHAR (100) NOT NULL,
	Description VARCHAR (500) NOT NULL,
	Location VARCHAR (100) NOT NULL,
	Rate DECIMAL NOT NULL
)
GO

CREATE TABLE Stay (

	StayId INT PRIMARY KEY,
	ListingId INT FOREIGN KEY REFERENCES Listing(ListingId) NOT NULL,
	GuestId INT FOREIGN KEY REFERENCES UserAccount(UserId) NOT NULL,
	ReviewId INT FOREIGN KEY REFERENCES Review(ReviewId),
	StartDate Date Not Null,
	EndDate Date Not Null
)
GO

CREATE TABLE Amenity (

	AmenityId INT PRIMARY KEY,
	AmenityName VARCHAR (100) NOT NULL,
)
GO

CREATE TABLE UserListing (
	UserId INT NOT NULL,
	ListingId INT NOT NULL,
	CONSTRAINT PK_UserListing
		PRIMARY KEY (UserId, ListingId),
	CONSTRAINT FK_UserId_UserListing
		FOREIGN KEY (UserId) REFERENCES UserAccount(UserId),
	CONSTRAINT FK_ListingId_UserListing
		FOREIGN KEY (ListingId) REFERENCES Listing(ListingId)
)
GO

CREATE TABLE AmenityListing (
	AmenityId INT NOT NULL,
	ListingId INT NOT NULL,
	CONSTRAINT PK_AmenityListing
		PRIMARY KEY (AmenityId, ListingId),
	CONSTRAINT FK_AmenityId_AmenityListing
		FOREIGN KEY (AmenityId) REFERENCES Amenity(AmenityId),
	CONSTRAINT FK_ListingId_AmenityListing
		FOREIGN KEY (ListingId) REFERENCES Listing(ListingId)
)
GO

CREATE TABLE ListingImage (
	ImageId INT NOT NULL,
	ListingId INT FOREIGN KEY REFERENCES Listing(ListingId) NOT NULL,
	Picture IMAGE NOT NULL
)
GO

CREATE TABLE UserStay (
	UserId INT NOT NULL,
	StayId INT NOT NULL,
	CONSTRAINT PK_UserStay
		PRIMARY KEY (UserId, StayId),
	CONSTRAINT FK_UserId_UserStay
		FOREIGN KEY (UserId) REFERENCES UserAccount(UserId),
	CONSTRAINT FK_StayId_UserStay
		FOREIGN KEY (StayId) REFERENCES Stay(StayId)
)
GO


--Get Listing by Id query:


--SELECT * FROM Listing
---INNER JOIN Stay on Stay.ListingId = Listing.ListingId
---INNER JOIN Review on Review.ReviewId = Stay.StayId
---INNER JOIN AmenityListing on AmenityListing.ListingId = Listing.ListingId
---WHERE Listing.ListingId=1;


---INITIAL TESTING DATA


INSERT INTO UserAccount
VALUES 
(1, 'Seth', 'mypass', 'myemail'),
(2, 'Bob', 'hispass', 'hisemail');


INSERT INTO Listing
VALUES
(1, 1, 'Cozy 2BR Cabin', 'Come stay at our gorgeous cabin by the river', 'Redwing, MN', 150),
(2, 2, 'Downtown loft', 'Great place to stay to catch a game.', 'Minneapolis, MN', 180);


INSERT INTO Review
VALUES
(1, 5, 'We had a wonderful stay.', 'Seth'),
(2, 4, 'We enjoyed our stay. Some wi-fi issues', 'Bob');


INSERT INTO Stay
VALUES
(1, 2, 1, 1, GETDATE(), GETDATE()),
(2, 1, 2, 2, GETDATE(), GETDATE()),
(3, 1, 1, null, GETDATE(), GETDATE()); 


--(stay 3 where listing 1 is stayed at by guest 1 (seth, his own listing), no review yet


INSERT INTO Amenity
VALUES
(1, 'Hot tub'),
(2, 'Fireplace'),
(3, 'Pool table'),
(4, 'Dishwasher'),
(5, 'Gas Range'),
(6, 'Lake Access');

INSERT INTO AmenityListing
VALUES
(1, 1),
(2, 1),
(3, 1),
(4, 2),
(5, 2),
(6, 2);

INSERT INTO UserListing
VALUES
(1, 2), 
(2, 1);
