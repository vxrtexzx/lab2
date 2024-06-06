using System;

public interface IPolynomial
{
    int Dimension { get; }
    double[] Coefficients { get; }
    Complex[] FindRoots();
}

public abstract class PolynomialBase : IPolynomial
{
    private double[] coefficients;
    protected Func<double[], Complex[]> solveStrategy;

    public int Dimension => coefficients.Length - 1;
    public double[] Coefficients => (double[])coefficients.Clone();

    public PolynomialBase(double[] coefficients, Func<double[], Complex[]> solveStrategy)
    {
        this.coefficients = coefficients;
        this.solveStrategy = solveStrategy;
    }

    public Complex[] FindRoots() => solveStrategy(coefficients);
}

public class LinearPolynomial : PolynomialBase
{
    public LinearPolynomial(double[] coefficients) : base(coefficients, PolynomialStrategies.SolveLinear) { }
}

public class QuadraticPolynomial : PolynomialBase
{
    public QuadraticPolynomial(double[] coefficients) : base(coefficients, PolynomialStrategies.SolveQuadratic) { }
}

public static class PolynomialStrategies
{
    public static Complex[] SolveLinear(double[] coefficients)
    {
        if (coefficients.Length != 2)
            throw new ArgumentException("A linear equation must have exactly 2 coefficients.");
        double a = coefficients[0];
        double b = coefficients[1];
        if (a == 0)
            throw new InvalidOperationException("No complex roots exist.");
        return new Complex[] { new Complex(-b / a, 0) };
    }

    public static Complex[] SolveQuadratic(double[] coefficients)
    {
        if (coefficients.Length != 3)
            throw new ArgumentException("A quadratic equation must have exactly 3 coefficients.");
        double a = coefficients[0];
        double b = coefficients[1];
        double c = coefficients[2];
        double discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            Complex sqrtDiscriminant = new Complex(0, Math.Sqrt(-discriminant));
            return new Complex[]
            {
                new Complex(-b / (2 * a), sqrtDiscriminant.Y),
                new Complex(-b / (2 * a), -sqrtDiscriminant.Y)
            };
        }
        else
        {
            double sqrtDiscriminant = Math.Sqrt(discriminant);
            return new Complex[]
            {
                new Complex((-b + sqrtDiscriminant) / (2 * a), 0),
                new Complex((-b - sqrtDiscriminant) / (2 * a), 0)
            };
        }
    }
}
