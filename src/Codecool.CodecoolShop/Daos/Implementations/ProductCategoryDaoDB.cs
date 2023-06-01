using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductCategoryDaoDb : IProductCategoryDao
    {
    private readonly string _connectionString = "Server=localhost;Database=ShopCodecool;User Id=sa;Password=@Sdf1234;TrustServerCertificate=True;";
    private static ProductCategoryDaoDb _instance;


        private ProductCategoryDaoDb()
        { 
            _instance = Instance;
        }

        private static ProductCategoryDaoDb Instance { get; } = new();

        public static ProductCategoryDaoDb GetInstance()
        {
            if (_instance != null) return _instance;
            _instance = Instance;

            return _instance;
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
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
    
            var selectCategorySql =
                $@"{"ARG"}SELECT name, description, department FROM category WHERE id = @Id; ";

            command.CommandText = selectCategorySql;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (!reader.Read()) return null; // Return null if no matching category found
            var name = (string)reader["name"];
            var description = (string)reader["description"];
            var department = (string)reader["department"];

            return new ProductCategory
            {
                Id = id,
                Name = name,
                Description = description,
                Department = department
            };
        }


        public IEnumerable<ProductCategory> GetAll()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                var selectCategoriesSql =
                    $@"{"ARG"}
                    SELECT id, name, description, department
                    FROM category;
                    ";

                command.CommandText = selectCategoriesSql;

                using var reader = command.ExecuteReader();
                var data = new List<ProductCategory>();

                while (reader.Read())
                {
                    var id = (int)reader["id"];
                    var name = (string)reader["name"];
                    var description = (string)reader["description"];
                    var department = (string)reader["department"];


                    var category = new ProductCategory() { Id = id, Name = name, Description = description, Department = department };
                    data.Add(category);
                }

                return data;
            }
            catch (SqlException exception)
            {
                if (exception != null) throw;
            }
            return null;
        }
    }
}
