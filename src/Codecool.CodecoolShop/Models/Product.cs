using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Codecool.CodecoolShop.Models
{
    [Serializable]
    public class Product : BaseModel
    {

        public string Currency { get; set; }
        public decimal DefaultPrice { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public Supplier Supplier { get; set; }
        public CartProduct CartProduct { get; set; }

        public void SetProductCategory(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
            ProductCategory.Products.Add(this);
        }
    }
}
