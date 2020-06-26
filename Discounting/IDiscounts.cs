using System.Collections.Generic;

namespace ShoppingBasket.Discounting
{
    internal interface IDiscounts : IEnumerable<IDiscount>
    {
        void Add(IDiscount discount);
    }
}