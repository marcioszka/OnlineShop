using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    [Serializable]
    public class Order
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string OrderStatus { get; set; }
        //[Required, MinLength(3, ErrorMessage = "First Name must contain at least 3 characters")]
        public string FirstName { get; set; }
        //[Required, MinLength(3, ErrorMessage = "Last Name must contain at least 3 characters")]
        public string LastName { get; set; }
        //[Required]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
         //ErrorMessage = "Invalid email format")]
        public string ClientEmail { get; set; }
        //[Required]
        //[RegularExpression(@"[A-Za-z0-9'\.\-\s\,]",
        //ErrorMessage = "Invalid address format")]
        public string ClientAddress { get; set; }
        //[Required]
        //[RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}",
         //ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }
        //[Required]
        //[RegularExpression(@"/[a-zA-Z]{2,}/gm",
         //ErrorMessage = "Invalid country format")]
        public string Country { get; set; }
        //[Required]
        //[RegularExpression(@"^([a-zA-Z\u0080-\u024F]+(?:. |-| |'))*[a-zA-Z\u0080-\u024F]*$",
         //ErrorMessage = "Invalid city format")]
        public string City { get; set; }
        //[Required]
        //[RegularExpression(@"\d{6}",
        //ErrorMessage = "Invalid zip code format")]
        public string ZipCode { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
