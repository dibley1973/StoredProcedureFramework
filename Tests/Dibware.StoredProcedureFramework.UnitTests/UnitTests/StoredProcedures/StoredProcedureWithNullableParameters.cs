using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    internal class StoredProcedureWithNullableParameters
        : NoReturnTypeStoredProcedureBase<StoredProcedureWithNullableParameters.BasicParameters>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithNullableParameters"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        public StoredProcedureWithNullableParameters(BasicParameters parameters)
            : base(parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithNullableParameters"/> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        public StoredProcedureWithNullableParameters(string procedureName,
            BasicParameters parameters)
            : base(procedureName, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithNullableParameters"/> 
        /// class with parameters, schema name and procedure name
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        public StoredProcedureWithNullableParameters(string schemaName,
            string procedureName,
            BasicParameters parameters)
            : base(schemaName, procedureName, parameters)
        {
        }

        #endregion

        internal class BasicParameters
        {
            public int? Id { get; set; }
            public string Name { get; set; }
        }
    }
}