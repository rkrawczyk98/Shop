# 🧺 **Shop - ASP.NET Core WebAPI Shop Manager** 🥑

### ASP.NET Core WebAPI CRUD application for managing shop.
**Features**

- Onion Architecture
- 3 Independent microservices (CustomersApi, OrdersApi & ProductsApi)
- 1 API that handles all microservices
- Create, read, update and delete Products, Orders and Customers.
- Store and retrieve data using SQL database
- Validation and error handling

**Requirements**

- .NET Core 3.1 or later
- SQL Server

### **Getting Started**

Clone the repository

```
git clone https://github.com/rkrawczyk98/Shop.git
```
    
Navigate to the project directory
```
cd WrenchWorks_-_Car_Service_Manager
```
Restore packages and build the project
```
dotnet restore
dotnet build
```
Ensure that connection strings in all runnable projects are equal to the following one.
```
"DefaultConnection": "Server=.;Database=ShopDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False"
```
Now you have to update your database.
To do so, you have follow the mentioned steps on each Microservice directory.

  [Services Directiories]:
  - Shop.CustomersApi
  - Shop.OrdersApi
  - Shop.ProductsApi

  Step 1. Enter first service directory e.g. "CustomersApi"
  ```
  cd Shop.CustomersApi
  ```
  
  Step 2. Create/Update database
  ```
  dotnet ef database update
  ```
  
  Step 3. Leave service directory
  ```
  cd ..
  ```

  Step 4. Go back to Steps 1 through 4 and repeat for the other services.

Configure runtime configuration to run all projects at once.
Run the application.



