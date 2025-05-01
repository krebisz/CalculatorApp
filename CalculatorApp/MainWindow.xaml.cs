using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculatorApp;

public partial class MainWindow : Window
{
    private readonly Calculator _calculator; //Calculator Injection
    private double? _firstNumber = null; //First Term entered
    private string? _operator = null; //Operator entered (+, -, /, *)

    public MainWindow(Calculator calculator)
    {
        InitializeComponent();
        _calculator = calculator;
        this.Loaded += (s, e) => Keyboard.Focus(DisplayTextBox); //Focus to receive KeyDown Event
    }



    #region MOUSE EVENTS

    ///<summary>
    ///BACKSPACE BUTTON: Remove last on-screen Character
    ///</summary>
    private void BackspaceButton_Click(object sender, RoutedEventArgs e)
    {
        BackSpace();
    }

    ///<summary>
    ///DIGIT BUTTON: _firstNumber = (0-9)
    ///</summary>
    private void NumberButton_Click(object sender, RoutedEventArgs e)
    {
        Number(sender as Button);
    }

    ///<summary>
    ///DECIMAL BUTTON: Allows for 1st & 2nd Term Decimal value entry
    ///</summary>
    private void DecimalButton_Click(object sender, RoutedEventArgs e)
    {
        Decimal();
    }

    ///<summary>
    ///OPERATOR BUTTON: _operator = (+, -, /, *)
    ///</summary>
    private void OperatorButton_Click(object sender, RoutedEventArgs e)
    {
        Operator(sender as Button);
    }

    ///<summary>
    ///EQUALS BUTTON: Compute the result of 1st Term (+, -, /, *) 2nd Term
    ///</summary>
    private void EqualsButton_Click(object sender, RoutedEventArgs e)
    {
        Equals();
    }

    ///<summary>
    ///CLEAR BUTTON: Resets the Calculator
    ///</summary>
    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        Clears();
    }

    #endregion



    #region KEYBOARD EVENTS

    ///<summary>
    ///Global KeyDown Event Handler: User can use Keyboard for Calculator Buttons.
    ///</summary>
    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        //Handle Digits (Top Row & Numpad)
        if (IsDigitKey(e.Key, out string digit))
        {
            DisplayTextBox.Text = (DisplayTextBox.Text == "0") ? digit : DisplayTextBox.Text + digit;
            e.Handled = true;
            return;
        }

        //Handle Operations
        if (TryHandleOperatorKey(e.Key))
        {
            e.Handled = true;
            return;
        }

        switch (e.Key)
        {
            case Key.OemComma:
                Decimal();
                break;
            case Key.Enter:
                Equals();
                break;
            case Key.Back:
                BackSpace();
                break;
            case Key.Escape:
                Clears();
                break;
            default:
                return;
        }

        e.Handled = true;
    }

    private bool IsDigitKey(Key key, out string? digit)
    {
        if (key >= Key.D0 && key <= Key.D9)
        {
            digit = (key - Key.D0).ToString();

            return true;
        }

        if (key >= Key.NumPad0 && key <= Key.NumPad9)
        {
            digit = (key - Key.NumPad0).ToString();

            return true;
        }

        digit = null;

        return false;
    }

    private bool TryHandleOperatorKey(Key key)
    {
        switch (key)
        {
            case Key.Add:
            case Key.OemPlus when Keyboard.Modifiers == ModifierKeys.None:
                Operator(new Button { Content = "+" });
                return true;

            case Key.Subtract:
            case Key.OemMinus when Keyboard.Modifiers == ModifierKeys.None:
                Operator(new Button { Content = "-" });
                return true;

            case Key.Multiply:
            case Key.D8 when Keyboard.Modifiers == ModifierKeys.Shift:
                Operator(new Button { Content = "*" });
                return true;

            case Key.Divide:
            case Key.Oem2:
                Operator(new Button { Content = "/" });
                return true;

            default:
                return false;
        }
    }

    #endregion



    #region CONTROL

    private void BackSpace()
    {
        var text = DisplayTextBox.Text;

        if (text.Length <= 1)
        {
            DisplayTextBox.Text = "0";
        }
        else
        {
            DisplayTextBox.Text = text.Substring(0, text.Length - 1);
        }
    }
    private void Number(Button button)
    {
        string digit = button.Content.ToString();

        if (DisplayTextBox.Text == "0" || DisplayTextBox.Text == "")
        {
            DisplayTextBox.Text = digit;
        }
        else
        {
            DisplayTextBox.Text += digit;
        }
    }
    private void Decimal()
    {
        if (String.IsNullOrWhiteSpace(_operator))
        {
            if (!DisplayTextBox.Text.Contains(","))
            {
                DisplayTextBox.Text += ",";
            }
        }
        else
        {
            string[] _secondNumber = DisplayTextBox.Text.Split(_operator);

            if (_secondNumber.Length == 2)
            {
                DisplayTextBox.Text += ",";
            }
        }
    }
    private void Operator(Button button)
    {
        string @operator = button.Content.ToString();

        //Parse and store the first number and the operator 
        if (double.TryParse(DisplayTextBox.Text, out double number))
        {
            _firstNumber = number;
            _operator = @operator;
            DisplayTextBox.Text = _firstNumber.ToString() + _operator;
        }
    }
    private void Equals()
    {
        //Only calculate if there is a valid 1st Number & Operator
        if (_firstNumber.HasValue && _operator != null)
        {
            string secondNumberString = DisplayTextBox.Text.Replace(_firstNumber.ToString() + _operator, string.Empty);

            if (double.TryParse(secondNumberString, out double secondNumber))
            {
                try
                {
                    double result = _calculator.Calculate(_firstNumber.Value, secondNumber, _operator);
                    DisplayTextBox.Text = result.ToString();

                    //Set First Number as Result (to reset for next operation)
                    _firstNumber = result;
                    _operator = null;
                }
                catch (Exception ex) //Handle errors (e.g. division by zero)
                {
                    DisplayTextBox.Text = "Error";
                    _firstNumber = null;
                    _operator = null;
                }
            }
        }
    }
    private void Clears()
    {
        DisplayTextBox.Text = "0";
        _firstNumber = null;
        _operator = null;
    }

    #endregion
}