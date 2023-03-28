USE master
IF EXISTS(select * from sys.databases where name='Bnb')
DROP DATABASE Bnb

CREATE DATABASE Bnb
GO

USE Bnb;
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
	Address VARCHAR (100) NOT NULL,
	Rate DECIMAL NOT NULL
)
GO

CREATE TABLE Review (

	ReviewId INT PRIMARY KEY,  
	Rating INT NOT NULL,
	ReviewText VARCHAR (500) NOT NULL
)
GO

CREATE TABLE Stay (

	StayId INT PRIMARY KEY,
	ListingId INT FOREIGN KEY REFERENCES Listing(ListingId) NOT NULL,
	GuestId INT FOREIGN KEY REFERENCES UserAccount(UserId) NOT NULL,
	ReviewId INT FOREIGN KEY REFERENCES Review(ReviewId)
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