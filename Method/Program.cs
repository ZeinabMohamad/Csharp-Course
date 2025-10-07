using System; // Include the System namespace to use Console, etc.

namespace ConsoleAppExample
{
    // Define a class named MathOperations
    class MathOperations
    {
        // Create a void method that takes two integers
        public void DoMath(int number1, int number2)
        {
            // Perform a math operation on the first number (e.g., multiply by 5)
            int result = number1 * 5;

            // Display the result of the math operation
            Console.WriteLine("Result of operation on first number: " + result);

            // Display the second number
            Console.WriteLine("Second number is: " + number2);
        }
    }

    class Program
    {
        // Main method - entry point of the console application
        static void Main(string[] args)
        {
            // Instantiate the MathOperations class
            MathOperations mathOps = new MathOperations();

            // Call the method with two integers using positional arguments
            mathOps.DoMath(5, 10);

            // Call the method again using named parameters
            mathOps.DoMath(number1: 20, number2: 40);

            // Wait for the user to press a key before closing the console window
            Console.ReadLine();
        }
    }
}
