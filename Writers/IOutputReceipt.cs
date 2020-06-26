using ShoppingBasket.Pricing;

namespace ShoppingBasket.Writers
{
    internal interface IOutputReceipt
    {
        void Print(Calculation calculation);
    }
}