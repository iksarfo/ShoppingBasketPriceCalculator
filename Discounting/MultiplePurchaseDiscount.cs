using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.Item;

namespace ShoppingBasket.Discounting
{
    internal class MultiplePurchaseDiscount : IDiscount
    {
        private readonly int _qualifyingItemCount;
        private readonly ItemName _qualifyingItem;
        private readonly PercentageDiscount _discount;

        public MultiplePurchaseDiscount(int qualifyingItemCount, ItemName qualifyingItem, PercentageDiscount discount)
        {
            if(qualifyingItemCount < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(qualifyingItemCount),
                    "At least one qualifying item is required");
            }
            _qualifyingItemCount = qualifyingItemCount;
            _qualifyingItem = qualifyingItem;
            _discount = discount;
        }

        public IEnumerable<DiscountFound> FindDiscountable(IList<ItemName> items)
        {
            var discountsFound = new List<DiscountFound>();
            var availableItems = new List<ItemName>(items);

            while (availableItems.Count(availableItem => availableItem.Equals(_qualifyingItem)) >= _qualifyingItemCount)
            {
                var discountable = _discount.FindDiscountable(availableItems).ToList();

                if (discountable.Any())
                {
                    var discounting = discountable.First();
                    discountsFound.Add(discounting);

                    availableItems.Remove(discounting.Name);
                }

                var removed = 0;
                while (removed < _qualifyingItemCount)
                {
                    availableItems.Remove(_qualifyingItem);
                    removed += 1;
                }
            }

            return discountsFound;
        }
    }
}