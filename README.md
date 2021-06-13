# **Bovelo-project-group-4** 

#### A software engineering 2 project

## **Project participants:**

- Mohamed AOUSJI 17236
- Antoine SCAVINER 20521
- Nicolas SAMELSON 17288
- Pedro ROQUERO DA COSTA PINTO 17010
- Matthieu FAGET 20517


## **Introduction**

The main goal of this project was to design and develop a software for a bike company : Bovelo.
Making the management easier and increase the organisation efficiency into a company is the most important.
This software was made by a team in "agile" method helped by a versionning software. 
Next you can find the main documents and informations related to this project.

## **1. User stories :**

###  **1.1 Sprint 1 :**  

- As a representative, I want to be able to login to access my account.  
- As a representative, I want to see the list of products on the homepage of the app in order to present it to the bicycle shop.
- As a representative, I want to click on a product to get its details.
- As a representative, I want to choose the color, quantity and size on a product to add it in the cart.
- As a representative, I want to know the price of a product to show it to the bicycle shop.
- As a representative, I want to be able to add, delete, or see a product in my cart to pass the order.
- As a representative, I want to validate, cancel my cart to manage my cart.
- As a representative, I would like to receive a summary of my order in order to be able to receive the information to collect it.

### **1.2 Sprint 2 :**  

- As a production manager, I want to be able to see all the orders to make a production planning.
- As an assembler, I want to see the production planning in order to know what I have to do for the week.
- As an assembler, I want to have access to the available bike parts in order to assemble the bikes.
- As a representative, I want to receive an estimated delivery time in order to inform the bicycle shop when the order will be ready.

###  **1.3 Sprint 3 :**  
-	As a manager, I want to know who is assembling the bike and when he started the production, in order to manage the production schedule according to the building process.
-	As a manager, I want to be able to modify the production schedule, in order to manage the orders.
-	As a manager, I want to know how much bike parts there are in stock, to order the bike parts needed.
-	As a manager, I want to be able to order new bike parts, in order to have always enough parts available to assemble the bikes.

## **2. Classes :**  

The classes in this project are :

- *User :*  
The user can be a Representative, Assembler or Manager. The representative can see the catalog, configure a bike, add to cart and pass orders. The assembler can see the plannings and change the state of a bike within the planning(New/Active/Closed). The Manager is able to do the same as the representative, and also create plannings, manage orders (bikes and bike parts), add new users, and create new bike models and bike parts.

- *BikePart :*  
It's regrouping every part needed to build a Bike. It also contains the location of each part, the price, the quantity in stock and the time to assemble it.

- *BikeModel :*  
The BikeModel is a class that contains the BikePart objects require to build a certain type of bike, with a colour and size. The price and time to build are also dependent of the parts.

- *Bike :*  
A bike is built with a BikeModel object. It takes the properties from the BikeModel and also others like a startBuildingTime and endBuildingTime.

- *Item :*  
The Item is a class that has a price and quantity properties.

- *ItemPart :*  
The ItemPart is a class Inheriting from Item and has a Part object. The price is calculated by multiplying the quantity with the price of the Part.
  
- *ItemBike :*  
The ItemBike is a class Inheriting from Item and has a Bike object. The price is calculated by multiplying the quantity with the price of the Bike.
  
- *OrderBike :*  
A class to create orders of Bike objects. An OrderBike object has at least 1 Bike in it. 

- *OrderBikePart :*  
A class to create orders of bikePart objects.

- *Planning :*  
A class that lists the Bike objects to assemble for the week. The object is created by the Manager and the state of Bike Objects in it can be updated by the Assemblers.



## **3. Diagrams, links and schematics :**  

### - **3.1. Diagrams :**  
- *User activity Diagram :* https://drive.google.com/file/d/1Gk_sozGZPf4hDu128_76lsnuZxIa-klZ/view  
- *Sequence Diagram :*
- *Use cases Diagram :* https://lucid.app/lucidchart/invitations/accept/inv_303171e2-174d-4240-b514-e82913fbebbc?viewport_loc=-1198%2C-177%2C4358%2C2150%2C0_0
- *Class Diagram :* https://app.creately.com/diagram/ET0DnpMZXqP/edit
- *Relational Diagram :*

### - **3.2. Links :**  
- *Design brief :* https://claco.ecam.be/apiv2/resource_file79568/raw
- *Azure Devops :* https://dev.azure.com/ECAM3BE/Groupe4  
- *Bike Parts List:* https://ecambxl-my.sharepoint.com/:t:/g/personal/17010_ecam_be/EaTMBrHmODJBg-rELj5LX1MB87OP6OkHwTrc_O8-oE5qKg?e=kAzBUZ
- *PowerPoint:* https://ecambxl-my.sharepoint.com/:p:/r/personal/17288_ecam_be/_layouts/15/Doc.aspx?sourcedoc=%7BD11F69A1-7A7A-43C3-98D8-35060BAB31A7%7D&file=Presentation.pptx&action=edit&mobileredirect=true


### - **3.3. Mock-ups :**  
- *Mockup sprint 1 :* https://drive.google.com/file/d/1E008b_3q2edldHUvpJ3NwuaSqwDhu-G8/view?usp=sharing
- *Mockup sprint 2 :* https://drive.google.com/file/d/1Z4jrSf73k9zW0ssS0kXVY-IMI5MD1hKv/view?usp=sharing
- *Mockup sprint 3 :* https://drive.google.com/file/d/1DUMMbBp80TinfHncI6ZJqvvj3tsIi1QV/view?usp=sharing



