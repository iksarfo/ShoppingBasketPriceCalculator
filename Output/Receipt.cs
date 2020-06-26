using System.Linq;
using ShoppingBasket.Pricing;
using ShoppingBasket.Writers;

namespace ShoppingBasket.Output
{
    internal class Receipt : IOutputReceipt
    {
        private readonly IConsole _writer;
        private readonly string _currencyMajorSymbol;
        private readonly string _currencyMinorSymbol;
        private readonly string _format;

        private const string DefaultFormat = "#,##0.00";

        public Receipt(IConsole writer, string currencyMajorSymbol, string currencyMinorSymbol, string format = DefaultFormat)
        {
            _writer = writer;
            _currencyMajorSymbol = currencyMajorSymbol;
            _currencyMinorSymbol = currencyMinorSymbol;
            _format = format;
        }

        public void Print(Calculation calculation)
        {
            _writer.WriteLine($"Subtotal: {_currencyMajorSymbol}{calculation.SubTotal.ToString(_format)}");

            foreach (var discount in calculation.Discounts)
            {
                _writer.WriteLine($"{discount.Name} {discount.PercentageOff}% off: -{discount.AmountOff.ToString(_format)}{_currencyMinorSymbol}");
            }

            if (!calculation.Discounts.Any())
            {
                _writer.WriteLine("(No offers available)");
            }

            _writer.WriteLine(text: $"Total: {_currencyMajorSymbol}{calculation.Total.ToString(_format)}");
        }
    }
}