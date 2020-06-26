using System;
using System.Collections.Generic;
using ShoppingBasket.Discounting;

namespace ShoppingBasket.Pricing
{
    internal class Calculation
    {
        public decimal SubTotal { get; }
        public List<DiscountApplied> Discounts { get; }
        public decimal Total { get; }

        public Calculation(decimal subTotal, List<DiscountApplied> discounts, decimal total)
        {
            SubTotal = subTotal;
            Discounts = discounts;
            Total = Math.Max(total, 0.0m);
        }
    }
}