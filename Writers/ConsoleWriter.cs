using System;

namespace ShoppingBasket.Writers
{
    internal class ConsoleWriter : IConsole
    {
        public void WriteLine(string text) => Console.WriteLine(text);
    }
}