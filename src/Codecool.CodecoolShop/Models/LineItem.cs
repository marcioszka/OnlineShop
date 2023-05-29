namespace Codecool.CodecoolShop.Models
{
    public class LineItem
    {
        private static int idCounter = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public LineItem(string name, decimal price, int quantity=1) 
        {
            this.Id = System.Threading.Interlocked.Increment(ref idCounter);
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
