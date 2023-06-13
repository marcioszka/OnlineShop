using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoDB : IProductDao
    {
        private readonly string _connectionString = "Server=localhost;Database=ShopCodecool;User Id=sa;Password=@Sdf1234;TrustServerCertificate=True;";

        private List<Product> data = new List<Product>();

        private static ProductDaoDB instance = null;

        private ProductDaoDB()
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public static ProductDaoDB GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductDaoDB();
            }

            return instance;
        }

        public void Add(Product item)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string insertProductSql =
                    $@"
                    INSERT INTO{"ARG"} product (name, description, currency, default_price, categoryID, supplierID)
                    VALUES (@Name, @Description, @Currency, @DefaultPrice, @ProductCategory, @Supplier);

                    SELECT SCOPE_IDENTITY();
                    ";

                command.CommandText = insertProductSql;
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Currency", item.Currency);
                command.Parameters.AddWithValue("@DefaultPrice", item.DefaultPrice);
                command.Parameters.AddWithValue("@ProductCategory", item.ProductCategory.Id);
                command.Parameters.AddWithValue("@Supplier", item.Supplier.Id);

                int itemId = Convert.ToInt32(command.ExecuteScalar());
                item.Id = itemId;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }

        public void Remove(int id)
        {
            //data.Remove(this.Get(id));
        }

        public Product Get(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectProductSql =
                    $@"
                    SELECT{"ARG0"} product.name as name, product.description as description, product.currency as currency, product.default_price as default_price, category.name as category, supplier.name as supplier
                    FROM product
                    JOIN category ON product.categoryID = category.id
                    JOIN supplier ON product.supplierID = supplier.id
                    WHERE product.id=@Id;
                    ";

                command.CommandText = selectProductSql;

                using var reader = command.ExecuteReader();
                command.Parameters.AddWithValue("@Id", id);
                Product item = new Product();

                if (reader.Read())
                {
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    string currency = (string)reader["currency"];
                    decimal defaultPrice = (decimal)reader["default_price"];
                    string categoryName = (string)reader["category"];
                    string supplierName = (string)reader["supplier"];

                    ProductCategory category = new ProductCategory() { Name = categoryName };
                    Supplier supplier = new Supplier() { Name = supplierName };

                    item.Id = id;
                    item.Name = name;
                    item.Description = description;
                    item.Currency = currency;
                    item.DefaultPrice = defaultPrice;
                    item.ProductCategory = category;
                    item.Supplier = supplier;
                }

                return item;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }


        public IEnumerable<Product> GetAll()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectProductsSql =
                    @"
                    SELECT product.id as id, product.name as name, product.description as description, product.currency as currency, product.default_price as default_price, category.name as category, supplier.name as supplier
                    FROM product
                    JOIN category ON product.categoryID = category.id
                    JOIN supplier ON product.supplierID = supplier.id;
                    ";

                command.CommandText = selectProductsSql;

                using var reader = command.ExecuteReader();
                List<Product> data = new List<Product>();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    string currency = (string)reader["currency"];
                    decimal defaultPrice = (decimal)reader["default_price"];
                    string categoryName = (string)reader["category"];
                    string supplierName = (string)reader["supplier"];

                    ProductCategory category = new ProductCategory() { Name = categoryName };
                    Supplier supplier = new Supplier() { Name = supplierName };

                    var product = new Product() { Id = id, Name = name, Description = description, Currency = currency, DefaultPrice = defaultPrice, ProductCategory = category, Supplier = supplier };
                    data.Add(product);
                }

                return data;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectProductsSql =
                    @"
                    SELECT product.id as id, product.name as name, product.description as description, product.currency as currency, product.default_price as default_price, category.name as category
                    FROM product
                    JOIN category ON product.categoryID = category.id
                    WHERE product.supplierID=@Supplier;
                    ";

                command.CommandText = selectProductsSql;
                command.Parameters.AddWithValue("@Supplier", supplier.Id);

                using var reader = command.ExecuteReader();
                List<Product> data = new List<Product>();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    string currency = (string)reader["currency"];
                    decimal defaultPrice = (decimal)reader["default_price"];
                    string categoryName = (string)reader["category"];

                    ProductCategory category = new ProductCategory() { Name = categoryName };

                    var product = new Product() { Id = id, Name = name, Description = description, Currency = currency, DefaultPrice = defaultPrice, ProductCategory = category, Supplier = supplier };
                    data.Add(product);
                }

                return data;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectProductsSql =
                    @"
                    SELECT product.id as id, product.name as name, product.description as description, product.currency as currency, product.default_price as default_price, supplier.name as supplier
                    FROM product
                    JOIN supplier ON product.supplierID = supplier.id
                    WHERE product.categoryID=@ProductCategory;
                    ";

                command.CommandText = selectProductsSql;
                command.Parameters.AddWithValue("@ProductCategory", productCategory.Id);

                List<Product> data = new List<Product>();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    string currency = (string)reader["currency"];
                    decimal defaultPrice = (decimal)reader["default_price"];
                    string supplierName = (string)reader["supplier"];

                    Supplier supplier = new Supplier() { Name = supplierName };

                    var product = new Product() { Id = id, Name = name, Description = description, Currency = currency, DefaultPrice = defaultPrice, ProductCategory = productCategory, Supplier = supplier };
                    data.Add(product);
                }

                return data;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }
    }
}