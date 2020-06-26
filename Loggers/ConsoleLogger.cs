using System;

namespace ShoppingBasket.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void Error(string text)
        {
            Console.WriteLine(text);
        }
    }
}