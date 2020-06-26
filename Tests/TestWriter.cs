using System.Text;
using ShoppingBasket.Writers;

namespace ShoppingBasket.Tests
{
    internal class TestWriter : IReadable, IConsole
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public void WriteLine(string text)
        {
            _stringBuilder.AppendLine(text);
        }

        public string GetContent()
        {
            return _stringBuilder.ToString();
        }
    }
}