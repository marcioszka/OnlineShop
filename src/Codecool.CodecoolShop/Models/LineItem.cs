using System.Threading;

namespace Codecool.CodecoolShop.Models
{
    public class LineItem
    {
        private static int idCounter;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //public LineItem(int id)
        //{
        //    Id = id;
        //}

        public LineItem(int id, string name, decimal price, int quantity = 1)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
