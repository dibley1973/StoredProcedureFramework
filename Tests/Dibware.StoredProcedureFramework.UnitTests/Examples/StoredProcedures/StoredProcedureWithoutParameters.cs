using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    // Verbose way of defining procedure
    //internal class StoredProcedureWithoutParameters
    //    : StoredProcedureBase<StoredProcedureWithoutParametersResultSet, NullStoredProcedureParameters>
    //{
    //    public StoredProcedureWithoutParameters()
    //        : base(new NullStoredProcedureParameters())
    //    {
    //    }
    //}

    //internal class StoredProcedureWithoutParameters
    //    : NoParametersStoredProcedureBase<List<StoredProcedureWithoutParametersReturnType>>
    //{
    //}

    //internal class StoredProcedureWithoutParameters
    //    : NoParametersStoredProcedureBase<StoredProcedureWithoutParametersResultSet>
    //{
    //}

    //internal class StoredProcedureWithoutParametersResultSet
    //{
    //    public List<StoredProcedureWithoutParametersReturnType> RecordSet { get; set; }

    //    public StoredProcedureWithoutParametersResultSet()
    //    {
    //        RecordSet = new List<StoredProcedureWithoutParametersReturnType>();
    //    }
    //}

    //internal class StoredProcedureWithoutParametersReturnType
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}