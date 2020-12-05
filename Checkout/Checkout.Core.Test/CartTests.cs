using Checkout.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Core.Test
{
    [TestClass]
    public class CarTest
    {
        private Product _product;
        private Product _product2;
        private CartItem _item1;
        private CartItem _item2;

        [TestInitialize]
        public void Setup()
        {
            _product = new Product("A99", 0.5m);
            _product.AddOffer("A99 Multi Buy", 3, 1.3m, OfferType.MultiBuy);           
            

            _item1 = new CartItem("A99", 0.5m, _product.Offers.ToList());
            _item2= new CartItem("D09", 0.5m);

        }

        [TestMethod]
        [TestCategory("Cart.Domain.Tests")]
        public void Cart_Valid_Add_item_Should_Successed()
        {
            var sut = new Cart();

            sut.Add(_item1);
            Assert.AreEqual(0.5m, sut.Total);
            Assert.AreEqual(1, sut.Items.Count);
            
        }

        [TestMethod]
        [TestCategory("Cart.Domain.Tests")]
        public void Cart_Valid_Add_two_items_Should_Successed()
        {
            var sut = new Cart();

            sut.Add(_item1);
            sut.Add(_item2);
            Assert.AreEqual(1.0m, sut.Total);
            Assert.AreEqual(2, sut.Items.Count);

        }

        [TestMethod]
        [TestCategory("Cart.Domain.Tests")]
        public void Cart_Valid_Add_many_items_Should_Successed()
        {
            var sut = new Cart();

            sut.Add(_item1);
            sut.Add(_item2);
            sut.Add(_item1);
            sut.Add(_item1);
            sut.Add(_item2);
            Assert.AreEqual(2.3m, sut.Total);
            Assert.AreEqual(2, sut.Items.Count);

        }


    }
}
