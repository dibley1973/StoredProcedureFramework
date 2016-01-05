using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    [Name("SizeAttributeStoredProcedure")]
    internal class CorrectSizeAttributeStoredProcedure
        : StoredProcedureBase<List<CorrectSizeAttributeStoredProcedure.Return>, CorrectSizeAttributeStoredProcedure.Parameter>
    {
        public CorrectSizeAttributeStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            [Size(20)]
            public string Value1 { get; set; }
        }

        internal class Return
        {
            [Size(255)]
            public string Value1 { get; set; }
        }
    }
}
