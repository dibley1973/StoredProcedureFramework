using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    /// <summary>
    /// Represents a "normal" stored procedure which has parameters and returns
    /// a single result set
    /// </summary>
    internal class ParametersAndReturnTypeStoredProcedure
        : StoredProcedureBase<
            List<ParametersAndReturnTypeStoredProcedure.Return>,
            ParametersAndReturnTypeStoredProcedure.Parameter>
    {
        public ParametersAndReturnTypeStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            public int Id { get; set; }
        }

        internal class Return
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Active { get; set; }
        }
    }
}