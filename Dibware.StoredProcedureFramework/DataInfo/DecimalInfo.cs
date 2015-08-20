using System;
using System.Globalization;

namespace Dibware.StoredProcedureFramework.DataInfo
{
    /// <summary>
    /// Represents information about a decimal.
    /// code courtesy of Jason Kresowaty
    /// Ref: http://stackoverflow.com/questions/763942/calculate-system-decimal-precision-and-scale
    /// </summary>
    public struct DecimalInfo
    {
        #region Fields

        /// <summary>
        /// Gets the precision.
        /// </summary>
        /// <value>
        /// The precision.
        /// </value>
        public int Precision { get; private set; }
        /// <summary>
        /// Gets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public int Scale { get; private set; }
        /// <summary>
        /// Gets the trailing zeros.
        /// </summary>
        /// <value>
        /// The trailing zeros.
        /// </value>
        public int TrailingZeros { get; private set; }

        #endregion

        #region Constructors

        //public DecimalInfo(decimal value)
        //{

        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalInfo"/> struct.
        /// </summary>
        /// <param name="precision">The precision.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="trailingZeros">The trailing zeros.</param>
        public DecimalInfo(int precision, int scale, int trailingZeros)
            : this()
        {
            Precision = precision;
            Scale = scale;
            TrailingZeros = trailingZeros;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a DecimalInfo for the specified decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DecimalInfo FromDecimal(decimal value)
        {
            // converting to a string seems a sledge hammer approach, but 
            // referring to the StackOverflow thread below, it is one of the fastest!
            //string stringRepresentation = value.ToString(CultureInfo.InvariantCulture);
            string stringRepresentation = value.ToString(CultureInfo.CurrentCulture);
            char decimalSepeartor = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            const char negativeSymbol = '-';
            int precision = 0;
            int scale = 0;
            int trailingZeros = 0;
            bool inFractionPortion = false;
            bool nonZeroSeen = false;

            foreach (char currentCharacter in stringRepresentation)
            {
                // check if we are already in teh fraction portion
                if (inFractionPortion)
                {
                    // We are...
                    // now is the current character a zero?
                    if (currentCharacter == '0')
                    {
                        trailingZeros++;
                    }
                    else
                    {
                        nonZeroSeen = true;
                        trailingZeros = 0;
                    }
                    // Increment both precision and scale
                    precision += 1;
                    scale += 1;
                }
                else
                {
                    // We are not...
                    // Is the current character a decimal seperator
                    if (currentCharacter == decimalSepeartor)
                    {
                        // Then we have moved to the fraction portion
                        inFractionPortion = true;
                    }
                    else if (currentCharacter != negativeSymbol) // Not a negative sign
                    {
                        // Not a zero OR we have already seen non zero chracters
                        if (currentCharacter != '0' || nonZeroSeen)
                        {
                            nonZeroSeen = true;
                            precision += 1;
                        }
                    }
                }
            }

            // Handles cases where all digits are zeros.
            if (!nonZeroSeen) { precision += 1; }

            // Return the DecimalInfo
            return new DecimalInfo(precision, scale, trailingZeros);
        }

        #endregion
    }
}