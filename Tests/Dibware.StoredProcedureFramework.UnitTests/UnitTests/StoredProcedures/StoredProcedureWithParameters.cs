
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedureParameters;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    /// <summary>
    /// Represents the a basic stored procedure which although has parameters, 
    /// does not return any result, it just performs an action.
    /// </summary>
    internal class StoredProcedureWithParameters
        : NoReturnTypeStoredProcedureBase<BasicParameters>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithParameters"/> 
        /// class with parameters. This is the minimum requirement for constructing
        /// a stored procedure.
        /// </summary>
        public StoredProcedureWithParameters(BasicParameters parameters)
            : base(parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithParameters"/> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        public StoredProcedureWithParameters(string procedureName, 
            BasicParameters parameters)
            : base(procedureName, parameters)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureWithParameters"/> 
        /// class with parameters, schema name and procedure name
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <param name="parameters">The parameters.</param>
        public StoredProcedureWithParameters(string schemaName,
            string procedureName, 
            BasicParameters parameters)
            : base(schemaName, procedureName, parameters)
        {
        }

        #endregion
    }
}