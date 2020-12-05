using Checkout.Core.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.Core.Queries
{
    public class GetProductQuery: IRequest<Product>
    {
        public string Sku { get; set; }
    }
}
