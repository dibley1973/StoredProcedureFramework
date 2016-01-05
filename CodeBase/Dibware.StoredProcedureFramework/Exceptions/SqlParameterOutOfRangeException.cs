using System;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Resources;

namespace Dibware.StoredProcedureFramework.Exceptions
{
    public sealed class SqlParameterOutOfRangeException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParameterOutOfRangeException" /> class.
        /// </summary>
        /// <param name="parameter">The Sqlparemeter that was out of range.</param>
        /// <param name="actualPrecision">The actual precision.</param>
        /// <param name="actualScale">The actual scale.</param>
        public SqlParameterOutOfRangeException(
            SqlParameter parameter,
            int actualPrecision,
            int actualScale)
            : this(parameter.ParameterName,
            parameter.Precision, parameter.Scale,
            actualPrecision, actualScale)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParameterOutOfRangeException" /> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="expectedPrecision">The expected precision.</param>
        /// <param name="expectedScale">The expected scale.</param>
        /// <param name="actualPrecision">The actual precision.</param>
        /// <param name="actualScale">The actual scale.</param>
        private SqlParameterOutOfRangeException(
            string parameterName,
            int expectedPrecision,
            int expectedScale,
            int actualPrecision,
            int actualScale)
            : base(CreateMessage(parameterName, expectedPrecision, expectedScale, actualPrecision, actualScale))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParameterOutOfRangeException" /> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="expectedSize">The expected size.</param>
        /// <param name="actualSize">The actual size.</param>
        public SqlParameterOutOfRangeException(
            string parameterName,
            int expectedSize,
            int actualSize)
            : base(CreateMessage(parameterName, expectedSize, actualSize))
        {
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates the message containing name and expected and actual types.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="expectedPrecision">The expected precision.</param>
        /// <param name="expectedScale">The expected scale.</param>
        /// <param name="actualPrecision">The actual precision.</param>
        /// <param name="actualScale">The actual scale.</param>
        /// <returns>A constructed message conatain</returns>
        private static string CreateMessage(string parameterName,
            int expectedPrecision, int expectedScale,
            int actualPrecision, int actualScale)
        {
            string messageFormat = ExceptionMessages.ParameterPrecisionAndScaleOutOfRangeFormat;
            return string.Format(messageFormat, parameterName, expectedPrecision, expectedScale, actualPrecision, actualScale);
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="expectedSize">The expected Size.</param>
        /// <param name="actualSize">The actual size.</param>
        /// <returns></returns>
        private static string CreateMessage(string parameterName,
            int expectedSize, int actualSize)
        {
            string messageFormat = ExceptionMessages.ParameterLengthOutOfRangeFormat;
            return string.Format(messageFormat, parameterName, expectedSize, actualSize);
        }

        #endregion
    }
}