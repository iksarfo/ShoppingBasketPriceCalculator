using System;
using NUnit.Framework;
using ShoppingBasket.Discounting;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class PercentageDiscountTests
    {
        [Test]
        public void CalculateDiscount()
        {
            var discount = new PercentageDiscount("Cake", 30);
            Assert.AreEqual(0.7m, discount.Value);
        }

        [Test]
        public void NoDiscount()
        {
            var none = new PercentageDiscount("Toffee", 0);
            Assert.AreEqual(1.0m, none.Value);
        }

        [Test]
        public void FreeItem()
        {
            var none = new PercentageDiscount("Toffee", 100);
            Assert.AreEqual(0.0m, none.Value);
        }

        [Test]
        public void LowerLimit()
        {
            try
            {
                var invalid = new PercentageDiscount("Grapes", -1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.True(ex.Message.Contains("Grapes may not have -1% discount applied"));
                return;
            }

            Assert.Fail("Expected exception not thrown");
        }

        [Test]
        public void UpperLimit()
        {
            try
            {
                var invalid = new PercentageDiscount("Grapes", 101);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.True(ex.Message.Contains("Grapes may not have 101% discount applied"));
                return;
            }

            Assert.Fail("Expected exception not thrown");
        }
    }
}