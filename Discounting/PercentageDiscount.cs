using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.Item;

namespace ShoppingBasket.Discounting
{
    internal class PercentageDiscount : IDiscount
    {
        private readonly int _percentage;

        public ItemName Item { get; }

        public decimal Value { get; }

        public PercentageDiscount(ItemName item, int percentageDiscount)
        {
            _percentage = percentageDiscount;

            Item = item;

            var outOfRangeErrorMessage = item + " may not have " + _percentage + "% discount applied. A valid discount is between 0 and 100 (including 0 and 100).";
            if(_percentage < 0) throw new ArgumentOutOfRangeException(nameof(percentageDiscount), outOfRangeErrorMessage);
            if(_percentage > 100) throw new ArgumentOutOfRangeException(nameof(percentageDiscount), outOfRangeErrorMessage);

            Value = (100 - _percentage) * 0.01m;
        }

        public IEnumerable<DiscountFound> FindDiscountable(IList<ItemName> items)
        {
            var matchingItems = items.Where(name => Equals(name, Item)).ToList();

            if (!matchingItems.Any())
            {
                return Enumerable.Empty<DiscountFound>();
            }

            return new List<DiscountFound>
            {
                new DiscountFound
                {
                    Percentage = Value,
                    Name = Item,
                    PercentageOff = _percentage,
                    Quantity = matchingItems.Count
                }
            };
        }
    }
}