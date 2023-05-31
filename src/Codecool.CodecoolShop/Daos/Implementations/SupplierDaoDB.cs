using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoDb : ISupplierDao
    {
        private readonly string _connectionString = "Server=LAPTOP-ETC7SMLE\\MSSQLSERVER2019;Database=ShopCodecool;Trusted_Connection=True;TrustServerCertificate=True;";
        private static SupplierDaoDb _instance;

        private SupplierDaoDb()
        {
            //_connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static SupplierDaoDb GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SupplierDaoDb();
            }

            return _instance;
        }

        public void Add(Supplier item)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            //TODO: implement adding data to DB
        }

        public void Remove(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            //TODO: implement deleting data from DB
        }

        public Supplier Get(int id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                var selectSupplierSql =
                    @"
                    SELECT name, description
                    FROM supplier
                    WHERE id=@Id;
                    ";

                command.CommandText = selectSupplierSql;
                command.Parameters.AddWithValue("@Id", id);

                using var reader = command.ExecuteReader();
                var item = new Supplier();

                if (reader.Read())
                {
                    var name = (string)reader["name"];
                    var description = (string)reader["description"];


                    item.Id = id;
                    item.Name = name;
                    item.Description = description;
                }
                return item;
            }
            catch (SqlException exception)
            {
                throw;
            }
        }

        public IEnumerable<Supplier> GetAll()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                var selectSuppliersSql =
                    @"
                    SELECT id, name, description
                    FROM supplier;
                    ";

                command.CommandText = selectSuppliersSql;

                using var reader = command.ExecuteReader();
                var data = new List<Supplier>();

                while (reader.Read())
                {
                    var id = (int)reader["id"];
                    var name = (string)reader["name"];
                    var description = (string)reader["description"];
                    

                    var supplier = new Supplier() { Id = id, Name = name, Description = description };
                    data.Add(supplier);
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
