using Codecool.CodecoolShop.Daos.Implementations;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class Order
    {
        private static int idCounter = 0;
        public int Id { get; set; }
        public List<LineItem> Items { get; set; }

        private static Order instance = null;

        public Order(string name, decimal price) 
        {
            this.Id = System.Threading.Interlocked.Increment(ref idCounter);
            this.Items = new List<LineItem>{ new LineItem(name, price)  };
        }
       
        public static Order GetInstance(string name, decimal price)
        {
            if (instance == null)
            {
                instance = new Order(name, price);
            }

            return instance;
        }

        public void AddLineItem(string name, decimal price) => this.Items.Add(new LineItem(name, price));


        public override string ToString()
        {
            string orderDetails = "";
            foreach (LineItem item in Items)
            {
                orderDetails += $"{item.Name}-{item.Quantity}-{item.Price};";
            }
            return orderDetails;
        }
    }
}
