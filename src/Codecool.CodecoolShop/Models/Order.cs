using Codecool.CodecoolShop.Daos.Implementations;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Codecool.CodecoolShop.Models
{
    public class Order
    {
        private static int idCounter;
        public int Id { get; set; }

        public int userId { get; set; }
        public List<LineItem> Items { get; set; }

        //private static Order instance = null;

        public Order(int userId=1) 
        {
            this.Id = System.Threading.Interlocked.Increment(ref idCounter);
            this.userId = userId;
            this.Items = new List<LineItem>();
        }

        //public static Order GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        instance = new Order();
        //    }

        //    return instance;
        //}
        public static Order GetInstance() => new Order();
        //public void AddLineItem(int id, string name, decimal price) => this.Items.Add(new LineItem(id, name, price));

        public void AddLineItem(LineItem item) => this.Items.Add(item);

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
