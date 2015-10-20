using Dibware.StoredProcedureFramework.Base;
using System;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    public class NotFullyConstructedStoredProcedure
        : NoParametersNoReturnTypeStoredProcedureBase
    {
        public NotFullyConstructedStoredProcedure()
        {
            EraseProcedureNameValue();
        }

        private void EraseProcedureNameValue()
        {
            Type type = GetType();
            var baseType = type.BaseType;
            if (baseType != null)
            {
                var baseBaseType = baseType.BaseType;
                if (baseBaseType != null)
                {
                    PropertyInfo pi = baseBaseType.GetProperty("ProcedureName",
                        BindingFlags.Public | BindingFlags.Instance);

                    if (pi != null) pi.SetValue(this, "");
                }
            }
        }
    }
}
