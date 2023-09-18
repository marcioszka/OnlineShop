# Online eCommerce Web Application

This is the README for an online eCommerce web application that allows users to browse products, add them to a shopping cart, and complete the purchase process by making payments. The application is built using ASP.NET Core, Entity Framework, and follows the MVC architecture pattern. It also incorporates the DAO (Data Access Object) design pattern for efficient data access.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Database](#database)
- [License](#license)

## Features

- **Product Catalog**: Browse a wide range of products with detailed descriptions and pricing information.

- **Shopping Cart**: Add products to a shopping cart, view the contents, and adjust quantities as needed.

- **Checkout Process**: A streamlined checkout process that includes shipping and payment information.

- **Search and Filtering**: Search for products and use filters to narrow down choices.

## Technologies

The application is built using the following technologies and design patterns:

- **ASP.NET Core**: The web framework used for building the application.

- **Entity Framework Core**: An ORM (Object-Relational Mapping) for database interactions.

- **MVC Architecture**: The application follows the Model-View-Controller architectural pattern for clear separation of concerns.

- **DAO Design Pattern**: The Data Access Object pattern is employed for efficient and organized data access and manipulation.

- **SQL Database**: The application uses a SQL database to store product, user, and order information.

## Getting Started

To run the application locally, follow these steps:

1. **Clone the Repository**: Clone this repository to your local machine.

2. **Database Configuration**:
   - Install a SQL Server and create a database for the application.
   - Update the database connection string in the `appsettings.json` file.

3. **Entity Framework Migrations**: Run Entity Framework migrations to create the database schema:

```shell
dotnet ef database update
```

4. **Run the Application**:

```shell
dotnet run
```

5. **Access the Application**: Open your web browser and navigate to `http://localhost:5000` to access the eCommerce application.

## Database

The application uses a SQL database to store product information, user accounts, and order history. Entity Framework Core is used to interact with the database, and you can find the database schema defined in the `DbContext` class.

## License

This project is licensed under the [MIT License](LICENSE).
