namespace CalculatorApp
{
    public class MultiplicationOperation : IOperation
    {
        public double Execute(double a, double b)
        {
            return a * b;
        }
    }
}