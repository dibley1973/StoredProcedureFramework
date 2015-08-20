using Dibware.StoredProcedureFramework.Resources;
using System;

namespace Dibware.StoredProcedureFramework.Exceptions
{
    public sealed class SqlParameterOutOfRangeException : Exception
    {
        #region Constructors

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SqlParameterOutOfRangeException"/> class.
        ///// </summary>
        //public SqlParameterOutOfRangeException()
        //{
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SqlParameterOutOfRangeException"/> class.
        ///// </summary>
        ///// <param name="parameter">The Sqlparemeter that was out of range.</param>
        //public SqlParameterOutOfRangeException(SqlParameter parameter, )
        //    : this(parameter.Precision, parameter.Scale)
        //{
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParameterOutOfRangeException" /> class.
        /// </summary>
        /// <param name="expectedPrecision">The expected precision.</param>
        /// <param name="expectedScale">The expected scale.</param>
        /// <param name="actualPrecision">The actual precision.</param>
        /// <param name="actualScale">The actual scale.</param>
        public SqlParameterOutOfRangeException(
            int expectedPrecision,
            int expectedScale,
            int actualPrecision,
            int actualScale)
            : base(CreateMessage(expectedPrecision, expectedScale, actualPrecision, actualScale))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParameterOutOfRangeException" /> class.
        /// </summary>
        /// <param name="expectedlength">The expectedlength.</param>
        /// <param name="actualLength">The actual length.</param>
        public SqlParameterOutOfRangeException(
            int expectedlength,
            int actualLength)
            : base(CreateMessage(expectedlength, actualLength))
        {
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SqlParameterOutOfRangeException"/> class.
        ///// </summary>
        ///// <param name="message">The message that describes the error.</param>
        //private SqlParameterOutOfRangeException(string message)
        //    : base(message)
        //{
        //}

        ///// <summary>
        ///// Initializes a new instance of the <see cref="SqlParameterOutOfRangeException"/> class.
        ///// </summary>
        ///// <param name="message">The message.</param>
        ///// <param name="innerException">
        ///// The exception that is the cause of the current exception, or a null 
        ///// reference (Nothing in Visual Basic) if no inner exception is specified.
        ///// </param>
        //public SqlParameterOutOfRangeException(string message, Exception innerException)
        //    : base(message, innerException)
        //{
        //}

        #endregion

        #region methods

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="expectedPrecision">The expected precision.</param>
        /// <param name="expectedScale">The expected scale.</param>
        /// <param name="actualPrecision">The actual precision.</param>
        /// <param name="actualScale">The actual scale.</param>
        /// <returns></returns>
        private static string CreateMessage(int expectedPrecision, int expectedScale, int actualPrecision, int actualScale)
        {
            string messageFormat = ExceptionMessages.ParameterPrecisionAndScaleOutOfRangeFormat;
            return string.Format(messageFormat, expectedPrecision, expectedScale, actualPrecision, actualScale);
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="expectedlength">The expectedlength.</param>
        /// <param name="actualLength">The actual length.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private static string CreateMessage(int expectedlength, int actualLength)
        {
            string messageformat = ExceptionMessages.ParameterLengthOutOfRangeFormat;
            throw new NotImplementedException();
        }

        #endregion
    }
}