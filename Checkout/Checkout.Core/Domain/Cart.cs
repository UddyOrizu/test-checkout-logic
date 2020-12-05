using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout.Core.Domain
{
    public class Cart
    {
        private List<CartItem> _items;
        public IReadOnlyCollection<CartItem> Items => _items;

        public Cart()
        {
            _items = new List<CartItem>();
        }

        public void Add(CartItem item)
        {
            if (!_items.Any(x => x.SKU == item.SKU))
            {
                _items.Add(item);
            }
            else
            {
                var cartItem = _items.FirstOrDefault(x => x.SKU == item.SKU);
                cartItem.SetQuantity(cartItem.Quantity + 1);
            }
        }

        public decimal Total
        {
            get
            {
                return _items.Sum(x => x.Price);
            }
        }
    }
}
