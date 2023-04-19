USE master
IF EXISTS(select * from sys.databases where name='Bnb')
DROP DATABASE Bnb

CREATE DATABASE Bnb
GO

USE Bnb;
GO


CREATE TABLE UserAccount (
	UserId INT IDENTITY(1,1) PRIMARY KEY,
	Username VARCHAR (100) NOT NULL,
	UserPassword VARCHAR (100) NOT NULL,
	Email VARCHAR (100) 
)
GO

CREATE TABLE Listing (

	ListingId INT IDENTITY(1,1) PRIMARY KEY,
	HostId INT FOREIGN KEY REFERENCES UserAccount(UserId) NOT NULL,
	Title VARCHAR (100) NOT NULL,
	Description VARCHAR (500) NOT NULL,
	Location VARCHAR (100) NOT NULL,
	Rate DECIMAL NOT NULL
)
GO

CREATE TABLE Review (

	ReviewId INT IDENTITY(1,1) PRIMARY KEY, 
	Rating INT NOT NULL,
	ReviewText VARCHAR (500) NOT NULL,
	Username VARCHAR (100) NOT NULL
)
GO

CREATE TABLE Stay (

	StayId INT IDENTITY(1,1) PRIMARY KEY,
	PropertyId INT FOREIGN KEY REFERENCES Listing(ListingId) ON DELETE CASCADE NOT NULL,
	ReviewId INT FOREIGN KEY REFERENCES Review(ReviewId), 
	GuestId INT FOREIGN KEY REFERENCES UserAccount(UserId) NOT NULL,
	StartDate Date Not Null,
	EndDate Date Not Null
)
GO


CREATE TABLE Amenity (

	AmenityId INT IDENTITY(1,1) PRIMARY KEY,
	AmenityName VARCHAR (100) NOT NULL,
)
GO

CREATE TABLE UserListing (
	UserId INT NOT NULL,
	FavoriteId INT NOT NULL,
	CONSTRAINT PK_UserListing
		PRIMARY KEY (UserId, FavoriteId),
	CONSTRAINT FK_UserId_UserListing
		FOREIGN KEY (UserId) REFERENCES UserAccount(UserId),
	CONSTRAINT FK_ListingId_UserListing
		FOREIGN KEY (FavoriteId) REFERENCES Listing(ListingId) ON DELETE CASCADE
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
		FOREIGN KEY (ListingId) REFERENCES Listing(ListingId) ON DELETE CASCADE
)
GO

CREATE TABLE ListingImage (
	ImageId INT IDENTITY(1,1) PRIMARY KEY,
	ListingShownId INT FOREIGN KEY REFERENCES Listing(ListingId) ON DELETE CASCADE NOT NULL,
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



INSERT INTO UserAccount
VALUES 
('Seth', '$2a$11$Ux6Rxc0A0Wx0.26EAAY6seRa5bAl9K7Sx/9uaUPprw9uM19VUAW52', 'myemail'),  
('Bob', '$2a$11$Ux6Rxc0A0Wx0.26EAAY6seRa5bAl9K7Sx/9uaUPprw9uM19VUAW52', 'hisemail');


INSERT INTO Listing
VALUES
(1, 'Cozy 2BR Cabin', 'Come stay at our gorgeous cabin by the river', 'Redwing, MN', 150),
(2, 'Downtown loft', 'Great place to stay to catch a game.', 'Minneapolis, MN', 180),
(1, 'Sailboat', 'Enjoy a cozy sailboat on the marina', 'Lisbon, Portugal', 75);



INSERT INTO Stay
VALUES
(2, NULL, 1, GETDATE(), GETDATE()),
(1, NULL, 2, GETDATE(), GETDATE()),
(1, NULL, 1, GETDATE(), GETDATE()),
(2, NULL, 1, '2023-04-12', '2023-04-14');


INSERT INTO Review
VALUES
(5, 'We had a wonderful stay.', 'Seth'),
(4, 'We enjoyed our stay. Some wi-fi issues', 'Bob');

UPDATE Stay SET ReviewId=1 WHERE StayId=1;

UPDATE Stay SET ReviewId=2 WHERE StayId=2;

INSERT INTO Amenity
VALUES
('Hot tub'),
('Fireplace'),
('Pool table'),
('Dishwasher'),
('Gas Range'),
('Lake Access');

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
