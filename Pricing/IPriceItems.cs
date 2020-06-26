using System.Collections.Generic;
using ShoppingBasket.Item;

namespace ShoppingBasket.Pricing
{
    internal interface IPriceItems
    {
        Calculation Calculate(IList<ItemName> items);
    }
}