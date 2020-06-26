using System.Collections;
using System.Collections.Generic;
using ShoppingBasket.Item;
using ShoppingBasket.Pricing.Exceptions;

namespace ShoppingBasket.Pricing
{
    public class PricedItems : IEnumerable<KeyValuePair<ItemName, PricedItem>>
    {
        private readonly Dictionary<ItemName, PricedItem> _pricedItems;

        public PricedItems()
        {
            _pricedItems = new Dictionary<ItemName, PricedItem>();
        }

        public IEnumerator<KeyValuePair<ItemName, PricedItem>> GetEnumerator()
        {
            return _pricedItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(PricedItem item)
        {
            _pricedItems.Add(item.Name, item);
        }

        public PricedItem this[ItemName name]
        {
            get
            {
                if (!_pricedItems.TryGetValue(name, out var pricedItem))
                {
                    throw new ItemNotFoundException("'" + name + "' not in stock");
                }

                return pricedItem;
            }
        }
    }
}