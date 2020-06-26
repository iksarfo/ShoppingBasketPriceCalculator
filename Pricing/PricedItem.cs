using ShoppingBasket.Item;

namespace ShoppingBasket.Pricing
{
    public class PricedItem
    {
        public ItemName Name { get; }
        public decimal Price { get; }

        public PricedItem(ItemName name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}