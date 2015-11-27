using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    /// <summary>
    /// Represents the most basic of stored procedures. This stored
    /// procedure does not have any parameters, neither does it return
    /// any result, it just performs an action.
    /// </summary>
    internal class MostBasicStoredProcedure
        : NoParametersNoReturnTypeStoredProcedureBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MostBasicStoredProcedure"/> 
        /// class without parameters.
        /// </summary>
        public MostBasicStoredProcedure()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MostBasicStoredProcedure"/> 
        /// class with parameters and procedure name.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        public MostBasicStoredProcedure(string procedureName)
            : base(procedureName)
        {
        }

        #endregion
    }
}
