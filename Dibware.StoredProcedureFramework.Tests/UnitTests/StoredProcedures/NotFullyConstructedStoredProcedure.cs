using Dibware.StoredProcedureFramework.Base;
using System;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    /// <summary>
    /// Used for testing purposes only.
    /// Resets the procedure name to empty string
    /// </summary>
    public class NotFullyConstructedStoredProcedure
        : NoParametersNoReturnTypeStoredProcedureBase
    {
        public NotFullyConstructedStoredProcedure()
        {
            EraseProcedureNameValue();
        }

        private void EraseProcedureNameValue()
        {
            var baseType = GetTypeOfStoredProcedureBase();
            if (baseType != null)
            {
                PropertyInfo pi = baseType.GetProperty("ProcedureName",
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance);

                if (pi != null) pi.SetValue(this, "");
            }
        }

        private Type GetTypeOfStoredProcedureBase()
        {
            Type baseType = GetType();
            do
            {
                baseType = baseType.BaseType;
            }
            while (baseType != null && baseType != typeof(StoredProcedureBase));
            return baseType;
        }
    }
}