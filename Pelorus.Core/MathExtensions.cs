using System;

namespace Pelorus.Core
{
    /// <summary>
    /// Mathematic methods for working with various numeric value types.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Truncates a decimal value to the given number of decimal point precision.
        /// </summary>
        /// <param name="source">Decimal value to truncate.</param>
        /// <param name="precision">Number of decimal place precision to truncate to.</param>
        /// <returns>Truncated decimal value.</returns>
        public static decimal Truncate(this decimal source, int precision)
        {
            int multiplier = (int) Math.Pow(10, precision);
            decimal result = Math.Truncate(source*multiplier)/multiplier;

            return result;
        }

        /// <summary>
        /// Truncates a double value to the given number of decimal point precision.
        /// </summary>
        /// <param name="source">Double value to truncate.</param>
        /// <param name="precision">Number of decimal place precision to truncate to.</param>
        /// <returns>Truncated double value.</returns>
        public static double Truncate(this double source, int precision)
        {
            int multiplier = (int) Math.Pow(10, precision);
            double result = Math.Truncate(source*multiplier)/multiplier;

            return result;
        }

        /// <summary>
        /// Truncates a float value to the given number of decimal point precision.
        /// </summary>
        /// <param name="source">Float value to truncate.</param>
        /// <param name="precision">Number of decimal place precision to truncate to.</param>
        /// <returns>Truncated float value.</returns>
        public static float Truncate(this float source, int precision)
        {
            int multiplier = (int) Math.Pow(10, precision);
            float result = (float) Math.Truncate(source*multiplier)/multiplier;

            return result;
        }
    }
}
