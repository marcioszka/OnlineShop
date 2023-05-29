using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class CartService
    {
        private readonly IOrderDao orderDaoDB;
        //private readonly ILineItemDao lineItemDao;

        public CartService(IOrderDao orderDaoDB)
        {
            this.orderDaoDB = orderDaoDB;
        }

        public void SaveOrder(Order order)
        {
            this.orderDaoDB.AddOrderDetails(order);
            this.orderDaoDB.Add(order);
        }

        public IEnumerable<LineItem> GetAllOrders(int id) => this.orderDaoDB.GetOrderDetails(id);
    }
}
