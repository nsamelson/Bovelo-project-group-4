# **Bovelo-project-group-4** 

Mohamed Aousji

Antoine Scaviner 

Nicolas Samelson

Pedro Roquero Da costa Pinto

Matthieu Faget


#### Software engineering 2 project

## Introduction

The main goal of this project was to design and develop a software for a bike factory.

Make the management easier and increase the organisation efficiency into a factory is the most important.

This software was made by a team in "agile" method helped by a versionning software. 

Next you can find the main documents and informations related to this project.

## **1. Classes :**  

The classes in this project are :

- *User :*  
The user can be a representative or an admin. The representative can see the catalog, configure a bike, add to cart and pass orders. The admin is able to do the same and is able to see its planning, the components needed to assemble a bike (and the location of the parts) and manage orders.

- *Catalog :*  
The catalog is a class that shows all the different types of bikes (City Bike, Explorer Bike and Adventure Bike).

- *BikePart :*  
It's regrouping every part needed to build a Bike. It also contains the location of each part, the quantity in stock and the time to assemble it.

- *Bike :*  
A bike is built with multiple BikePart's. It contains a type, a total price and can be configured with a size and color.

- *OrderBike :*  
A class to create orders of bikes. A user may have multiple OrderBike’s. An OrderBike has at least 1 Bike in it. 

- *OrderBikePart :*  
A class to create orders of bikePart, it’s the stock management that can order (or “addToCart()”?) and the admins will verify and pass the order.

- *Planning :*  
A class that takes all the OrderBike objects every week to create a planning, depending on the number of admins, when the order was passed and the stock of bike parts.

- *StockManagement :*  
A class that takes all the OrderBike objects every week and will check the stock and order BikeParts if needed. 

- *IupdatableComponent :*  
An interface that updates the classes it's implemented in.


## **2. Diagrams, links and schematics :**  

### **2.1. Diagrams :**  
- *Class Diagram :* https://app.creately.com/diagram/ET0DnpMZXqP/edit
- *Use cases Diagram :* https://lucid.app/lucidchart/a3912014-fae6-4ac3-ad07-5af240573b45/edit?beaconFlowId=0FBAD1001AD03DB9&page=0_0#?folder_id=home&browser=icon
- *User activity Diagram :* https://drive.google.com/file/d/1Gk_sozGZPf4hDu128_76lsnuZxIa-klZ/view  

### **2.2. Links :**  
- *Project report :* https://ecambxl-my.sharepoint.com/:w:/r/personal/17288_ecam_be/_layouts/15/Doc.aspx?sourcedoc=%7BD0B7E887-BAD6-419C-882A-3D3F2BA094BB%7D&file=Rapport.docx&action=default&mobileredirect=true
- *Design brief :* https://claco.ecam.be/apiv2/resource_file79568/raw
- *Azure Devops :* https://dev.azure.com/ECAM3BE/Groupe4  
- *Bike Parts List:* https://ecambxl-my.sharepoint.com/:t:/g/personal/17010_ecam_be/EaTMBrHmODJBg-rELj5LX1MB87OP6OkHwTrc_O8-oE5qKg?e=kAzBUZ
- *PowerPoint:* https://ecambxl-my.sharepoint.com/:p:/g/personal/17338_ecam_be/ET_e1rXheQVIjLAcY5zWgIMBcUq5bcwVUpFtmmSPUWidMQ?rtime=LCVDwNLi2Eg
- *Drive:* https://ecambxl-my.sharepoint.com/:f:/g/personal/17288_ecam_be/EnvNpO93UF9GoMOfZWpRylIBqxhiICFPJOAyQJj0v6-fGg?e=l2qxKf

### **2.3. Schematics :**  
- *Interface mockup :*  
- *Mockup iteration 1 :* https://drive.google.com/file/d/1E008b_3q2edldHUvpJ3NwuaSqwDhu-G8/view?usp=sharing
- *Mockup iteration 2 :* https://drive.google.com/file/d/1Z4jrSf73k9zW0ssS0kXVY-IMI5MD1hKv/view?usp=sharing
- *Mockup iteration 3 :* https://drive.google.com/file/d/1DUMMbBp80TinfHncI6ZJqvvj3tsIi1QV/view?usp=sharing


## **3. User stories :**

###  **3.1 Iteration 1 :**  

- As a representative, I want to be able to login to access my account.  
- As a representative, I want to see the list of products on the homepage of the app in order to present it to the bicycle shop.
- As a representative, I want to click on a product to get its details.
- As a representative, I want to choose the color, quantity and size on a product to add it in the cart.
- As a representative, I want to know the price of a product to show it to the bicycle shop.
- As a representative, I want to be able to add, delete, or see a product in my cart to pass the order.
- As a representative, I want to validate, cancel my cart to manage my cart.
- As a representative, I would like to receive a summary of my order in order to be able to receive the information to collect it.

###  **3.2 Iteration 2 :**  

- As a production manager, I want to be able to see all the orders to make a production planning.
- As an assembler, I want to see the production planning in order to know what I have to do for the week.
- As an assembler, I want to have access to the available bike parts in order to assemble the bikes.
- As a representative, I want to receive an estimated delivery time in order to inform the bicycle shop when the order will be ready.

###  **3.3 Iteration 3 :**  
-	As a manager, I want to know who is assembling the bike and when he started the production, in order to manage the production schedule according to the building process.
-	As a manager, I want to be able to modify the production schedule, in order to manage the orders.
-	As a manager, I want to know how much bike parts there are in stock, to order the bike parts needed.
-	As a manager, I want to be able to order new bike parts, in order to have always enough parts available to assemble the bikes.

## **4. Old links :**  

- *First Diagrams [old] :* https://lucid.app/lucidchart/2d7e6938-bb10-4799-b251-3f24b5d9584f/edit?page=0_0#?folder_id=home&browser=icon  
- *First Class Diagram [old] :* https://app.creately.com/diagram/KlDGg7FOAVr/edit
- *Second Class Diagram [old] :* https://app.creately.com/diagram/V171HVeEYau/edit

