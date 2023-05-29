namespace Codecool.CodecoolShop.Models
{
    public class LineItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public LineItem(string name, decimal price, int quantity=1, int id=1) 
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
