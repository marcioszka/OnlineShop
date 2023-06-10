namespace Codecool.CodecoolShop.Models
{
    public class Checkout
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
        public bool Status { get; set; }
    }
}
