using System;
using System.Reflection;
using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    public class NotFullyConstructedStoredProcedure
        : NoParametersNoReturnTypeStoredProcedureBase
    {
        public NotFullyConstructedStoredProcedure()
        {
            Type type = GetType();
            var baseType = type.BaseType;
            if (baseType != null)
            {
                var baseBaseType = baseType.BaseType;

                if (baseBaseType != null)
                {
                    FieldInfo fi = baseBaseType.GetField("_procedureName",
                        BindingFlags.NonPublic | BindingFlags.Instance);

                    if (fi != null) fi.SetValue(this, "");
                }
            }
        }
    }
}
