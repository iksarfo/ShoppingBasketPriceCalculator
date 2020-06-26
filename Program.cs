using System;
using System.Collections.Generic;
using ShoppingBasket.Discounting;
using ShoppingBasket.Item;
using ShoppingBasket.Loggers;
using ShoppingBasket.Output;
using ShoppingBasket.Pricing;
using ShoppingBasket.Readers;
using ShoppingBasket.Writers;

namespace ShoppingBasket
{
    class Program
    {
        public static IConsole Writer = new ConsoleWriter();
        public static ILogger Logger = new ConsoleLogger(); // typically being written elsewhere
        public static void Main(string[] args)
        {
            var itemsPurchased = ItemsParser.List(args);
            var receipt = new Receipt(Writer, "£", "p");
            var pricesList = new Prices {{"Beans", 0.65m}, {"Bread", 0.80m}, {"Milk", 1.30m}, {"Apples", 1.00m}};
            var discounts = new Discounts
            {
                new PercentageDiscount("Apples", 10),
                new MultiplePurchaseDiscount(2, "Beans", new PercentageDiscount("Bread", 50))
            };

            try
            {
                GenerateReceipt(pricesList, discounts, itemsPurchased, receipt);
            }
            catch (NotSupportedException notFoundException)
            {
                Console.WriteLine(notFoundException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured.");
                Logger.Error(ex.Message); // only showing exception message in place of real logging
            }
            
        }

        public static void GenerateReceipt(
            IPrices pricesList,
            IDiscounts discounts,
            IList<ItemName> itemsPurchased,
            IOutputReceipt receipt)
        {
            var itemsPricing = new ItemsPricing(pricesList, discounts);
            var calculation = itemsPricing.Calculate(itemsPurchased);
            receipt.Print(calculation);
        }
    }
}
