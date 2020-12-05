using Checkout.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Checkout.Core.Test
{
    [TestClass]
    public class ProductTest
    {
             

        


        [TestMethod]
        [TestCategory("Product.Domain.Tests")]
        public void Product_Valid_Sku_Unitprice_Should_Successed()
        {
            var sut = new Product("A99", 0.5m);

            Assert.AreEqual(sut.SKU, "A99");  
            Assert.AreEqual(sut.UnitPrice, 0.5m);
        }

        [TestMethod]
        [TestCategory("Product.Domain.Tests")]
        public void Product_InValid_Sku_Should_Fail()
        {           
            Assert.ThrowsException<Exception>(() => new Product(string.Empty, 0.5m));
        }

        [TestMethod]
        [TestCategory("Product.Domain.Tests")]
        public void Product_InValid_Unitprice_Should_Fail()
        {
            Assert.ThrowsException<Exception>(() => new Product("A99", 0.0m));
        }

        [TestMethod]
        [TestCategory("Product.Domain.Tests")]
        public void Product_InValid_Sku_Unitprice_Should_Fail()
        {
            Assert.ThrowsException<Exception>(() => new Product(string.Empty, 0.0m));
        }

        [TestMethod]
        [TestCategory("Product.Domain.Tests")]
        public void Product_Valid_Offer_Should_Successed()
        {
            var sut = new Product("A99", 0.5m);
            sut.AddOffer("A99 Multi Buy", 3, 1.3m, OfferType.MultiBuy);
            sut.AddOffer("A99 Pecentage Off", 0, 0.2m, OfferType.PecentageOff);

            Assert.AreEqual(sut.SKU, "A99");
            Assert.AreEqual(2, sut.Offers.Count);
            Assert.AreEqual(sut.UnitPrice, 0.5m);
        }

        [TestMethod]
        [TestCategory("Product.Domain.Tests")]
        public void Product_InValid_Offer_Should_Successed()
        {
            var sut = new Product("A99", 0.5m);
            sut.AddOffer("A99 Multi Buy", 3, 1.3m, OfferType.MultiBuy);

            Assert.AreEqual(sut.SKU, "A99");
            Assert.AreEqual(1, sut.Offers.Count);
            Assert.ThrowsException<Exception>(() => sut.AddOffer("A99 Multi Buy", 2, 1.3m, OfferType.MultiBuy));
        }
    }
}
