using System.Collections;
using System.Collections.Generic;

namespace ShoppingBasket.Discounting
{
    internal class Discounts : IDiscounts
    {
        private readonly HashSet<IDiscount> _discounts = new HashSet<IDiscount>();

        public void Add(IDiscount discount)
        {
            _discounts.Add(discount);
        }

        public IEnumerator<IDiscount> GetEnumerator()
        {
            return _discounts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}