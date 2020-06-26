using System;
using NUnit.Framework;
using ShoppingBasket.Discounting;
using ShoppingBasket.Output;
using ShoppingBasket.Pricing;
using ShoppingBasket.Pricing.Exceptions;
using ShoppingBasket.Readers;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void SingleItem()
        {
            var itemsPurchased = ItemsParser.List(new []{ "Beans"} );
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "£", "p");
            var pricesList = new Prices { { "Beans", 0.65m }, { "Bread", 0.80m } };
            var discounts = new Discounts();

            Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);

            var expected = Expected.Text(new []
            {
                "Subtotal: £0.65",
                "(No offers available)",
                "Total: £0.65"
            });

            Assert.AreEqual(expected, writer.GetContent());
        }

        [Test]
        public void RepeatedItem()
        {
            var itemsPurchased = ItemsParser.List(new[] { "Bread", "Beans", "Bread" });
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "£", "p");
            var pricesList = new Prices { { "Beans", 0.65m }, { "Bread", 0.80m } };
            var discounts = new Discounts();

            Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);

            var expected = Expected.Text(new[]
            {
                "Subtotal: £2.25",
                "(No offers available)",
                "Total: £2.25"
            });

            Assert.AreEqual(expected, writer.GetContent());
        }

        [Test]
        //[ExpectedException(typeof(ArgumentException))] - ExpectedException is missing!?
        public void InvalidItem()
        {
            var itemsPurchased = ItemsParser.List(new[] { "Kitchen Sink" });
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "£", "p");
            var pricesList = new Prices { { "Beans", 0.65m }, { "Bread", 0.80m } };
            var discounts = new Discounts();

            Exception expected = null;
            try
            {
                Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);
            }
            catch (Exception ex)
            {
                expected = ex;
            }
            
            Assert.True(expected is ItemNotFoundException);
            Assert.AreEqual("'Kitchen Sink' not in stock", expected.Message);
        }

        [Test]
        public void ApplyPercentageDiscountForOneItemOnce()
        {
            var itemsPurchased = ItemsParser.List(new[] { "Apples" });
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "£", "p");
            var pricesList = new Prices { { "Beans", 0.65m }, { "Bread", 0.80m }, { "Apples", 1.00m } };
            var discounts = new Discounts
            {
                new PercentageDiscount("Apples", 10)
            };

            Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);

            var expected = Expected.Text(new[]
            {
                "Subtotal: £1.00",
                "Apples 10% off: -0.10p",
                "Total: £0.90"
            });

            Assert.AreEqual(expected, writer.GetContent());
        }

        [Test]
        public void ApplyPercentageDiscountForOneItemRepeatedly()
        {
            var itemsPurchased = ItemsParser.List(new[] { "Apples", "Apples" });
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "£", "p");
            var pricesList = new Prices { { "Beans", 0.65m }, { "Bread", 0.80m }, { "Apples", 1.00m } };
            var discounts = new Discounts
            {
                new PercentageDiscount("Apples", 10)
            };

            Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);

            var expected = Expected.Text(new[]
            {
                "Subtotal: £2.00",
                "Apples 10% off: -0.20p",
                "Total: £1.80"
            });

            Assert.AreEqual(expected, writer.GetContent());
        }

        [Test]
        public void ApplyPercentageDiscountForMultipleItems()
        {
            var itemsPurchased = ItemsParser.List(new[] { "Apples", "Bread", "Apples", "Bread" });
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "£", "p");
            var pricesList = new Prices { { "Beans", 0.65m }, { "Bread", 0.80m }, { "Apples", 1.00m } };
            var discounts = new Discounts
            {
                new PercentageDiscount("Apples", 10),
                new PercentageDiscount("Bread", 20)
            };

            Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);

            var expected = Expected.Text(new[]
            {
                "Subtotal: £3.60",
                "Apples 10% off: -0.20p",
                "Bread 20% off: -0.32p",
                "Total: £3.08"
            });

            Assert.AreEqual(expected, writer.GetContent());
        }

        [Test]
        public void ApplyPercentageDiscountForMultipleItemsIncludingNonDiscounted()
        {
            var itemsPurchased = ItemsParser.List(new[] { "Apples", "Apples", "Beans", "Bread", "Bread" });
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "£", "p");
            var pricesList = new Prices { { "Beans", 0.65m }, { "Bread", 0.80m }, { "Apples", 1.00m } };
            var discounts = new Discounts
            {
                new PercentageDiscount("Apples", 10),
                new PercentageDiscount("Bread", 20)
            };

            Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);

            var expected = Expected.Text(new[]
            {
                "Subtotal: £4.25",
                "Apples 10% off: -0.20p",
                "Bread 20% off: -0.32p",
                "Total: £3.73"
            });

            Assert.AreEqual(expected, writer.GetContent());
        }

        [Test]
        public void ReadMeExample()
        {
            var itemsPurchased = ItemsParser.List(new[] { "Apples", "Milk", "Bread",});
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "£", "p");
            var pricesList = new Prices { { "Beans", 0.65m }, { "Bread", 0.80m }, { "Apples", 1.00m }, { "Milk", 1.30m } };
            var discounts = new Discounts
            {
                new PercentageDiscount("Apples", 10)
            };

            Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);

            var expected = Expected.Text(new[]
            {
                "Subtotal: £3.10",
                "Apples 10% off: -0.10p",
                "Total: £3.00"
            });

            Assert.AreEqual(expected, writer.GetContent());
        }

        [Test]
        public void MultipleSourcesOfDiscount()
        {
            var itemsPurchased = ItemsParser.List(new[] { "Milk", "Bread" });
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "$", "c");
            var pricesList = new Prices { { "Bread", 0.80m }, { "Apples", 1.00m }, { "Milk", 1.30m } };
            var discounts = new Discounts
            {
                new PercentageDiscount("Bread", 50),
                new MultiplePurchaseDiscount(1, "Milk", new PercentageDiscount("Bread", 50))
            };

            Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);

            var expected = Expected.Text(new[]
            {
                "Subtotal: $2.10",
                "Bread 50% off: -0.40c",
                "Bread 50% off: -0.40c",
                "Total: $1.30"
            });

            Assert.AreEqual(expected, writer.GetContent());
        }

        [Test]
        public void MultipleSourcesOfMultipleDiscounts()
        {
            var itemsPurchased = ItemsParser.List(new[] { "Milk", "Milk", "Milk", "Milk" });
            var writer = new TestWriter();
            var receipt = new Receipt(writer, "$", "c");
            var pricesList = new Prices { { "Bread", 0.80m }, { "Apples", 1.00m }, { "Milk", 1.30m } };
            var discounts = new Discounts
            {
                new PercentageDiscount("Milk", 50),
                new MultiplePurchaseDiscount(2, "Milk", new PercentageDiscount("Milk", 75))
            };

            Program.GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);

            var expected = Expected.Text(new[]
            {
                "Subtotal: $5.20",
                "Milk 50% off: -2.60c",
                "Milk 75% off: -3.90c",
                "Total: $0.00"
            });

            // Without the check in Pricing\Calculation.cs the purchaser would be owed money for purchasing milk with these discounts!
            Assert.AreEqual(expected, writer.GetContent());
        }

    }
}