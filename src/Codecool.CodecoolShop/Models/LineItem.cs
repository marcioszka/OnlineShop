﻿using System.Threading;

namespace Codecool.CodecoolShop.Models
{
    public class LineItem : Product
    {
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public LineItem(int id)
        {
            Id = id;
        }

        public decimal CountPrice() => this.Quantity * this.DefaultPrice;

        public override string ToString()
        {
            return $"{this.Name} - {Quantity} * {this.DefaultPrice} {this.Currency} - {this.CountPrice()}";
        }
    }
}
