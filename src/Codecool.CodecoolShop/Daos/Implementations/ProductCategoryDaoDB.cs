using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductCategoryDaoDB : IProductCategoryDao
    {
        private readonly string _connectionString = "Server=DESKTOP-4F59TUD\\MSQLSERVER2019;Database=ShopCodecool;Trusted_Connection=True;TrustServerCertificate=True;";
        private static ProductCategoryDaoDB instance = null;

        private ProductCategoryDaoDB()
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public static ProductCategoryDaoDB GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductCategoryDaoDB();
            }

            return instance;
        }

        public void Add(ProductCategory item)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            //TODO: implement adding data to DB;
        }

        public void Remove(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            //TODO: implement deleting data from DB
        }

        public ProductCategory Get(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectCategorySql =
                    @"
                    SELECT name, description, department
                    FROM category
                    WHERE id=@Id;
                    ";

                command.CommandText = selectCategorySql;
                command.Parameters.AddWithValue("@Id", id);

                using var reader = command.ExecuteReader();
                ProductCategory item = new ProductCategory() { Id = id };

                if (reader.Read())
                {
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    string department = (string)reader["department"];

                    item.Name = name;
                    item.Description = description;
                    item.Department = department;
                }
                return item;
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectCategoriesSql =
                    @"
                    SELECT id, name, description, department
                    FROM category;
                    ";

                command.CommandText = selectCategoriesSql;

                using var reader = command.ExecuteReader();
                List<ProductCategory> data = new List<ProductCategory>();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    string department = (string)reader["department"];


                    ProductCategory category = new ProductCategory() { Id = id, Name = name, Description = description, Department = department };
                    data.Add(category);
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