
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
        : NoParametersStoredProcedureBase<StoredProcedureWithoutParametersReturntype>
    {
    }


    internal class StoredProcedureWithoutParametersReturntype
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}