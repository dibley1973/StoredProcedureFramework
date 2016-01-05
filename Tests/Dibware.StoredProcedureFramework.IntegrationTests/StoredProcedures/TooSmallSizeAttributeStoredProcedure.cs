using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    [Name("SizeAttributeStoredProcedure")]
    internal class TooSmallSizeAttributeStoredProcedure
        : StoredProcedureBase<List<TooSmallSizeAttributeStoredProcedure.Return>, TooSmallSizeAttributeStoredProcedure.Parameter>
    {
        public TooSmallSizeAttributeStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            [Size(10)]
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