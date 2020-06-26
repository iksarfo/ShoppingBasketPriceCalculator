using System.Collections.Generic;

namespace ShoppingBasket.Pricing.Exceptions
{
    public class ItemNotFoundException : KeyNotFoundException
    {
        public ItemNotFoundException(string message) : base(message)
        {
        }
    }
}