using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ShoppingBasket.Discounting;
using ShoppingBasket.Output;
using ShoppingBasket.Pricing;
using ShoppingBasket.Writers;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class ReceiptTests
    {
        [Test]
        public void DisplaysCurrencySymbols()
        {
            var mockConsole = new Mock<IConsole>();
            var receipt = new Receipt(mockConsole.Object, "€", "¢");
            var discounts = new List<DiscountApplied> { new DiscountApplied() };
            var calculation = new Calculation(0, discounts, 0);
            receipt.Print(calculation);

            mockConsole.Verify(_ => _.WriteLine(It.Is<string>(text => text.Contains("€"))), Times.Exactly(2));
            mockConsole.Verify(_ => _.WriteLine(It.Is<string>(text => text.Contains("¢"))), Times.Exactly(1));
        }

        [Test]
        public void DisplaysSubtotal()
        {
            var mockConsole = new Mock<IConsole>();
            var receipt = new Receipt(mockConsole.Object, "€", "¢");
            var calculation = new Calculation(2, Enumerable.Empty<DiscountApplied>().ToList(), 1);
            receipt.Print(calculation);

            mockConsole.Verify(_ => _.WriteLine("Subtotal: €2.00"));
        }

        [Test]
        public void DisplaysNoAvailableOffers()
        {
            var mockConsole = new Mock<IConsole>();
            var receipt = new Receipt(mockConsole.Object, "€", "¢");
            var calculation = new Calculation(2, Enumerable.Empty<DiscountApplied>().ToList(), 1);
            receipt.Print(calculation);

            mockConsole.Verify(_ => _.WriteLine("(No offers available)"));
        }

        [Test]
        public void DisplaysTotal()
        {
            var mockConsole = new Mock<IConsole>();
            var receipt = new Receipt(mockConsole.Object, "€", "¢");
            var calculation = new Calculation(2, Enumerable.Empty<DiscountApplied>().ToList(), 1);
            receipt.Print(calculation);

            mockConsole.Verify(_ => _.WriteLine("Total: €1.00"));
        }

        [Test]
        public void DisplaysDiscount()
        {
            var mockConsole = new Mock<IConsole>();
            var receipt = new Receipt(mockConsole.Object, "€", "¢");
            var discounts = new List<DiscountApplied> { new DiscountApplied { Name = "Flour", PercentageOff = 25, AmountOff = 0.25m } };
            var calculation = new Calculation(0, discounts, 0);
            receipt.Print(calculation);

            mockConsole.Verify(_ => _.WriteLine("Flour 25% off: -0.25¢"));
        }

        [Test]
        public void DisplaysDiscounts()
        {
            var mockConsole = new Mock<IConsole>();
            var receipt = new Receipt(mockConsole.Object, "€", "¢");
            var discounts = new List<DiscountApplied> {
                new DiscountApplied { Name = "Flowers", PercentageOff = 15, AmountOff = 0.50m },
                new DiscountApplied { Name = "Tesla", PercentageOff = 5, AmountOff = 20000m }
            };
            var calculation = new Calculation(0, discounts, 0);
            receipt.Print(calculation);

            mockConsole.Verify(_ => _.WriteLine("Flowers 15% off: -0.50¢"));
            mockConsole.Verify(_ => _.WriteLine("Tesla 5% off: -20,000.00¢"));
        }
    }
}