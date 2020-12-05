using Checkout.Core.Domain;
using Checkout.Core.Queries;
using Checkout.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Core.Handlers.Queries
{
    public class GetProductQueryHandler : RequestHandler<GetProductQuery, Product>
    {
        private readonly ICatalog _productStore;

        public GetProductQueryHandler(ICatalog productStore)
        {
            _productStore = productStore;
        }
        
        protected override Product Handle(GetProductQuery query)
        {
            return _productStore.GetProduct(query.Sku);
        }
    }
}
