using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IOrderDao : IDao<Order>
    {
        public void AddOrderDetails(Order order);

        public IEnumerable<LineItem> GetOrderDetails(int id);

        public LineItem GetProductDetails(int id);
    }
}
