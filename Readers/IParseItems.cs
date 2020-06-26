using System.Collections.Generic;
using ShoppingBasket.Item;

namespace ShoppingBasket.Readers
{
    public interface IParseItems
    {
        IEnumerable<ItemName> Parse(string input);
    }
}