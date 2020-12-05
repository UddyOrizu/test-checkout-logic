using Checkout.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Core.Commands
{
    public class AddProductToCartCommand : IRequest<bool>
    {
        public string SKU { get;  set; }
        public decimal UnitPrice { get;  set; }

        public List<Offer> Offers { get; set; }
    }
}
