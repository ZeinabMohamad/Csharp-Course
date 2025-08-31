using System;

class Program
{
    static void Main()
    {
        // Welcome the user warmly
        Console.WriteLine("Welcome to Package Express. Please follow the instructions below.");

        // Ask for weight and confirm
        Console.Write("Please input the package weight: ");
        decimal pkgWeight = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine($"You entered weight: {pkgWeight} lbs");

        // Validate weight
        if (pkgWeight > 50)
        {
            Console.WriteLine("Package too heavy to be shipped via Package Express. Have a good day.");
            return;
        }

        // Get and confirm dimensions
        Console.Write("Enter width: ");
        decimal width = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine($"Width recorded: {width} inches");

        Console.Write("Enter height: ");
        decimal height = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine($"Height recorded: {height} inches");

        Console.Write("Enter length: ");
        decimal length = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine($"Length recorded: {length} inches");

        // Dimension check
        decimal sizeTotal = width + height + length;
        if (sizeTotal > 50)
        {
            Console.WriteLine("Package too big to be shipped via Package Express.");
            return;
        }

        // Compute and show the quote
        decimal shippingPrice = (width * height * length * pkgWeight) / 100;
        Console.WriteLine($"Your estimated total for shipping this package is: ${shippingPrice:0.00}");
        Console.WriteLine("Thank you for using Package Express!");
    }
}
