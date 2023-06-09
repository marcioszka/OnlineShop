using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    [Serializable]
    public class OrderProduct
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
