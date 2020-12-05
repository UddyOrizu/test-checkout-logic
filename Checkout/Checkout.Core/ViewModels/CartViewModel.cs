using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Core.ViewModels
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CartItemViewModel
    {
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Discount { get; set; }
    }
}
