using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Codecool.CodecoolShop.Models
{
    public class Order
    {
        private static int idCounter;
        public int Id { get; set; }
        public string Name { get; set; }
        public int userId { get; set; }
        public List<LineItem> Items { get; set; }

        public decimal Sum { get; set; }

        public int ItemsCount { get; set; }

        public Order(int userId=1) 
        {
            this.Id = System.Threading.Interlocked.Increment(ref idCounter);
            this.userId = userId;
            this.Items = new List<LineItem>();
        }

        public static Order GetInstance() => new Order();

        public void AddLineItem(LineItem item) => this.Items.Add(item);

        public decimal CountSum() => this.Items.Sum(lineItem => lineItem.DefaultPrice * lineItem.Quantity);       
     

        public int CountItems() => this.Items.Sum(lineItem => lineItem.Quantity);
     

        public override string ToString()
        {
            string orderDetails = "";
            foreach (LineItem item in Items)
            {
                orderDetails += $"{item.Name}-{item.Quantity}-{item.DefaultPrice};";
            }
            return orderDetails;
        }
    }
}
