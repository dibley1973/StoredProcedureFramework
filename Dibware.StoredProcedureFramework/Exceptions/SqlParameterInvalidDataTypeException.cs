using Dibware.StoredProcedureFramework.Resources;
using System;

namespace Dibware.StoredProcedureFramework.Exceptions
{
    public class SqlParameterInvalidDataTypeException : Exception
    {
        public SqlParameterInvalidDataTypeException(Type expectedType, Type actualType)
            : base(CreateMessage(expectedType, actualType))
        {
        }

        private static string CreateMessage(Type expectedType, Type actualType)
        {
            string messageFormat = ExceptionMessages.ParameterInvalidTypeFormat;
            return string.Format(messageFormat, expectedType, actualType);
        }

    }
}