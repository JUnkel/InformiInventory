CREATE TABLE Users
(
	UserId INTEGER NOT NULL PRIMARY KEY DEFAULT(1),
	
	UserName VARCHAR(40) UNIQUE NOT NULL,
	
	KeyCode VARCHAR(40) UNIQUE NOT NULL,

	StoreId INT NULL,

	FOREIGN KEY(StoreId) REFERENCES Stores(Id)
);

INSERT INTO Users (Username, KeyCode, StoreId) VALUES ('admin', 'prosoft',null);
INSERT INTO Users (Username, KeyCode, StoreId) VALUES ('user', 'test', '1');

CREATE TABLE Stores
(
	StoreId INTEGER NOT NULL PRIMARY KEY DEFAULT(1),
	
	StoreName VARCHAR (40) NOT NULL Unique
);

INSERT INTO Stores (StoreName) VALUES ('Iserlohn');

CREATE TABLE Storages
(
	StorageId INTEGER NOT NULL PRIMARY KEY DEFAULT(1),
	
	StorageName VARCHAR (40) NOT NULL Unique
);

CREATE TABLE  Inventories
(
		InventoryId INTEGER NOT NULL PRIMARY KEY DEFAULT(1),		
	
        StoreId INTEGER NOT NULL,

        Dt DateTime NOT NULL, 

        UserId INTEGER NOT NULL, 

		FOREIGN KEY(UserId) REFERENCES Users(Id),

		FOREIGN KEY(StoreId) REFERENCES Stores(Id)
);


CREATE TABLE InventoryLines
(
		InevntoryLineId INTEGER NOT NULL PRIMARY KEY DEFAULT(1),		
	
        InventoryId INTEGER NOT NULL,

        UserId INTEGER NOT NULL,

		ArtId VARCHAR(40) NOT NULL,
		
		Amnt DECIMAL NOT NULL DEFAULT(0),

		TargetStock DECIMAL NOT NULL NULL DEFAULT(0),

		FOREIGN KEY(ArtId) REFERENCES Articles(Id),

		FOREIGN KEY(InventoryId) REFERENCES Inventories(Id)
);


CREATE TABLE Articles
(
	ArticleId INTEGER  PRIMARY KEY DEFAULT(1),
	
	GTIN VARCHAR(14) UNIQUE,
	
	ADesc VARCHAR(200),

	StorageId int NULL,
	FOREIGN KEY(StorageId) REFERENCES Storages(Id)

);

CREATE TABLE Restocks
(
		RestockId INTEGER NOT NULL PRIMARY KEY DEFAULT(1),		
	
        StoreId INTEGER NULL,

        Dt DateTime NOT NULL, 

        UserId INTEGER NOT NULL,
		
		IsProcd INTEGER NOT NULL DEFAULT(0),

		TemplateId INTEGER NULL,

		FOREIGN KEY(UserId) REFERENCES Users(Id),

		FOREIGN KEY(StoreId) REFERENCES Stores(Id),

		FOREIGN KEY(TemplateId) REFERENCES Restocks(Id)

);

CREATE TABLE RestockLines 
(
	RestockLineId INTEGER NOT NULL PRIMARY KEY DEFAULT(1),		
		
	RestockId INTEGER NOT NULL,

    Pos INTEGER NULL,

	ArtId VARCHAR(40) NOT NULL,
	
	Amt INTEGER NOT NULL DEFAULT(0),

	FOREIGN KEY(ArtId) REFERENCES Articles(Id),

	FOREIGN KEY(RestockId) REFERENCES Restocks(Id)
);