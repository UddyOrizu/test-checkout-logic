using Checkout.Core.Commands;
using MediatR;
using System.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Core.Domain;

namespace Checkout.Core.Handlers.Commands
{
    public class AddProductToCartCommandHandler : RequestHandler<AddProductToCartCommand, bool>
    {
        private readonly ObjectCache cache;

        public AddProductToCartCommandHandler()
        {
            cache = MemoryCache.Default;
        }

        protected override bool Handle(AddProductToCartCommand request)
        {
            try
            {
                var basket = (Domain.Cart)cache.Get("basket");

                var item = new CartItem(request.SKU, request.UnitPrice, request.Offers);
                basket.Add(item);

                cache.Set("basket", basket, null);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
