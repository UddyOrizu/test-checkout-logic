using Checkout.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Core.Services
{
    public interface  ICatalog
    {
        Product GetProduct(string sku);
    }
}
