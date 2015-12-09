using Dibware.StoredProcedureFramework.Resources;
using System;

namespace Dibware.StoredProcedureFramework.Exceptions
{
    /// <summary>
    /// The exception thrown when a nullable type was unexpectedly encounterd
    /// </summary>
    public class NullableFieldTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullableFieldTypeException"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actualType">The actual type.</param>
        public NullableFieldTypeException(string fieldName,
            Type expectedType, Type actualType)
            : base(CreateMessage(fieldName, expectedType, actualType))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullableFieldTypeException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public NullableFieldTypeException(string message,
           Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="actualType">The actual type.</param>
        /// <returns></returns>
        private static string CreateMessage(string fieldName,
            Type expectedType, Type actualType)
        {
            string messageFormat = ExceptionMessages.FieldNotNullableTypeFormat;
            return string.Format(messageFormat, fieldName, expectedType, actualType);
        }
    }
}