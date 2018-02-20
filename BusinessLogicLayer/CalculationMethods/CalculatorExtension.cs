using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.CalculationMethods
{
    public static class CalculatorExtension
    {
        public static double CalculateStandardDeviation(this List<double> numbers)
        {
            var average = numbers.Average();
            var variance = numbers.Sum(value => Math.Pow(value - average, 2));
            variance = variance / average;
            return Math.Sqrt(variance);
        }
    }
}