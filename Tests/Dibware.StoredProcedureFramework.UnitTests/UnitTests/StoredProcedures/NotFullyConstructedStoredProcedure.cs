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
            // This does not work!
            bool resetProcedureName = false;
            var baseType = GetTypeOfStoredProcedureBase();
            if (baseType != null)
            {
                PropertyInfo pi = baseType.GetProperty("ProcedureName",
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.SetProperty);

                if (pi != null && pi.GetSetMethod() != null)
                {
                    pi.SetValue(this, "", null);
                    resetProcedureName = true;
                }

                if (resetProcedureName == false)
                {

                    var stateFieldInfo = baseType.GetField("_state", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (stateFieldInfo != null)
                    {
                        var state = (StoredProcedureBaseState)stateFieldInfo.GetValue(this);
                        object boxedState = state;

                        var stateType = stateFieldInfo.FieldType;
                        FieldInfo fieldInfo =
                            stateType.GetField("ProcedureName",
                                BindingFlags.Public | BindingFlags.Instance);

                        if (fieldInfo != null)
                        {
                            fieldInfo.SetValue(boxedState, "");

                            resetProcedureName = true;
                        }
                    }
                }

                if (!resetProcedureName) throw new InvalidOperationException("failed to reset procedure Name");
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