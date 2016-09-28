using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Maestro.Common.Helpers
{
    /// <summary>
    /// NaturalSortComparer.
    /// </summary>
    public class NaturalSortComparer : IComparer<string>
    {
        private const string RegexString = @"([0-9]+(\.[0-9]+)?([Ee][+-]?[0-9]+)?)";
        private Dictionary<string, string[]> table = new Dictionary<string, string[]>();
        private readonly bool isAscending;

        /// <summary>
        /// Initializes a new instance of the <see cref="NaturalSortComparer"/> class.
        /// </summary>
        public NaturalSortComparer()
        {
            this.isAscending = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NaturalSortComparer"/> class.
        /// </summary>
        /// <param name="isAscendingOrder">if set to <c>true</c> [is ascending order].</param>
        public NaturalSortComparer(bool isAscendingOrder)
        {
            this.isAscending = isAscendingOrder;
        }

        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        int IComparer<string>.Compare(string x, string y)
        {
            if (x == y)
            {
                return 0;
            }
            
            string[] x1, y1;

            if (!table.TryGetValue(x, out x1))
            {
                x1 = Regex.Split(x.Replace(" ", string.Empty), RegexString);
                table.Add(x, x1);
            }

            if (!table.TryGetValue(y, out y1))
            {
                y1 = Regex.Split(y.Replace(" ", string.Empty), RegexString);
                table.Add(y, y1);
            }

            int returnVal;

            for (int i = 0; i < x1.Length && i < y1.Length; i++)
            {
                if (x1[i] != y1[i])
                {
                    returnVal = PartCompare(x1[i], y1[i]);

                    return isAscending ? returnVal : -returnVal;
                }
            }

            if (y1.Length > x1.Length)
            {
                returnVal = 1;
            }
            else if (x1.Length > y1.Length)
            {
                returnVal = -1;
            }
            else
            {
                returnVal = 0;
            }

            return isAscending ? returnVal : -returnVal;
        }

        private static int PartCompare(string left, string right)
        {
            decimal x, y;

            if (!decimal.TryParse(left, NumberStyles.Any, CultureInfo.InvariantCulture, out x))
            {
                return string.Compare(left, right, StringComparison.Ordinal);
            }

            if (!decimal.TryParse(right, NumberStyles.Any, CultureInfo.InvariantCulture, out y))
            {
                return string.Compare(left, right, StringComparison.Ordinal);
            }

            return x.CompareTo(y);
        }
    }
}