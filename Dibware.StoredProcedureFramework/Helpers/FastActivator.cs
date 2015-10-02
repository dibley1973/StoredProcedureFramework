//using System;
//using System.Collections.Generic;
//using System.Reflection.Emit;

//namespace Dibware.StoredProcedureFramework.Helpers
//{
//    // For use with no-parameter constructors. Also contains constants and utility methods
//    public static class FastActivator
//    {
//        // THIS VERSION NOT THREAD SAFE YET
//        // Investigate locking using http://stackoverflow.com/a/2045313/254215


//        static readonly Dictionary<Type, Func<object>> ConstructorCache = new Dictionary<Type, Func<object>>();

//        private const string DynamicMethodPrefix = "DM$_FastActivator_";

//        public static object CreateInstance(Type objType)
//        {
//            return GetConstructor(objType)();
//        }

//        private static Func<object> GetConstructor(Type objType)
//        {
//            Func<object> constructor;
//            if (!ConstructorCache.TryGetValue(objType, out constructor))
//            {
//                constructor = (Func<object>)BuildConstructorDelegate(objType, typeof(Func<object>), new Type[] { });
//                ConstructorCache.Add(objType, constructor);
//            }
//            return constructor;
//        }

//        internal static object BuildConstructorDelegate(Type objType, Type delegateType, Type[] argTypes)
//        {
//            var dynamicMethod = new DynamicMethod(DynamicMethodPrefix + objType.Name + "$" + argTypes.Length, objType, argTypes, objType);
//            ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
//            for (int argIdx = 0; argIdx < argTypes.Length; argIdx++)
//            {
//                //ilGenerator.Emit(OpCodes.Ldarg, argIdx);
//                if (argIdx < 4)
//                {
//                    switch (argIdx)
//                    {
//                        case 0:
//                            ilGenerator.Emit(OpCodes.Ldarg_0);
//                            break;
//                        case 1:
//                            ilGenerator.Emit(OpCodes.Ldarg_1);
//                            break;
//                        case 2:
//                            ilGenerator.Emit(OpCodes.Ldarg_2);
//                            break;
//                        case 3:
//                            ilGenerator.Emit(OpCodes.Ldarg_3);
//                            break;
//                    }
//                }
//                else
//                {
//                    ilGenerator.Emit(OpCodes.Ldarg_S, argIdx);
//                }
//            }
//            ilGenerator.Emit(OpCodes.Newobj, objType.GetConstructor(argTypes));
//            ilGenerator.Emit(OpCodes.Ret);
//            return dynamicMethod.CreateDelegate(delegateType);
//        }
//    }

//    // For use with one-parameter constructors, argument type = T1
//    public static class FastActivator<T1>
//    {
//        // THIS VERSION NOT THREAD SAFE YET
//        static readonly Dictionary<Type, Func<T1, object>> ConstructorCache = new Dictionary<Type, Func<T1, object>>();

//        public static object CreateInstance(Type objType, T1 arg1)
//        {
//            return GetConstructor(objType, new[] { typeof(T1) })(arg1);
//        }

//        public static Func<T1, object> GetConstructor(Type objType, Type[] argTypes)
//        {
//            Func<T1, object> constructor;
//            if (!ConstructorCache.TryGetValue(objType, out constructor))
//            {
//                constructor = (Func<T1, object>)FastActivator.BuildConstructorDelegate(objType, typeof(Func<T1, object>), argTypes);
//                ConstructorCache.Add(objType, constructor);
//            }
//            return constructor;
//        }
//    }
//}