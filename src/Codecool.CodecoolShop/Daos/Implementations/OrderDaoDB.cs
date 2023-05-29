using Codecool.CodecoolShop.Models;
using System.Collections.Generic;
using System.Configuration;

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
            throw new System.NotImplementedException();
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
    }
}
