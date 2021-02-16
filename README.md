# **Bovelo-project-group-4**  

Software engineering 2 project


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
- *Design brief :* https://claco.ecam.be/apiv2/resource_file79568/raw
- *Azure Devops :* https://dev.azure.com/ECAM3BE/Groupe4  

### **2.3. Schematics :**  
- *Interface mockup :*
![](Mockup Bovelo Itération 1.png)


## **3. Old links :**  

- *First Diagrams [old] :* https://lucid.app/lucidchart/2d7e6938-bb10-4799-b251-3f24b5d9584f/edit?page=0_0#?folder_id=home&browser=icon  
- *First Class Diagram [old] :* https://app.creately.com/diagram/KlDGg7FOAVr/edit
- *Second Class Diagram [old] :* https://app.creately.com/diagram/V171HVeEYau/edit

