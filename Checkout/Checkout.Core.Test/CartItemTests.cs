using Checkout.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Core.Test
{
    [TestClass]
    public class CarItemTest
    {
        private Product _product;

        [TestInitialize]
        public void Setup()
        {
            _product = new Product("A99", 0.5m);
            _product.AddOffer("A99 Multi Buy", 3, 1.3m, OfferType.MultiBuy);
          
        }

        [TestMethod]
        [TestCategory("CartItem.Domain.Tests")]
        public void CartItem_Valid_Should_Successed()
        {
            var sut = new CartItem("A99", 0.5m);            

            Assert.AreEqual(sut.SKU, "A99");
            Assert.AreEqual(0.5m, sut.UnitPrice);
            Assert.AreEqual(0.5m, sut.Price);
        }

        [TestMethod]
        [TestCategory("CartItem.Domain.Tests")]
        public void CartItem_Valid_Offer_Should_Successed()
        {         

            var sut = new CartItem("A99", 0.5m, _product.Offers.ToList());
            Assert.AreEqual(sut.SKU, "A99");
            Assert.AreEqual(0.5m, sut.UnitPrice);
            Assert.AreEqual(0.5m, sut.Price);
        }

        [TestMethod]
        [TestCategory("CartItem.Domain.Tests")]
        public void CartItem_Valid_Offer_Apply_Should_Successed()
        {
            var sut = new CartItem("A99", 0.5m, _product.Offers.ToList());
            sut.SetQuantity(3);

            Assert.AreEqual(sut.SKU, "A99");
            Assert.AreEqual(0.5m, sut.UnitPrice);
            Assert.AreEqual(1.3m, sut.Price);
        }

        [TestMethod]
        [TestCategory("CartItem.Domain.Tests")]
        public void CartItem_Valid_Offer_Apply_set_Should_Successed()
        {
            var sut = new CartItem("A99", 0.5m, _product.Offers.ToList());
            sut.SetQuantity(6);

            Assert.AreEqual(sut.SKU, "A99");
            Assert.AreEqual(0.5m, sut.UnitPrice);
            Assert.AreEqual(2.6m, sut.Price);
        }

        [TestMethod]
        [TestCategory("CartItem.Domain.Tests")]
        public void CartItem_Valid_Offer_NotApply_Should_Successed()
        {
            var sut = new CartItem("A99", 0.5m, _product.Offers.ToList());
            sut.SetQuantity(2);

            Assert.AreEqual(sut.SKU, "A99");
            Assert.AreEqual(0.5m, sut.UnitPrice);
            Assert.AreEqual(1.0m, sut.Price);
        }

        [TestMethod]
        [TestCategory("CartItem.Domain.Tests")]
        public void CartItem_Valid_Offer_Apply_with_extra_item_Should_Successed()
        {
            var sut = new CartItem("A99", 0.5m, _product.Offers.ToList());
            sut.SetQuantity(4);

            Assert.AreEqual(sut.SKU, "A99");
            Assert.AreEqual(0.5m, sut.UnitPrice);
            Assert.AreEqual(1.8m, sut.Price);
        }

        [TestMethod]
        [TestCategory("CartItem.Domain.Tests")]
        public void CartItem_Valid_BestOffer_Should_Successed()
        {

            _product.AddOffer("A99 Pecentage Off", 0, 0.2m, OfferType.PecentageOff);
            _product.AddOffer("A99 Amount Off", 0, 1.0m, OfferType.AmountOff);

            var sut = new CartItem("A99", 0.5m, _product.Offers.ToList());

            sut.SetQuantity(4);

            Assert.AreEqual(sut.SKU, "A99");
            Assert.AreEqual(0.5m, sut.UnitPrice);
            Assert.AreEqual(1.0m, sut.Price);
            Assert.AreEqual("A99 Amount Off", sut.Discount);
        }
    }
}
