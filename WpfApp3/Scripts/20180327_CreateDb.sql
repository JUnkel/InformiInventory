CREATE TABLE Users (
	UserId INTEGER NOT NULL PRIMARY KEY,
	
	UserName VARCHAR(40) NOT NULL,
	
	KeyCode VARCHAR(40) NOT NULL
);

INSERT INTO Users (Username, KeyCode) VALUES ('admin', 'prosoft');

CREATE TABLE Stores (
	
	StoreId INTEGER NOT NULL PRIMARY KEY,
	
	StoreName VARCHAR (40) NOT NULL Unique,
);

INSERT INTO Stores (StoreName) VALUES ('Iserlohn');

CREATE TABLE  Inventories
(
		InventoryId INTEGER NOT NULL PRIMARY KEY,		
	
        StoreId INTEGER NOT NULL UNIQUE,

        CrDt DateTime NOT NULL, 

        CrUserId INTEGER NOT NULL, 

        InventoryUserId INTEGER NULL,

		FOREIGN KEY(CrUserId) REFERENCES Users(UserId),
	
		FOREIGN KEY(InventoryUserId) REFERENCES Users(UserId),

		FOREIGN KEY(StoreId) REFERENCES Stores(StoreId)
);

INSERT INTO Inventories(StoreId, CrDt, CrUserId,InventoryUserId) VALUES (0,GETDATE(),0,0);


CREATE TABLE  InventoryLines
(
		InventoryLineId INTEGER NOT NULL PRIMARY KEY,		
	
        InventoryId INTEGER NOT NULL,

        BookDt DateTime NOT NULL, 

        UserId INTEGER NOT NULL,

		ArtId VARCHAR(40) NOT NULL,
		
		Amnt DECIMAL NOT NULL DEFAULT(0),

		TargetStock DECIMAL NOT NULL NULL DEFAULT(0),

		FOREIGN KEY(ArtId) REFERENCES Articles(ArtId),

		FOREIGN KEY(InventoryId) REFERENCES Inventories(InventoryId),
);

INSERT INTO Inventories(InventoryId, BookDt, UserId, ArtId, Amnt, TargetStock) VALUES (0,GETDATE(),0,0,5,10);

CREATE TABLE Articles
(
	ArtId INTEGER PRIMARY KEY,
	
	AN VARCHAR(40),
	
	GTIN VARCHAR(14) UNIQUE,	
);

INSERT INTO Articles(AN, GTIN) VALUES ('Pizza Thunfisch', '0123456789');


