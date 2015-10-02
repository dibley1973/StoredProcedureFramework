
using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    internal class StoredProcedureWithoutParameters2
        : StoredProcedureBase<StoredProcedureWithoutParametersReturntype, NullStoredProcedureParameters>
    {
        public StoredProcedureWithoutParameters2()
            : base(new NullStoredProcedureParameters())
        {
        }
    }

    internal class StoredProcedureWithoutParameters
        : NoParametersStoredProcedureBase<StoredProcedureWithoutParametersResultSet>
    {
    }

    internal class StoredProcedureWithoutParametersResultSet
    {
        public List<StoredProcedureWithoutParametersReturntype> RecordSet { get; set; }

        public StoredProcedureWithoutParametersResultSet()
        {
            RecordSet = new List<StoredProcedureWithoutParametersReturntype>();
        }
    }


    internal class StoredProcedureWithoutParametersReturntype
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}