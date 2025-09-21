# InventoryManagement

[![.NET](https://img.shields.io/badge/.NET-8.0-blue?logo=dotnet)](https://dotnet.microsoft.com/) 


## Introduction
**InventoryManagement** is a RESTful API for an E-Commerce Inventory System built with **.NET Core 8** and **Entity Framework Core**.  

It allows secure management of products and categories with full CRUD operations, JWT-based authentication and authorization, and optional product image handling.  

The API follows **Domain-Driven Design (DDD)** principles with a layered architecture, ensuring clean, maintainable, and scalable code. Swagger documentation is included for easy testing and integration with front-end applications.

---

## Tech Stack
- **Backend:** .NET Core 8  
- **Architecture:** Domain-Driven Design (DDD) layered  
- **ORM:** Entity Framework Core  
- **Database:** SQL Server
- **Authentication:** JWT  
- **API Docs:** Swagger (Swashbuckle.AspNetCore)   

---

## Features

### Authentication
- **Register:** `POST /api/auth/register` – Create a user with email, password, username  
- **Login:** `POST /api/auth/login` – Returns JWT token  
- JWT-based authorization for all endpoints  

### Product Management
- **Create Product:** `POST /api/products` – Name, description, price, stock, category ID and image  
- **List Products:** `GET /api/products` – Supports filters by category, price range, pagination  
- **Get Single Product:** `GET /api/products/{id}` – Includes category 
- **Update Product:** `PUT /api/products/{id}` – Get product by ID
- **Delete Product:** `DELETE /api/products/{id}`  – Remove product.

- **Search Products:** `GET /api/products/search?q=keyword` – Search products by name/description.

### Category Management
- **Create Category:** `POST /api/categories` – Unique name, description  
- **List Categories:** `GET /api/categories` – Includes product counts  
- **Get Category by ID:** `GET /api/categories/{id}` – Get category by ID.
- **Update Category:** `PUT /api/categories/{id}` – Update category details
- **Delete Category:** `DELETE /api/categories/{id}` – Remove category if no linked products.

---

## Setup Instructions

1. **Clone the repository**  
```bash
https://github.com/Sajjad617/InventoryManagement
cd InventoryManagement
```
2. **Restore NuGet packages**  
```bash
dotnet restore
```
3. **Configure appsettings.json**  
```bash
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "Key": "Wearefromtechstdio.iamsajjadhosenfrombanglades.thanksforgivemethechanch",
    "Issuer": "SajidHosen",
    "Audience": "TechStdio",
    "ExpireMinutes": 60
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DevConnection": "Server=DESKTOP-H90K7JR\\SQLEXPRESS;Database=Inventory;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}

```
4. **Apply migrations and update database**  
```bash
dotnet ef database update
```
5. **Run the project**  
```bash
dotnet run
```
5. **Access Swagger UI for API testing**  
```bash
[dotnet run](https://localhost:7102/swagger/index.html)
```
