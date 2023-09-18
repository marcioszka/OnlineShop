using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class CartService : Order
    {
        private readonly IOrderDao orderDaoDB;
        //private readonly ILineItemDao lineItemDao;
        //public Order Order { get; set; }

        public CartService(IOrderDao orderDaoDB)
        {
            this.orderDaoDB = orderDaoDB;
        }

        //public void CreateOrder() => this.Order = new Order();

        public void SaveOrder(Order Order)
        {
            this.orderDaoDB.AddOrderDetails(Order);
            this.orderDaoDB.Add(Order);
        }

        public LineItem GetProductDetails(int id) => this.orderDaoDB.GetProductDetails(id);

        public IEnumerable<LineItem> GetAllOrders(int id) => this.orderDaoDB.GetOrderDetails(id);


    }
}
