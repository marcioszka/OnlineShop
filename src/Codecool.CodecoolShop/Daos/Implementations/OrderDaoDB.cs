using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDaoDB : IOrderDao
    {
        private readonly string _connectionString = "Server=LAPTOP-ETC7SMLE\\MSSQLSERVER2019;Database=ShopCodecool;Trusted_Connection=True;TrustServerCertificate=True;";

        private List<Order> data = new List<Order>();

        private static OrderDaoDB instance = null;

        private OrderDaoDB()
        {
            //_connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public static OrderDaoDB GetInstance()
        {
            if (instance == null)
            {
                instance = new OrderDaoDB();
            }

            return instance;
        }

        public void Add(Order item)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string insertOrderSql =
                    @"
                    INSERT INTO orders (id, userID)
                    VALUES (@Id, @UserID);
                    ";

                command.CommandText = insertOrderSql;
                command.Parameters.AddWithValue("@Id", item.Id);
                command.Parameters.AddWithValue("@UserID", 1);
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }
        
        public void Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public Order Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void AddOrderDetails(Order order)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                foreach(var item in order.Items)
                {
                    string insertOrderDetailsSql =
                    @"
                    INSERT INTO line_item (id, name, price, quantity, order_id)
                    VALUES (@Id, @Name, @Price, @Quantity, @Order_id);
                    ";

                    command.CommandText = insertOrderDetailsSql;
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Price", item.Price);
                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command.Parameters.AddWithValue("@Order_id", order.Id);
                }
            }
            catch (SqlException exception)
            {
                throw exception;
            }
        }

        public List<LineItem> GetOrderDetails(int orderId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                string selectOrderDetailsSql =
                    @"
                    SELECT id, name, price, quantity
                    FROM line_item
                    WHERE order_id=@Order_id;
                    ";

                command.CommandText = selectOrderDetailsSql;
                command.Parameters.AddWithValue("@Order_id", orderId);

                using var reader = command.ExecuteReader();
                List<LineItem> data = new List<LineItem>();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    decimal price = (decimal)reader["price"];
                    int quantity = (int)reader["quantity"];

                    LineItem item = new LineItem(name, price, quantity) { Id = id };

                    data.Add(item);
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
