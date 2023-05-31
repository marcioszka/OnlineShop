using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDaoDb : IProductDao
    {
        private readonly string _connectionString = "Server=LAPTOP-ETC7SMLE\\MSSQLSERVER2019;Database=ShopCodecool;Trusted_Connection=True;TrustServerCertificate=True;";
        
        private List<Product> _data = new List<Product>();

        private static ProductDaoDb _instance;

        private ProductDaoDb()
        {
            //_connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static ProductDaoDb GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ProductDaoDb();
            }

            return _instance;
        }

        public void Add(Product item)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                var insertProductSql =
                    @"
                    INSERT INTO product (name, description, currency, default_price, product_category, supplier)
                    VALUES (@Name, @Description, @Currency, @DefaultPrice, @ProductCategory, @Supplier);

                    SELECT SCOPE_IDENTITY();
                    ";

                command.CommandText = insertProductSql;
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Currency", item.Currency);
                command.Parameters.AddWithValue("@DefaultPrice", item.DefaultPrice);
                command.Parameters.AddWithValue("@ProductCategory", item.ProductCategory);
                command.Parameters.AddWithValue("@Supplier", item.Supplier);

                var itemId = Convert.ToInt32(command.ExecuteScalar());
                item.Id = itemId;
            }
            catch (SqlException exception)
            {
                throw;
            }
        }

        public void Remove(int id)
        {
            //data.Remove(this.Get(id));
        }

        public Product Get(int id)
        {
            return _data.Find(x => x.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                var selectProductsSql =
                    @"
                    SELECT name, description, currency, default_price, product_category, supplier
                    FROM product;
                    ";

                command.CommandText = selectProductsSql;
                
                using var reader = command.ExecuteReader();
                var data = new List<Product>();

                while (reader.Read())
                {
                    var name = (string)reader["name"];
                    var description = (string)reader["description"];
                    var currency = (string)reader["currency"];
                    var defaultPrice = (decimal)reader["default_price"];
                    var productCategory = (string)reader["product_category"];
                    var productSupplier = (string)reader["supplier"];

                    var category = new ProductCategory() { Name=productCategory};
                    var supplier = new Supplier() { Name = productSupplier };

                    var product = new Product() { Name = name, Description = description, Currency = currency, DefaultPrice = defaultPrice, ProductCategory = category, Supplier = supplier };
                    data.Add(product);
                }

                return data;
            }
            catch (SqlException exception)
            {
                throw;
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

                var selectProductsSql =
                    @"
                    SELECT id, name, description, currency, default_price, product_category
                    FROM product
                    WHERE supplier=@Supplier;
                    ";

                command.CommandText = selectProductsSql;
                command.Parameters.AddWithValue("@Supplier", supplier.Name);

                using var reader = command.ExecuteReader();
                var data = new List<Product>();

                while (reader.Read())
                {
                    var id = (int)reader["id"];
                    var name = (string)reader["name"];
                    var description = (string)reader["description"];
                    var currency = (string)reader["currency"];
                    var defaultPrice = (decimal)reader["default_price"];
                    var productCategory = (string)reader["product_category"];

                    var category = new ProductCategory() { Name = productCategory };

                    var product = new Product() { Id = id, Name = name, Description = description, Currency = currency, DefaultPrice = defaultPrice, ProductCategory = category, Supplier = supplier };
                    data.Add(product);
                }

                return data;
            }
            catch (SqlException exception)
            {
                throw;
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

                var selectProductsSql =
                    @"
                    SELECT id, name, description, currency, default_price, supplier
                    FROM product
                    WHERE product_category=@ProductCategory;
                    ";

                command.CommandText = selectProductsSql;
                command.Parameters.AddWithValue("@ProductCategory", productCategory.Name);

                var data = new List<Product>();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = (int)reader["id"];
                    var name = (string)reader["name"];
                    var description = (string)reader["description"];
                    var currency = (string)reader["currency"];
                    var defaultPrice = (decimal)reader["default_price"];
                    var productSupplier = (string)reader["supplier"];

                    var supplier = new Supplier() { Name = productSupplier };

                    var product = new Product() { Id = id, Name = name, Description = description, Currency = currency, DefaultPrice = defaultPrice, ProductCategory = productCategory, Supplier = supplier };
                    data.Add(product);
                }

                return data;
            }
            catch (SqlException exception)
            {
                throw;
            }
        }
    }
}
