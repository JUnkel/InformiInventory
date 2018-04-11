CREATE TABLE Users
(
	Id INTEGER NOT NULL PRIMARY KEY,
	
	UserName VARCHAR(40) UNIQUE NOT NULL,
	
	KeyCode VARCHAR(40) UNIQUE NOT NULL,

	StoreId INT,

	FOREIGN KEY(StoreId) REFERENCES Stores(Id)
);

INSERT INTO Users (Username, KeyCode) VALUES ('admin', 'prosoft');
INSERT INTO Users (Username, KeyCode, StoreId) VALUES ('user', 'test', '1');

CREATE TABLE Stores
(
	Id INTEGER NOT NULL PRIMARY KEY,
	
	StoreName VARCHAR (40) NOT NULL Unique
);

INSERT INTO Stores (StoreName) VALUES ('Iserlohn');

CREATE TABLE Storages
(
	Id INTEGER NOT NULL PRIMARY KEY,
	
	StorageName VARCHAR (40) NOT NULL Unique
);

INSERT INTO Storages(StorageName) VALUES('A');
INSERT INTO Storages(StorageName) VALUES('B');
INSERT INTO Storages(StorageName) VALUES('C');
INSERT INTO Storages(StorageName) VALUES('D');
INSERT INTO Storages(StorageName) VALUES('E');
INSERT INTO Storages(StorageName) VALUES('F');

CREATE TABLE  Inventories
(
		Id INTEGER NOT NULL PRIMARY KEY,		
	
        StoreId INTEGER NOT NULL,

        Dt DateTime NOT NULL, 

        UserId INTEGER NOT NULL, 

		FOREIGN KEY(UserId) REFERENCES Users(Id),

		FOREIGN KEY(StoreId) REFERENCES Stores(Id)
);

INSERT INTO Inventories(StoreId, Dt, UserId, UserId) VALUES (0,date('now'),0,0);

CREATE TABLE InventoryLines
(
		Id INTEGER NOT NULL PRIMARY KEY,		
	
        InventoryId INTEGER NOT NULL,

        UserId INTEGER NOT NULL,

		ArtId VARCHAR(40) NOT NULL,
		
		Amnt DECIMAL NOT NULL DEFAULT(0),

		TargetStock DECIMAL NOT NULL NULL DEFAULT(0),

		FOREIGN KEY(ArtId) REFERENCES Articles(Id),

		FOREIGN KEY(InventoryId) REFERENCES Inventories(Id)
);

INSERT INTO InventoryLines(InventoryId, UserId, ArtId, Amnt, TargetStock) VALUES (0,0,0,5,10);

CREATE TABLE Articles
(
	Id INTEGER PRIMARY KEY,
	
	GTIN VARCHAR(14) UNIQUE,
	
	ADesc VARCHAR(40),

	StorageId int NULL,
	FOREIGN KEY(StorageId) REFERENCES Storages(Id)

);

INSERT INTO Articles(ADesc, GTIN) VALUES ('Pizza Thunfisch', '0123456789');

CREATE TABLE Restocks
(
		Id INTEGER NOT NULL PRIMARY KEY,		
	
        StoreId INTEGER NOT NULL,

        Dt DateTime NOT NULL, 

        UserId INTEGER NOT NULL,
		
		IsProcd INTEGER NOT NULL DEFAULT(0),

		IsTemplate INTEGER NOT NULL DEFAULT(0),

		FOREIGN KEY(UserId) REFERENCES Users(Id),

		FOREIGN KEY(StoreId) REFERENCES Stores(Id)
);

CREATE TABLE RestockLines
(
		Id INTEGER NOT NULL PRIMARY KEY,		
		
		RestockId INTEGER NOT NULL,

        Pos INTEGER NULL,

		ArtId VARCHAR(40) NOT NULL,
		
		Amt INTEGER NOT NULL DEFAULT(0),

		FOREIGN KEY(ArtId) REFERENCES Articles(Id),

		FOREIGN KEY(RestockId) REFERENCES Restocks(Id)
);