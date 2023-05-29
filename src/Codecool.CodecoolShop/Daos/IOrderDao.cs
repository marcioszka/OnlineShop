using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos
{
    public interface IOrderDao : IDao<Order>
    {
        public void AddOrderDetails(Order item);

        public List<LineItem> GetOrderDetails(Order item);
    }
}
