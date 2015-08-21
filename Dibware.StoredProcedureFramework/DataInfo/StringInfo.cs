using System;

namespace Dibware.StoredProcedureFramework.DataInfo
{
    /// <summary>
    /// Represents information about a string
    /// </summary>
    public class StringInfo
    {
        #region Fields

        private bool _isNull = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is null.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is null; otherwise, <c>false</c>.
        /// </value>
        public bool IsNull
        {
            get { return _isNull; }
            private set { _isNull = value; }
        }

        //public bool IsNull { get; private set; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringInfo"/> class.
        /// </summary>
        public StringInfo()
        {
            IsNull = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringInfo"/> struct.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        public StringInfo(int length)
            : this()
        {
            IsNull = false;
            Length = length;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a stringInfo from the specified string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public static StringInfo FromString(string value)
        {
            // Check for
            if (value == null) return new StringInfo();

            return new StringInfo(value.Length);
        }

        #endregion
    }
}
