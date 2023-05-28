using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDaoDB : ISupplierDao
    {
        private readonly string _connectionString = "Server=LAPTOP-ETC7SMLE\\MSSQLSERVER2019;Database=ShopCodecool;Trusted_Connection=True;TrustServerCertificate=True;";
        private static SupplierDaoDB instance = null;

        private SupplierDaoDB()
        {
            //_connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public static SupplierDaoDB GetInstance()
        {
            if (instance == null)
            {
                instance = new SupplierDaoDB();
            }

            return instance;
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

                string selectSupplierSql =
                    @"
                    SELECT name, description
                    FROM supplier
                    WHERE id=@Id;
                    ";

                command.CommandText = selectSupplierSql;
                command.Parameters.AddWithValue("@Id", id);

                using var reader = command.ExecuteReader();
                Supplier item = null;

                if (reader.Read())
                {
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];


                    item.Id = id;
                    item.Name = name;
                    item.Description = description;
                }
                return item;
            }
            catch (SqlException exception)
            {
                throw exception;
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

                string selectSuppliersSql =
                    @"
                    SELECT id, name, description
                    FROM supplier;
                    ";

                command.CommandText = selectSuppliersSql;

                using var reader = command.ExecuteReader();
                List<Supplier> data = new List<Supplier>();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    string description = (string)reader["description"];
                    

                    Supplier supplier = new Supplier() { Id = id, Name = name, Description = description };
                    data.Add(supplier);
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
