namespace CalculatorApp
{
    ///<summary>
    ///Arithmetic Operation Interface (between two terms).
    ///</summary>
    public interface IOperation
    {
        /// <summary>
        /// Executes the operation on two operands.
        /// </summary>
        double Execute(double a, double b);
    }
}