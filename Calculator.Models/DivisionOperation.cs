namespace CalculatorApp
{
    public class DivisionOperation : IOperation
    {
        public double Execute(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Cannot Divide by Zero");
            }

            return a / b;
        }
    }
}