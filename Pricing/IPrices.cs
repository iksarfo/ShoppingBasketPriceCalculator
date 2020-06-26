using System.Collections.Generic;

namespace ShoppingBasket.Pricing
{
    internal interface IPrices : IEnumerable<PricedItem>
    {
        void Add(string name, decimal price);
    }
}