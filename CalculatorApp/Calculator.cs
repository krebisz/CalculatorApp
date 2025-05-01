namespace CalculatorApp
{
    ///<summary>
    ///Calculator class that selects the right operation strategy.
    ///</summary>
    public class Calculator
    {
        //Maps operator symbols to operation implementations.
        private readonly Dictionary<string, IOperation> _operations;

        public Calculator()
        {
            _operations = new Dictionary<string, IOperation>()
            {
                { "+", new AdditionOperation() },
                { "-", new SubtractionOperation() },
                { "*", new MultiplicationOperation() },
                { "/", new DivisionOperation() }
            };
        }

        ///<summary>
        ///Performs Calculation based on the Operation corresponding to the operation string. Errors & Undefined Operations throw an Error.
        ///</summary>
        public double Calculate(double a, double b, string @operator)
        {
            if (!_operations.ContainsKey(@operator))
            {
                throw new InvalidOperationException($"Unknown Operator '{@operator}'.");
            }

            return _operations[@operator].Execute(a, b);
        }
    }
}