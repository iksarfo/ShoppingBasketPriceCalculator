using System.Collections;
using System.Collections.Generic;

namespace ShoppingBasket.Pricing
{
    internal class Prices : IPrices
    {
        private readonly IList<PricedItem> _pricedItems = new List<PricedItem>();

        public void Add(string name, decimal price)
        {
            _pricedItems.Add(new PricedItem(name, price));
        }

        public IEnumerator<PricedItem> GetEnumerator()
        {
            return _pricedItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}