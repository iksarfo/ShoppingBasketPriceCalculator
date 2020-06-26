using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingBasket.Item;

namespace ShoppingBasket.Readers
{
    public class ItemsParser : IParseItems
    {
        private readonly IList<string> _inputItemSeparators;

        public ItemsParser()
        {
            _inputItemSeparators = new List<string> { " " };
        }

        public static IList<ItemName> List(string[] args)
        {
            return args.Select(item => (ItemName) item).ToList();
        }

        public IEnumerable<ItemName> Parse(string input)
        {
            var separator = _inputItemSeparators.ToArray();
            return List(input.Split(separator, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}