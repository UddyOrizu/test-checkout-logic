using Checkout.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Core.Services
{
    public interface ICheckoutService
    {
        Task<CartViewModel> GetBasket();
        Task<CartViewModel> AddToBasket(string sku);
    }
}
