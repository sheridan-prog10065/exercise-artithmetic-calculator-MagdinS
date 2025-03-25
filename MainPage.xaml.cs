using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MathOperators;

public partial class MainPage : ContentPage
{
    /// <summary>
    /// Remembers the expressions that were calculated by the user
    /// </summary>
    private ObservableCollection<string> _expList;
    
    public MainPage()
    {
        InitializeComponent();
        
        _expList = new ObservableCollection<string>();
        _lstExpHistory.ItemsSource = _expList;     
    }

    private async void OnCalculate(object sender, EventArgs e)
    {
        try
        {
            //Get the input to the arithmetic operation
            double leftOperand = ReadLeftOperand();
            double rightOperand = ReadRightOperand();
            char operation = ReadArithmeticOperation();

            //Perform the arithmetic operation and obtain the result
            double result = PerformArithmeticOperation(operation, leftOperand, rightOperand);

            //Display the arithmetic calculation to the user. Show the work!
            string expression = $"{leftOperand} {operation} {rightOperand} = {result}";

            //remember the expression in the page's field variable
            _expList.Add(expression);

            _txtMathExp.Text = expression;
        }
        catch (CalculatorException ex)
        {
            await DisplayAlert("Arithmetic Calculator", ex.Message, "OK");
        }

    }

    private char ReadArithmeticOperation()
    {
        //Step 1: Obtain the input
        string opInput  = _pckOperand.SelectedItem as  string;
        
        //Step 2: Validate the input
        if (String.IsNullOrWhiteSpace(opInput))
        { 
            throw new CalculatorException("Please select one of the arithmetic operations.");
        }
        
        //Step 3: Use the input
        //Obtain the character that represents the arithmetic operation
        //Cast to string is possible because SelectedItem is an Object
        //Extra paranthesis are needed to ensure the index operator is applied to the result of the cast
        char operation = opInput[0];
        return operation;
    }

    private double ReadRightOperand()
    {
        //Step1: Obtain Input
        string rightOperandInput = _txtRightOp.Text;
        
        //Step 2: Validate the input
        if (String.IsNullOrWhiteSpace(rightOperandInput))
        {
            throw new CalculatorException("Please provide the right operand for the arithmetic operation.");
        }

        double rightOperand;
        if (Double.TryParse(rightOperandInput,  out rightOperand) == false)
        {
            //Parsing has failed. The input is not a number
            throw new CalculatorException("Please enter a number as the right operand for the arithmetic operation.");
        }
        
        //Step 3: Use the input
        return rightOperand;
    }

    private double ReadLeftOperand()
    {
        //Step1: Obtain Input
        string leftOperandInput = _txtRightOp.Text;
        
        //Step 2: Validate the input
        if (String.IsNullOrWhiteSpace(leftOperandInput))
        {
            throw new CalculatorException("Please provide the left operand for the arithmetic operation.");
        }

        double leftOperand;
        if (Double.TryParse(leftOperandInput,  out leftOperand) == false)
        {
            //Parsing has failed. The input is not a number
            throw new CalculatorException("Please enter a number as the left operand for the arithmetic operation.");
        }
        
        //Step 3: Use the input
        return leftOperand;
    }

    private double PerformArithmeticOperation(char operation, double leftOperand, double rightOperand)
    {
        switch (operation)
        {
            case '+':
                return PerformAddition(leftOperand, rightOperand);

            case '-':
                return PerformSubraction(leftOperand, rightOperand);

            case '*':
                return PerformMultiplication(leftOperand, rightOperand);

            case '/':
                return PerformDivision(leftOperand, rightOperand);

            case '%':
                return PerformDivRemainder(leftOperand, rightOperand);

            default:
                Debug.Assert(false, "Unknown arithmetic opearand. Cannot perform the artighmetic operation.");
                return 0;
        }
    }

    private double PerformAddition(double leftOperand, double rightOperand)
    {
        return (leftOperand + rightOperand);
    }
    
    private double PerformSubraction(double leftOperand, double rightOperand)
    {
        return leftOperand - rightOperand;
    }
    
    private double PerformMultiplication(double leftOperand, double rightOperand)
    {
        return leftOperand * rightOperand;
    }
    
    private double PerformDivision(double leftOperand, double rightOperand)
    {
        string divOp = _pckOperand.SelectedItem as string; //another way of casting used for objects
        if (divOp.Contains("Int", StringComparison.OrdinalIgnoreCase))
        {
            //Integer division is performed when the operands are both integers
            int intLeftOp = (int)leftOperand;
            int intRightOp = (int)rightOperand;
            double result =  intLeftOp / intRightOp;
            return result;
        }
        else
        {
            //Real division
            return leftOperand / rightOperand;    
        }
    }
    
    private double PerformDivRemainder(double leftOperand, double rightOperand)
    {                                                                          
        return leftOperand % rightOperand;                                     
    }                                                                          
}