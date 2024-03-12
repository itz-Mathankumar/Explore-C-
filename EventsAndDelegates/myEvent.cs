using System;

public class myEvent
{
    public event StringDelegate StringEvent;
    public event SquareDelegate SquareEvent;
    public event ExceptionDelegate ExceptionEvent;
    public delegate void StringDelegate(string name);
    public delegate double SquareDelegate(int number);
    public delegate void ExceptionDelegate(string name);

    public static void Main(string[] args)
    {
        myEvent myDelegateInstance = new myEvent();
        myDelegateInstance.StringEvent += DisplayName;
        myDelegateInstance.StringEvent += Display;
        myDelegateInstance.StringEvent?.Invoke("Hello World");
        myDelegateInstance.StringEvent -= DisplayName;
        myDelegateInstance.StringEvent?.Invoke("Hello Earth");
        myDelegateInstance.StringEvent -= Display;
        myDelegateInstance.StringEvent?.Invoke("Hello Earth");

        myDelegateInstance.SquareEvent += ManualSquare;
        myDelegateInstance.SquareEvent += BuiltInSquare;
        Console.WriteLine(myDelegateInstance.SquareEvent?.Invoke(5));
        foreach (SquareDelegate squareMethod in myDelegateInstance.SquareEvent.GetInvocationList())
        {
            Console.WriteLine("Output: " + squareMethod(6));
        }

        myDelegateInstance.ExceptionEvent += DisplayName;
        myDelegateInstance.ExceptionEvent += ExceptionMethod;
        myDelegateInstance.ExceptionEvent += Display;
        foreach (ExceptionDelegate method in myDelegateInstance.ExceptionEvent.GetInvocationList())
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
        Console.WriteLine($"{nameof(DisplayName)}: " + displayName);
    }
    public static void Display(string name)
    {
        Console.WriteLine($"{nameof(Display)}: " + name);
    }
    public static double ManualSquare(int number)
    {
        Console.WriteLine($"{nameof(ManualSquare)}: " + number);
        return number * number;
    }
    public static double BuiltInSquare(int number)
    {
        Console.WriteLine($"{nameof(BuiltInSquare)}: " + number);
        return Math.Pow(number, 2);
    }
    public static void ExceptionMethod(string name)
    {
        Console.WriteLine($"{nameof(ExceptionMethod)}: " + name);
        throw new Exception($"Sample Exception {name}.");
    }
}
