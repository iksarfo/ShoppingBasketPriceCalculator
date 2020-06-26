using System;

namespace ShoppingBasket.Item.Exceptions
{
    public class BadItemNameException : ArgumentException
    {
        public BadItemNameException(string message) : base(message)
        {
        }
    }
}