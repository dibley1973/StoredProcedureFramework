using Dibware.StoredProcedureFramework.Exceptions;
using System;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public static class ExceptionHelper
    {
        #region StoredProcedureConstructionException
        
        /// <summary>
        /// Creates the stored procedure construction exception with message parameter.
        /// </summary>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <returns>A StoredProcedureConstructionException constructed with the specified exception message</returns>
        public static StoredProcedureConstructionException CreateStoredProcedureConstructionException(
            string exceptionMessage)
        {
            return new StoredProcedureConstructionException(exceptionMessage);
        }

        /// <summary>
        /// Creates the stored procedure construction exception with message parameter.
        /// </summary>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <returns>
        /// A StoredProcedureConstructionException constructed with the specified exception message
        /// </returns>
        public static StoredProcedureConstructionException CreateStoredProcedureConstructionException(
            string exceptionMessage,
            Exception innerException)
        {
            return new StoredProcedureConstructionException(exceptionMessage, innerException);
        }

        #endregion
    }
}