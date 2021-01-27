using System;
using UnityEngine;

namespace Utils
{
    public static class Polynomial
    {
        public static Tuple<double, double> QuadraticFormula(double a, double b, double c)
        {
            var discriminant = Math.Pow(b, 2) - 4 * a * c;
            if (discriminant < 0)
            {
                Debug.LogWarning("Polynomial has complex roots! Zeros will be given instead.");
                return new Tuple<double, double>(0, 0);
            }

            var discriminantRoot = Math.Sqrt(discriminant);
            return new Tuple<double, double>((-b + discriminantRoot) / (2 * a), (-b - discriminantRoot) / (2 * a));
        }
    }
}