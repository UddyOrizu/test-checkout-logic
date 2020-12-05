using Checkout.Core.Commands;
using Checkout.Core.Domain;
using Checkout.Core.Queries;
using Checkout.Core.ViewModels;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Checkout.Core.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IMediator _dispatcher;
        
        public CheckoutService(IMediator dispatcher)
        {
            _dispatcher = dispatcher;
            
        }
        public async Task<CartViewModel> AddToBasket(string sku)
        {
            var domain_product = await _dispatcher.Send(new GetProductQuery() { Sku = sku.Trim() });
             
            if(domain_product != null)
            {     
                var is_added = await _dispatcher.Send(new AddProductToCartCommand() { SKU = domain_product.SKU, UnitPrice = domain_product.UnitPrice, Offers = domain_product.Offers.ToList() });

                if (!is_added)
                {
                    Log.Information("Failed to Add product to Basket contact admin");
                }
            }
            else
            {
                Log.Information("Product not found contact admin");
            }
            return await GetBasket();

        }

        public async Task<CartViewModel> GetBasket()
        {
            var domain_basket = await _dispatcher.Send(new GetCartSummaryQuery());

            var basket = new CartViewModel();
            basket.Items = new List<CartItemViewModel>();

            foreach (var item in domain_basket.Items)
            {
                basket.Items.Add(new CartItemViewModel
                {
                    Discount = item.Discount,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    SKU = item.SKU,
                    UnitPrice = item.UnitPrice
                });
            }

            basket.TotalPrice = domain_basket.Total;

            return basket;
        }
    }
}
