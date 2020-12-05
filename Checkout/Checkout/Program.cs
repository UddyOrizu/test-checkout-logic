using Checkout.Core.Domain;
using Checkout.Core.Handlers.Queries;
using Checkout.Core.Queries;
using Checkout.Core.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Runtime.Caching;

namespace Checkout
{
    class Program
    {
        static int tableWidth = 73;
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //initalize basket in memory. 
            ObjectCache cache = MemoryCache.Default;
            cache.Add("basket", new Cart(), null);

            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .CreateLogger();

            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            var checkout = serviceProvider.GetService<ICheckoutService>();
            Console.WriteLine("Welcome of Checkout");
            Console.WriteLine("We have the following products");
            Console.WriteLine("A99");
            Console.WriteLine("B15");
            Console.WriteLine("C40");
            Console.WriteLine("Scan is your item? Example type A99 or B15 or C40");
            var answer = "";
            while (true)
            {
                try
                {
                    answer = Console.ReadLine();

                    await checkout.AddToBasket(answer.ToUpper().Trim());

                    var basket = await checkout.GetBasket();

                    if (basket.Items.Count > 0)
                    {

                        PrintLine();
                        PrintRow("SKU", "Quantity", "Unit Price", "Sub-Total");
                        PrintLine();

                        foreach (var item in basket.Items)
                        {
                            PrintRow(item.SKU, item.Quantity.ToString(), "£" + item.UnitPrice.ToString("0.00"), "£" + item.Price.ToString("0.00"));
                            if (!string.IsNullOrWhiteSpace(item.Discount))
                            {
                                PrintRow("", "", "", item.Discount);
                            }

                        }
                        PrintLine();
                        PrintRow("", "", "Total", "£" + basket.TotalPrice.ToString("0.00"));
                        PrintLine();
                    }

                    Console.WriteLine("Scan is your item? Example type A99 or B15 or C40");
                }
                catch (Exception ex)
                {
                    Log.Information(ex.Message);
                }
            }




        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging(configure => configure.AddSerilog());

            services.AddOptions();
            services.AddMediatR(typeof(GetProductQuery).Assembly, typeof(GetProductQueryHandler).Assembly);
            services.AddTransient<ICatalog, Catalog>();
            services.AddTransient<ICheckoutService, CheckoutService>();

            return services;
        }


        #region presntation
        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        #endregion
    }
}
