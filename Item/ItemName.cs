using System;
using ShoppingBasket.Item.Exceptions;

namespace ShoppingBasket.Item
{
    public class ItemName
    {
        public string Value { get; }

        public ItemName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BadItemNameException("An item's name is required");
            }

            Value = value;
        }

        public static implicit operator string(ItemName itemName) => itemName.Value;
        public static implicit operator ItemName(string itemName) => new ItemName(itemName);

        public override bool Equals(object obj)
        {
            return obj is ItemName name && Equals(name);
        }

        protected bool Equals(ItemName other)
        {
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}