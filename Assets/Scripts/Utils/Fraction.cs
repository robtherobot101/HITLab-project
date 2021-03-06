﻿using System;
using UnityEngine;

// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading
namespace Utils
{
    [Serializable]
    public struct Fraction
    {
        public static readonly Fraction Zero = new Fraction(0, 1);

        [SerializeField] private int num;
        [SerializeField] private int den;

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0) throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
            num = numerator;
            den = denominator;
        }

        public int Numerator => num;
        public int Denominator => den;

        public bool Equals(Fraction other)
        {
            return num == other.num && den == other.den;
        }

        public override bool Equals(object obj)
        {
            return obj is Fraction other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (num * 397) ^ den;
            }
        }

        public static Fraction operator +(Fraction a)
        {
            return a;
        }

        public static Fraction operator -(Fraction a)
        {
            return new Fraction(-a.num, a.den);
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(a.num * b.den + b.num * a.den, a.den * b.den);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return a + -b;
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.num * b.num, a.den * b.den);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.num == 0) throw new DivideByZeroException();
            return new Fraction(a.num * b.den, a.den * b.num);
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            return (a - b).num == 0;
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return (a - b).num > 0;
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            return (a - b).num < 0;
        }

        public override string ToString()
        {
            return $"{num}/{den}";
        }

        public Fraction Simplify()
        {
            var gcd = Gcd(num, den);
            return new Fraction(num / gcd, den / gcd);
        }

        // https://github.com/drewnoakes/metadata-extractor-dotnet/blob/46ccdd489739ddc11dd5d4b41335290598df6ac1/MetadataExtractor/Rational.cs#L275
        private static int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
                if (a > b)
                    a %= b;
                else
                    b %= a;
            return a | b;
        }

        public float Value()
        {
            return num / (float) den;
        }
    }
}