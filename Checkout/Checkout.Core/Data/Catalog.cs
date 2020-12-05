using Checkout.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkout.Core.Services
{
    public class Catalog : ICatalog
    {

        public Product GetProduct(string sku)
        {

            var products = new List<Product>();

            var p1 = new Product("A99", 0.5m);
            p1.AddOffer("A99 Multi Buy", 3, 1.3m, OfferType.MultiBuy);
            products.Add(p1);
            var p2 = new Product("B15", 0.3m);
            p2.AddOffer("B15 Multi Buy", 2, 0.45m, OfferType.MultiBuy);
            products.Add(p2);
            var p3 = new Product("C40", 0.6m);            
            products.Add(p3);

            return products.FirstOrDefault(x => x.SKU == sku);            
        }
    }
}
