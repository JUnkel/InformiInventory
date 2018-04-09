CREATE TABLE Users
(
	UserId INTEGER NOT NULL PRIMARY KEY,
	
	UserName VARCHAR(40) NOT NULL,
	
	KeyCode VARCHAR(40) NOT NULL,

	StoreId INT,

	FOREIGN KEY(StoreId) REFERENCES Stores(StoreId)
);

INSERT INTO Users (Username, KeyCode) VALUES ('admin', 'prosoft');

CREATE TABLE Stores
(
	StoreId INTEGER NOT NULL PRIMARY KEY,
	
	StoreName VARCHAR (40) NOT NULL Unique
);

CREATE TABLE Storages
(
	StorageId INTEGER NOT NULL PRIMARY KEY,
	
	StorerageName VARCHAR (40) NOT NULL Unique
);


INSERT INTO Storages(StorerageName) VALUES('A');
INSERT INTO Storages(StorerageName) VALUES('B');
INSERT INTO Storages(StorerageName) VALUES('C');
INSERT INTO Storages(StorerageName) VALUES('D');
INSERT INTO Storages(StorerageName) VALUES('E');
INSERT INTO Storages(StorerageName) VALUES('F');



INSERT INTO Stores (StoreName) VALUES ('Iserlohn');

CREATE TABLE  Inventories
(
		InventoryId INTEGER NOT NULL PRIMARY KEY,		
	
        StoreId INTEGER NOT NULL UNIQUE,

        Dt DateTime NOT NULL, 

        UserId INTEGER NOT NULL, 

		FOREIGN KEY(UserId) REFERENCES Users(UserId),

		FOREIGN KEY(StoreId) REFERENCES Stores(StoreId)
);

INSERT INTO Inventories(StoreId, Dt, UserId) VALUES (0,date('now'),0);

CREATE TABLE InventoryLines
(
		Id INTEGER NOT NULL PRIMARY KEY,		
	
        InventoryId INTEGER NOT NULL,

        UserId INTEGER NOT NULL,

		ArtId VARCHAR(40) NOT NULL,
		
		Amnt DECIMAL NOT NULL DEFAULT(0),

		TargetStock DECIMAL NOT NULL NULL DEFAULT(0),

		FOREIGN KEY(ArtId) REFERENCES Articles(Id),

		FOREIGN KEY(InventoryId) REFERENCES Inventories(InventoryId)
);

INSERT INTO InventoryLines(InventoryId, UserId, ArtId, Amnt, TargetStock) VALUES (0,0,0,5,10);

CREATE TABLE Articles
(
	Id INTEGER PRIMARY KEY,
	
	GTIN VARCHAR(14) UNIQUE,
	
	ADesc VARCHAR(40),

	StorageId int NULL,
	FOREIGN KEY(StorageId) REFERENCES Storages(id)

);

INSERT INTO Articles(ADesc, GTIN) VALUES ('Pizza Thunfisch', '0123456789');

CREATE TABLE Restocks
(
		Id INTEGER NOT NULL PRIMARY KEY,		
	
        StoreId INTEGER NOT NULL,

        Dt DateTime NOT NULL, 

        UserId INTEGER NOT NULL, 

		FOREIGN KEY(UserId) REFERENCES Users(UserId),

		FOREIGN KEY(StoreId) REFERENCES Stores(StoreId)
);

CREATE TABLE RestockLines
(
		Id INTEGER NOT NULL PRIMARY KEY,		
		
		RestockId INTEGER NOT NULL,

        Pos INTEGER NULL,

		ArtId VARCHAR(40) NOT NULL,
		
		Amt DECIMAL NOT NULL DEFAULT(0),

		FOREIGN KEY(ArtId) REFERENCES Articles(Id),

		FOREIGN KEY(RestockId) REFERENCES Restocks(Id)
);






