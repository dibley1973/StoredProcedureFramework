using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    [Name("SizeAttributeStoredProcedure")]
    internal class TooLargeValueButNoSizeAttributeStoredProcedure
        : StoredProcedureBase<
            List<TooLargeValueButNoSizeAttributeStoredProcedure.Return>,
            TooLargeValueButNoSizeAttributeStoredProcedure.Parameter>
    {
        public TooLargeValueButNoSizeAttributeStoredProcedure(TooLargeValueButNoSizeAttributeStoredProcedure.Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
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