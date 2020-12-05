using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout.Core.Domain
{
    public class Product
    {
        public string SKU { get; private set; }
        public decimal UnitPrice { get; private set; }

        public IReadOnlyCollection<Offer> Offers => _offers;

        private List<Offer> _offers;

        public Product(string sku, decimal unitprice)
        {
            // simple domain validation            
            if (!string.IsNullOrWhiteSpace(sku.Trim()) && unitprice > 0)
            {
                this.SKU = sku.Trim();
                this.UnitPrice = unitprice;
                _offers = new List<Offer>();
            }
            else
            {
                throw new Exception("Domain Validation error product SKU cant be empty and Unit price must be > 0");
            }
        }

        public void AddOffer(string name, int quantity, decimal value, OfferType type = OfferType.MultiBuy)
        {
            if (!_offers.Any(x => x.Name.Equals(name.Trim(),StringComparison.OrdinalIgnoreCase)))
            {

                _offers.Add(new Offer(name, quantity, value, type));
            }
            else
            {
                throw new Exception("Offer already exists");
            }
            
        }
    }

    public class Offer
    {
        public string Name { get; private set; }
        public OfferType OfferType { get; private set; }
        public int Quantity { get; private set; }
        public decimal Value { get; private set; }
        public Offer(string name, int quantity, decimal value, OfferType type = OfferType.MultiBuy)
        {
            this.Name = name;
            this.OfferType = type;
            this.Value = value;
            this.Quantity = quantity;
        }
    }

    public enum OfferType
    {
        MultiBuy,
        PecentageOff,
        AmountOff
    }

}
