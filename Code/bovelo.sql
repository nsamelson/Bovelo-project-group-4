CREATE TABLE Part(id_part INTEGER UNIQUE,StorageLocation TEXT,Price INTEGER NOT NULL,Quantity INTEGER NOT NULL,ProviderName TEXT NOT NULL,DeliveryTime INTEGER NOT NULL);
INSERT INTO Part VALUES(1,"Cadre 26","A0",300,5,"ALU TOLERIE",3);
INSERT INTO Part VALUES(2,"Cadre 28","A1",350,5,"ALU TOLERIE",2);
INSERT INTO Part VALUES(3,"Roue 26","A2",40,8,"PEUGEOT",1);
INSERT INTO Part VALUES(4,"Roue 28","A3",50,10,"PEUGEOT",1);
INSERT INTO Part VALUES(5,"Guidon 26","A4",80,2,"OXY SOUDURE",3);
INSERT INTO Part VALUES(1,"Guidon 28","A5",90,4,"OXY SOUDURE",4);


CREATE TABLE Bike(id_bike INTEGER NOT NULL,BikeType TEXT NOT NULL,StateBuilding BOOLEAN,StateReadyBuilding BOOLEAN,BuildingTime INTEGER NOT NULL,Quantity INTEGER NOT NULL).
INSERT INTO Bike VALUES(1,"City 26",0,0,3,10);
INSERT INTO Bike VALUES(2,"City 28",0,0,3,10);
INSERT INTO Bike VALUES(3,"Explorer 26",0,0,4,8);
INSERT INTO Bike VALUES(4,"Explorer 28",0,0,4,5);
INSERT INTO Bike VALUES(5,"Adventure 26",0,0,5,4);
INSERT INTO Bike VALUES(6,"Adventure 28",0,0,5,3);

CREATE TABLE OrderBike(id_order INTEGER NOT NULL,OrderType BOOLEAN, DeliveryDate TIME,OrderState TEXT NOT NULL,ShippingAddress TEXT);
INSERT INTO OrderBike VALUES(1,0,NULL,"ReadyToOrder","rue de l'expedition");
INSERT INTO OrderBike VALUES(2,0,NULL,"Processing","rue de l'expedition");
INSERT INTO OrderBike VALUES(3,1,NULL,"ReadyToBuild","rue de l'expedition");
INSERT INTO OrderBike VALUES(4,1,NULL,"ReadyToSend","rue de l'expedition");

CREATE TABLE User(id_user INTEGER NOT NULL,StateUser INTEGER,FirstName TEXT,LastName TEXT,UserLogin TEXT,UserPassword TEXT);
INSERT INTO User VALUES(1,0,"Jean","Dupond","205556","motdepassenonsecurise");
INSERT INTO User VALUES(2,2,"Eric","Dubois","205557","motdepassenonsecurise");
INSERT INTO User VALUES(3,1,"Camille","Duruisseau","205558","motdepassenonsecurise");
INSERT INTO User VALUES(4,1,"Luc","Dufleuve","205559","motdepassenonsecurise");
INSERT INTO User VALUES(5,1,"Herve","Dujardin","205560","motdepassenonsecurise");
