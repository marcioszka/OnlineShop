using System.Threading;

namespace Codecool.CodecoolShop.Models
{
    public class LineItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public string Currency { get; set; }

        public LineItem(int id)
        {
            Id = id;
        }
    }
}
