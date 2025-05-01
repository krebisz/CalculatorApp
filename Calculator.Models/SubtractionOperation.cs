namespace CalculatorApp
{
    public class SubtractionOperation : IOperation
    {
        public double Execute(double a, double b)
        {
            return a - b;
        }
    }
}