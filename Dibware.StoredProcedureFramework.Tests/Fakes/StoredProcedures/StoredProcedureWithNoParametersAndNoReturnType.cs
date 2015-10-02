using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.Tests.Fakes.StoredProcedures
{
    internal class StoredProcedureWithNoParametersAndNoReturnType 
        : NoParametersNoReturnTypeStoredProcedureBase
    {
        public StoredProcedureWithNoParametersAndNoReturnType()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithNoParametersAndNoReturnType"/> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public StoredProcedureWithNoParametersAndNoReturnType(
            string procedureName)
            : base(procedureName)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithNoParametersAndNoReturnType"/> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="schemaName">name of the schema</param>
        /// <param name="procedureName">Name of the procedure.</param>
        public StoredProcedureWithNoParametersAndNoReturnType(
            string schemaName,
            string procedureName)
            : base(schemaName, procedureName)
        {}
    }
}