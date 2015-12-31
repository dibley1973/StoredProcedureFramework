using System;
using System.Reflection;
using System.Reflection.Emit;
using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Functions
{
    /// <summary>
    /// Used for testing purposes only.
    /// Resets the procedure name to empty string
    /// </summary>
    internal class NotFullyConstructedScalarFunction
        : SqlFunctionBase<
            int,
            NotFullyConstructedScalarFunction.Parameter>
    {
        public NotFullyConstructedScalarFunction() : base(null)
        {
            EraseProcedureNameValue();
        }

        private void EraseProcedureNameValue()
        {
            // This does not work!
            bool resetProcedureName = false;
            var baseType = GetTypeOfScalarFunctionBase();
            if (baseType != null)
            {
                PropertyInfo pi = baseType.GetProperty("ProcedureName",
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.SetProperty);

                if (pi != null && pi.GetSetMethod() != null)
                {
                    pi.SetValue(this, "");
                    resetProcedureName = true;
                }

                if (resetProcedureName == false)
                {

                    var stateFieldInfo = baseType.GetField("_state", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (stateFieldInfo != null)
                    {


                        var state = (SqlFunctionBaseState)stateFieldInfo.GetValue(this);

                        object boxedState = state;

                        var stateType = stateFieldInfo.FieldType;
                        FieldInfo fieldInfo =
                            stateType.GetField("ProcedureName",
                                BindingFlags.Public | BindingFlags.Instance);

                        if (fieldInfo != null)
                        {
                            fieldInfo.SetValue(boxedState, "");

                            state = (SqlFunctionBaseState)boxedState;
                            resetProcedureName = true;
                        }
                    }
                }

                if (!resetProcedureName) throw new InvalidOperationException("failed to reset procedure Name");
            }
        }

        private Type GetTypeOfScalarFunctionBase()
        {
            Type baseType = GetType();
            do
            {
                baseType = baseType.BaseType;
            }
            while (baseType != null && baseType != typeof(SqlFunctionBase));
            return baseType;
        }

        private delegate void SetHandler<T>(ref T source, object value) where T : struct;

        private static SetHandler<T> GetDelegate<T>(FieldInfo fieldInfo) where T : struct
        {
            var type = typeof(T);
            DynamicMethod dm = new DynamicMethod("setter", typeof(void), new Type[] { type.MakeByRefType(), typeof(object) }, type, true);
            ILGenerator setGenerator = dm.GetILGenerator();

            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.DeclareLocal(type);
            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldnull);
            setGenerator.Emit(OpCodes.Stind_Ref);
            setGenerator.Emit(OpCodes.Ldarg_1);
            setGenerator.Emit(OpCodes.Unbox_Any, fieldInfo.FieldType);
            setGenerator.Emit(OpCodes.Stfld, fieldInfo);
            setGenerator.Emit(OpCodes.Ldloc, 0);
            setGenerator.Emit(OpCodes.Box, type);
            setGenerator.Emit(OpCodes.Ret);
            return (SetHandler<T>)dm.CreateDelegate(typeof(SetHandler<>).MakeGenericType(type));
        }

        internal class Parameter
        {
            public int Value1 { get; set; }
        }
    }
}