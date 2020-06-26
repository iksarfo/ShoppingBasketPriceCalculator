using ShoppingBasket.Item;

namespace ShoppingBasket.Discounting
{
    internal class DiscountFound
    {
        public decimal Percentage { get; set; }
        public int Quantity { get; set; }
        public int PercentageOff { get; set; } 
        public ItemName Name { get; set; }
    }

    internal class DiscountApplied
    {
        public decimal AmountOff { get; set; }
        public int PercentageOff { get; set; }
        public ItemName Name { get; set; }

    }
}