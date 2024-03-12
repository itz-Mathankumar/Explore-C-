using System;
using System.Reflection;
public class myDelegate
{
    public delegate void StringDelegate(string name);
    public delegate double SquareDelegate(int number);
    public delegate void ExceptionDelegate(string name);
    
    public static void Main(string[] args)
    {
        // Initialize delegate StringDelegate StringDelegate = DisplayName or StringDelegate stringDelegateInstance = new StringDelegate(DisplayName).
        StringDelegate stringDelegateInstance = DisplayName;
        stringDelegateInstance += Display;
        // Invoking Delegate => myDelegateInstance.Invoke("Hello World") or myDelegateInstance("Hello World").
        stringDelegateInstance("Hello World");
        stringDelegateInstance -= DisplayName;
        // Argument 1: cannot convert from 'void' to 'bool' => System.Console.WriteLine(stringDelegateInstance("Hello Earth")).
        stringDelegateInstance("Hello Earth");
        stringDelegateInstance -= Display;
        // System.NullReferenceException: Object reference not set to an instance of an object => myDelegateInstance("Hello Earth") && myDelegateInstance.Invoke("Hello Earth").
        stringDelegateInstance?.Invoke("Hello Earth");

        SquareDelegate squareDelegateInstance = ManualSquare;
        squareDelegateInstance += BuiltInSquare;
        // Returns last method result => 25.
        System.Console.WriteLine(squareDelegateInstance(5));
        //Invoke individually to Obtain each return value.
        foreach (SquareDelegate squareMethod in squareDelegateInstance.GetInvocationList())
        {
            Console.WriteLine("Output: " + squareMethod(6));
        }
        // var results = squareDelegateInstance.GetInvocationList().Cast<SquareDelegate>().Select(x => x()).ToList().

        //ExceptionDelegate exceptionDelegateInstance = () => { throw new Exception("Exception Delegate"); };
        ExceptionDelegate exceptionDelegateInstance = new ExceptionDelegate(DisplayName);
        exceptionDelegateInstance += ExceptionMethod;
        exceptionDelegateInstance += Display;
        // Display method will not be called and program crashes => exceptionDelegateInstance("Exception Delegate").

        foreach (ExceptionDelegate method in exceptionDelegateInstance.GetInvocationList())
        {
            try
            {
                method("throw new Exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
            }
        }
    }

    public static void DisplayName(string displayName)
    {
        System.Console.WriteLine($"{nameof(DisplayName)}: " + displayName);
    }
    public static void Display(string name)
    {
        System.Console.WriteLine($"{nameof(Display)}: " + name);
    }
    public static double ManualSquare(int number)
    {
        System.Console.WriteLine($"{nameof(ManualSquare)}: " + number);
        return number * number;
    }
    public static double BuiltInSquare(int number)
    {
        System.Console.WriteLine($"{nameof(BuiltInSquare)}: " + number);
        return Math.Pow(number, 2);
    }
    public static void ExceptionMethod(string name)
    {
        System.Console.WriteLine($"{nameof(ExceptionMethod)}: " + name);
        throw new Exception($"Sample Exception {name}.");
    }
}
