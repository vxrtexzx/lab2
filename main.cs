using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the coefficients of the polynomial (space-separated): ");
        string? input = Console.ReadLine();
        if (input == null)
        {
            Console.WriteLine("No input provided.");
            return;
        }

        string[] parts = input.Split(' ');
        double[] coefficients;

        try
        {
            coefficients = Array.ConvertAll(parts, double.Parse);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter numbers only.");
            return;
        }

        IPolynomial polynomial;

        switch (coefficients.Length)
        {
            case 2:
                polynomial = new LinearPolynomial(coefficients);
                break;
            case 3:
                polynomial = new QuadraticPolynomial(coefficients);
                break;
            default:
                Console.WriteLine("Unsupported polynomial degree.");
                return;
        }

        try
        {
            Complex[] roots = polynomial.FindRoots();
            Console.WriteLine("The roots are:");
            foreach (var root in roots)
            {
                Console.WriteLine(root);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
