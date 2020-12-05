using Checkout.Core.Domain;
using Checkout.Core.Queries;
using Checkout.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout.Core.Handlers.Queries
{
    public class GetCartSummaryQueryHandler : RequestHandler<GetCartSummaryQuery, Cart>
    {
        private readonly ObjectCache cache;

        public GetCartSummaryQueryHandler()
        {
            cache = MemoryCache.Default;
        }
                      

        protected override Cart Handle(GetCartSummaryQuery request)
        {
            return (Domain.Cart)cache.Get("basket");
        }
    }
}
