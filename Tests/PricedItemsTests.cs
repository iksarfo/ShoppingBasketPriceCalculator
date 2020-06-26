using System.Linq;
using NUnit.Framework;
using ShoppingBasket.Pricing;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class PricedItemsTests
    {
        private PricedItems _pricedItems;

        [SetUp]
        public void Setup()
        {
            _pricedItems = new PricedItems();
        }

        [Test]
        public void Empty()
        {
            Assert.IsEmpty(_pricedItems);
        }

        [Test]
        public void AddedItem()
        {
            var item = new PricedItem("item", 1.0m);
            _pricedItems.Add(item);

            Assert.AreEqual(1, _pricedItems.Count());
            Assert.AreEqual(item, _pricedItems.Single().Value);
        }
    }
}