using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ShoppingBasket.Discounting;
using ShoppingBasket.Item;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class MultiplePurchaseDiscountTests
    {
        [Test]
        public void InvalidDiscount()
        {
            try
            {
                var discount = new MultiplePurchaseDiscount(0, "boom!", new PercentageDiscount("O", 5));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.That(ex.Message.Contains("At least one qualifying item is required"));
                return;
            }

            Assert.Fail("Expected exception not thrown");
        }

        [Test]
        public void NoQualifyingItems()
        {
            var discount = new MultiplePurchaseDiscount(1, "X", new PercentageDiscount("O", 25));
            Assert.IsEmpty(discount.FindDiscountable(new List<ItemName>{ "O" }));
        }
        [Test]
        public void NoDiscountable()
        {
            var discount = new MultiplePurchaseDiscount(1, "X", new PercentageDiscount("O", 25));
            Assert.IsEmpty(discount.FindDiscountable(new List<ItemName> { "X" }));
        }

        [Test]
        public void TooFewQualifyingItems()
        {
            var discount = new MultiplePurchaseDiscount(2, "X", new PercentageDiscount("O", 25));
            Assert.IsEmpty(discount.FindDiscountable(new List<ItemName> { "X", "O" }));
        }

        [Test]
        public void QualifyingItems()
        {
            var discount = new MultiplePurchaseDiscount(2, "X", new PercentageDiscount("O", 25));
            Assert.AreEqual(1, discount.FindDiscountable(new List<ItemName> { "O", "X", "X" }).Count());
            Assert.AreEqual(0.75m, discount.FindDiscountable(new List<ItemName> { "O", "X", "X" }).Single().Percentage);
            Assert.AreEqual(25, discount.FindDiscountable(new List<ItemName> { "O", "X", "X" }).Single().PercentageOff);
        }

        [Test]
        public void IrregularQualifyingItems()
        {
            var discount = new MultiplePurchaseDiscount(2, "X", new PercentageDiscount("O", 25));
            Assert.AreEqual(1, discount.FindDiscountable(new List<ItemName> { "X", "O", "X", "X" }).Count());
        }

        [Test]
        public void MultipleQualifyingItemsTooFewDiscountable()
        {
            var discount = new MultiplePurchaseDiscount(2, "X", new PercentageDiscount("O", 25));
            Assert.AreEqual(1, discount.FindDiscountable(new List<ItemName> { "X", "O", "X", "X", "X" }).Count());
        }

        [Test]
        public void MultipleQualifyingItemsMultipleDiscountable()
        {
            var discount = new MultiplePurchaseDiscount(2, "X", new PercentageDiscount("O", 25));
            Assert.AreEqual(2, discount.FindDiscountable(new List<ItemName> { "X", "O", "X", "X", "X", "O", "O" }).Count());
        }
    }
}