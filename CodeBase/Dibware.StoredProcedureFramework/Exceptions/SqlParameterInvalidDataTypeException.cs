using Dibware.StoredProcedureFramework.Resources;
using System;

namespace Dibware.StoredProcedureFramework.Exceptions
{
    public class SqlParameterInvalidDataTypeException : Exception
    {
        public SqlParameterInvalidDataTypeException(string parameterName,
            Type expectedType, Type actualType)
            : base(CreateMessage(parameterName, expectedType, actualType))
        {}

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actualType">The actual type.</param>
        /// <returns></returns>
        private static string CreateMessage(string parameterName,
            Type expectedType, Type actualType)
        {
            string messageFormat = ExceptionMessages.ParameterInvalidTypeFormat;
            return string.Format(messageFormat, parameterName, expectedType, actualType);
        }
    }
}