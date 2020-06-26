using System.Text;

namespace ShoppingBasket.Tests
{
    internal static class Expected
    {
        public static string Text(string[] lines)
        {
            var stringBuilder = new StringBuilder();

            foreach (var line in lines)
            {
                stringBuilder.AppendLine(line);
            }

            return stringBuilder.ToString();
        }
    }
}