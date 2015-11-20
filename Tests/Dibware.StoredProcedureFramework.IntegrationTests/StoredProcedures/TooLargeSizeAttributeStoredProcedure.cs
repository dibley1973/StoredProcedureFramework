using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    [Name("SizeAttributeStoredProcedure")]
    internal class TooLargeSizeAttributeStoredProcedure
        : StoredProcedureBase<List<TooLargeSizeAttributeStoredProcedure.Return>, TooLargeSizeAttributeStoredProcedure.Parameter>
    {
        public TooLargeSizeAttributeStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            [Size(30)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string Value1 { get; set; }
        }

        internal class Return
        {
            [Size(255)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string Value1 { get; set; }
        }
    }
}