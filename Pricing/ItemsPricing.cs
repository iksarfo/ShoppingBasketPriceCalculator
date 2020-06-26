using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.Discounting;
using ShoppingBasket.Item;

namespace ShoppingBasket.Pricing
{
    internal class ItemsPricing : IPriceItems
    {
        private readonly IDiscounts _discounts;
        readonly PricedItems _priceList = new PricedItems();

        public ItemsPricing(IPrices pricesList, IDiscounts discounts)
        {
            foreach (var item in pricesList)
            {
                _priceList.Add(item);
            }
            _discounts = discounts;
        }

        public Calculation Calculate(IList<ItemName> itemsPurchased)
        {
            var subTotal = itemsPurchased.Sum(item => _priceList[item].Price);
            var discountsFound = FindDiscounts(itemsPurchased);
            var discountsApplied = ApplyDiscounts(discountsFound);
            var total = subTotal - discountsApplied.Sum(discount => discount.AmountOff);
            return new Calculation(subTotal, discountsApplied, total);
        }

        private List<DiscountApplied> ApplyDiscounts(IEnumerable<DiscountFound> discountsFound)
        {
            static decimal DiscountForItems(DiscountFound found)
            {
                return found.Percentage;
            }

            decimal OriginalSumForItems(DiscountFound found)
            {
                return found.Quantity * _priceList[found.Name].Price;
            }

            return discountsFound
                .Select(found =>
                    new DiscountApplied
                    {
                        Name = found.Name,
                        PercentageOff = found.PercentageOff,
                        AmountOff = OriginalSumForItems(found) - OriginalSumForItems(found) * DiscountForItems(found)
                    })
                .ToList();
        }

        private IEnumerable<DiscountFound> FindDiscounts(IList<ItemName> items)
        {
            var foundDiscounts = new List<DiscountFound>();

            foreach (var discount in _discounts)
            {
                foundDiscounts.AddRange(discount.FindDiscountable(items));
            }

            return foundDiscounts;
        }
    }
}