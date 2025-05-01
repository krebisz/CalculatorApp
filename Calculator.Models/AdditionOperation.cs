namespace CalculatorApp
{
    public class AdditionOperation : IOperation
    {
        public double Execute(double a, double b)
        {
            return a + b;
        }
    }
}