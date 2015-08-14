using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Contracts;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class DbConnectionExtensions2
    {
        public static List<IReturnType> ExecuteStoredProcedure1<TProcedure>(TProcedure procedure1)
            where TProcedure : IStoredProcedure2<IReturnType, IParameterType>
        {

            return new List<IReturnType>();

        }


        public static List<IReturnType> ExecuteStoredProcedure<TProcedure>(TProcedure procedure1)
            where TProcedure : IStoredProcedure<IReturnType, IParameterType>
        {

            return new List<IReturnType>();

        }

       

        //public static List<TReturnType> ExecuteStoredProcedure2<TProcedure>(TProcedure procedure1)
        //    where TProcedure : IStoredProcedure2<TReturnType, TParameterType>
        //    where TReturnType : class
        //    where TParameterType : class
        //{

        //    return new List<IReturnType>();

        //}

        //public static void ExecuteStoredProcedure2<TProcedure>(TProcedure procedure1)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //{
        //    // do some work
        //}

        //public static void ExecuteStoredProcedure3<IStoredProcedure<TReturnType, TParameterType>>(IStoredProcedure procedure1)
        //    where TReturnType : class
        //    where TParameterType : class
        //{
        //    // do some work
        //}

        //public static void DoAction<TProcedure, TReturnType, TParameterType>(TProcedure procedure1)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //{
        //    // do some work
        //}

        //public static void DoAction3<TProcedure, TReturnType, TParameterType>(TProcedure procedure1)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //{
        //    // do some work
        //}

        public static void DoAction<TReturnType, TParameterType>
            (IStoredProcedure<TReturnType, TParameterType> procedure1)
            where TReturnType : class
            where TParameterType : class
        {
            // do some work
        }

        public static List<TReturnType> GetAction<TReturnType, TParameterType>
            (IStoredProcedure<TReturnType, TParameterType> procedure1)
            where TReturnType : class
            where TParameterType : class
        {
            // do some work
            return new List<TReturnType>();
        }

        public static void DoAction4<TProcedure, TReturnType, TParameterType>(TProcedure procedure1)
            where TProcedure : IStoredProcedure<TReturnType, TParameterType>
            where TReturnType : class
            where TParameterType : class
        {
            // do some work
        }

    }
}