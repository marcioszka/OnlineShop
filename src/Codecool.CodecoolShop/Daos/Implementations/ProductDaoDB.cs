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
        private readonly string _connectionString = "Server=LAPTOP-ETC7SMLE\\MSSQLSERVER2019;Database=ShopCodecool;Trusted_Connection=True;TrustServerCertificate=True;";
        
        private List<Product> data = new List<Product>();

        private static ProductDaoDB instance = null;

        private ProductDaoDB()
        {
            //_connectionString = ConfigurationManager.AppSettings["ConnectionString"];
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
                    @"
                    INSERT INTO product (name, description, currency, default_price, product_category, supplier, image_path)
                    VALUES (@Name, @Description, @Currency, @DefaultPrice, @ProductCategory, @Supplier, @ImagePath);

                    SELECT SCOPE_IDENTITY();
                    ";

                command.CommandText = insertProductSql;
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Currency", item.Currency);
                command.Parameters.AddWithValue("@DefaultPrice", item.DefaultPrice);
                command.Parameters.AddWithValue("@ProductCategory", item.ProductCategory);
                command.Parameters.AddWithValue("@Supplier", item.Supplier);
                command.Parameters.AddWithValue("@ImagePath", item.ImagePath);

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
            return data.Find(x => x.Id == id);
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
                    SELECT name, description, currency, default_price, product_category, supplier, image_path
                    FROM product;
                    ";

                command.CommandText = selectProductsSql;
                
                using var reader = command.ExecuteReader();
                List<Product> data = new List<Product>();

                while (reader.Read())
                {
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    string currency = (string)reader["currency"];
                    decimal defaultPrice = (decimal)reader["default_price"];
                    string productCategory = (string)reader["product_category"];
                    string productSupplier = (string)reader["supplier"];
                    string imagePath = (string)reader["image_path"];

                    ProductCategory category = new ProductCategory() { Name=productCategory};
                    Supplier supplier = new Supplier() { Name = productSupplier };

                    var product = new Product() { Name = name, Description = description, Currency = currency, DefaultPrice = defaultPrice, ProductCategory = category, Supplier = supplier, ImagePath = imagePath };
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
                    SELECT id, name, description, currency, default_price, product_category, image_path
                    FROM product
                    WHERE supplier=@Supplier;
                    ";

                command.CommandText = selectProductsSql;
                command.Parameters.AddWithValue("@Supplier", supplier);

                using var reader = command.ExecuteReader();
                List<Product> data = new List<Product>();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    string department = (string)reader["department"];
                    string currency = (string)reader["currency"];
                    decimal defaultPrice = (decimal)reader["default_price"];
                    ProductCategory productCategory = (ProductCategory)reader["product_category"];
                    string imagePath = (string)reader["image_path"];

                    var product = new Product() { Id = id, Name = name, Description = description, Currency = currency, DefaultPrice = defaultPrice, ProductCategory = productCategory, Supplier = supplier, ImagePath = imagePath };
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
                    SELECT id, name, description, currency, default_price, supplier, image_path
                    FROM product
                    WHERE product_category=@ProductCategory;
                    ";

                command.CommandText = selectProductsSql;
                command.Parameters.AddWithValue("@ProductCategory", productCategory);

                using var reader = command.ExecuteReader();
                List<Product> data = new List<Product>();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    string department = (string)reader["department"];
                    string currency = (string)reader["currency"];
                    decimal defaultPrice = (decimal)reader["default_price"];
                    Supplier supplier = (Supplier)reader["supplier"];
                    string imagePath = (string)reader["image_path"];

                    var product = new Product() { Id = id, Name = name, Description = description, Currency = currency, DefaultPrice = defaultPrice, ProductCategory = productCategory, Supplier = supplier, ImagePath = imagePath };
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
