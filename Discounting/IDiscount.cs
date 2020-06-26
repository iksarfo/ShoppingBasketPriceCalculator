using System.Collections.Generic;
using ShoppingBasket.Item;

namespace ShoppingBasket.Discounting
{
    internal interface IDiscount
    {
        IEnumerable<DiscountFound> FindDiscountable(IList<ItemName> items);
    }
}