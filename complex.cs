using System;

public class Complex
{
    public double X { get; private set; }
    public double Y { get; private set; }

    public static readonly Complex Zero = new Complex(0, 0);
    public static readonly Complex One = new Complex(1, 0);
    public static readonly Complex ImaginaryOne = new Complex(0, 1);

    public Complex(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static Complex Re(double x) => new Complex(x, 0);
    public static Complex Im(double y) => new Complex(0, y);
    public static Complex Sqrt(double x) => new Complex(Math.Sqrt(x), 0);

    public double Length => Math.Sqrt(X * X + Y * Y);

    public static Complex operator +(Complex a, Complex b) => new Complex(a.X + b.X, a.Y + b.Y);
    public static Complex operator -(Complex a, Complex b) => new Complex(a.X - b.X, a.Y - b.Y);
    public static Complex operator *(Complex a, Complex b) => new Complex(a.X * b.X - a.Y * b.Y, a.X * b.Y + a.Y * b.X);
    public static Complex operator /(Complex a, Complex b)
    {
        if (b.X == 0 && b.Y == 0)
            throw new DivideByZeroException();
        double denom = b.X * b.X + b.Y * b.Y;
        return new Complex((a.X * b.X + a.Y * b.Y) / denom, (a.Y * b.X - a.X * b.Y) / denom);
    }

    public static Complex operator +(Complex a) => a;
    public static Complex operator -(Complex a) => new Complex(-a.X, -a.Y);

    public override string ToString() => $"({X} + {Y}i)";
    public override bool Equals(object? obj)
    {
        if (obj is Complex other)
            return X == other.X && Y == other.Y;
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}
