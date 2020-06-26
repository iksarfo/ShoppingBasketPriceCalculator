using System.Linq;
using NUnit.Framework;
using ShoppingBasket.Item;
using ShoppingBasket.Readers;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class ItemsParserTests
    {
        private IParseItems _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new ItemsParser();
        }

        [Test]
        public void ReturnsNoItems()
        {
            Assert.IsEmpty(_parser.Parse(""));
        }

        [Test]
        public void IgnoresWhitespace()
        {
            Assert.IsEmpty(_parser.Parse("   "));
        }

        [Test]
        public void ReturnsOneItem()
        {
            Assert.AreEqual(1, _parser.Parse("item").Count());
            Assert.AreEqual((ItemName)"namedItem", _parser.Parse("namedItem").Single());
        }

        [Test]
        public void ReturnsMultipleItems()
        {
            var itemNames = _parser.Parse("first second").ToList();
            Assert.AreEqual(2, itemNames.Count());
            Assert.Contains((ItemName)"first", itemNames);
            Assert.Contains((ItemName)"second", itemNames);
        }
    }
}