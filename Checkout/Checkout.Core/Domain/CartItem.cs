using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout.Core.Domain
{
    public class CartItem
    {
        public string SKU { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public string Discount { get; private set; }

        private List<Offer> _offers;

        public CartItem(string sku, decimal unitprice)
        {
            // simple domain validation            
            if (!string.IsNullOrWhiteSpace(sku.Trim()) && unitprice > 0)
            {

                this.SKU = sku.Trim();
                this.UnitPrice = unitprice;
                this._offers = new List<Offer>();
                SetQuantity(1);

            }
            else
            {
                throw new Exception("Domain Validation error CartItem SKU cant be empty and Unit price must be > 0");
            }
        }

        public CartItem(string sku, decimal unitprice, List<Offer> offers)
        {
            // simple domain validation            
            if (!string.IsNullOrWhiteSpace(sku.Trim()) && unitprice > 0)
            {
                
                this.SKU = sku.Trim();
                this.UnitPrice = unitprice;
                this._offers = offers;
                SetQuantity(1);
                
            }
            else
            {
                throw new Exception("Domain Validation error CartItem SKU cant be empty and Unit price must be > 0");
            }
        }

        //Assumptions here is that you can only have 1 offer applied (Best Offer)
        private void ApplyOffer()
        {
            if (_offers.Any() && Quantity > 0 && Price > 0)
            {
                var price = Price;
                var bestOffer = string.Empty;

                foreach (var offer in this._offers)
                {
                    var newprice = 0.0m;

                    switch (offer.OfferType)
                    {
                        case OfferType.MultiBuy:
                            newprice = processMultiBuy(offer, newprice);
                            break;
                        case OfferType.PecentageOff:
                            newprice = processPecentageOff(offer);
                            break;
                        case OfferType.AmountOff:
                            newprice = Price - offer.Value;
                            break;

                    }

                    if (price > newprice && newprice > 0.0m)
                    {                        
                        price = newprice;
                        bestOffer = offer.Name;
                    }

                }

                this.Price = price;
                this.Discount = bestOffer;
            }
        }

        

        public void SetQuantity(int quantity)
        {
            this.Quantity = quantity;
            this.Price = this.UnitPrice * this.Quantity;
            ApplyOffer();
        }

        private decimal processMultiBuy(Offer offer, decimal newprice)
        {
            if (Quantity >= offer.Quantity)
            {
                var instances = Quantity % offer.Quantity;
                if (instances == 0)
                {
                    newprice = offer.Value * (Quantity / offer.Quantity);
                }
                else
                {
                    var unaffectedPrices = this.UnitPrice * instances;
                    var affectedPrices = ((Quantity - instances) / offer.Quantity) * offer.Value;
                    newprice = unaffectedPrices + affectedPrices;
                }
            }

            return newprice;
        }

        private decimal processPecentageOff(Offer offer)
        {
            decimal newprice = (offer.Value / 100) * Price;
            newprice = Price - newprice;
            return newprice;
        }
    }
}
