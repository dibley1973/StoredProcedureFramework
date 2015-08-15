using Dibware.StoredProcedureFramework.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

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



        //public static List<TReturnType> ExecuteStoredProcedure2<TProcedure>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure2<TReturnType, TParameterType>
        //    where TReturnType : class
        //    where TParameterType : class
        //{

        //    return new List<IReturnType>();

        //}

        //public static void ExecuteStoredProcedure2<TProcedure>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //{
        //    // do some work
        //}

        //public static void ExecuteStoredProcedure3<IStoredProcedure<TReturnType, TParameterType>>(IStoredProcedure procedure)
        //    where TReturnType : class
        //    where TParameterType : class
        //{
        //    // do some work
        //}

        //public static void ExecSproc<TProcedure, TReturnType, TParameterType>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //{
        //    // do some work
        //}

        //public static void DoAction3<TProcedure, TReturnType, TParameterType>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //{
        //    // do some work
        //}

        //public static void DoAction<TReturnType, TParameterType>
        //    (IStoredProcedure<TReturnType, TParameterType> procedure)
        //    where TReturnType : class
        //    where TParameterType : class
        //{
        //    // do some work
        //}

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static List<TReturnType> ExecSproc<TReturnType, TParameterType>(
            this DbConnection connection,
            IStoredProcedure<TReturnType, TParameterType> procedure,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null)
            where TReturnType : class
            where TParameterType : class
        {
            string procedureName = procedure.GetTwoPartName();
            Type parameterType = typeof(TParameterType);
            Type returnType = typeof(TReturnType);

            // Prepare the parameters if any exist
            IEnumerable<SqlParameter> procedureParameters =
                (procedure.Parameters is NullStoredProcedureParameters) ?
                null :
                GetProcedureParameters(procedure);

            // Populate results using an overload
            var results = ExecSproc<TReturnType>(
                connection,
                procedureName,
                returnType,
                procedureParameters,
                commandTimeout,
                commandBehavior,
                transaction);

            // return the results
            return results;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        public static List<TReturnType> ExecSproc<TReturnType>(
            this DbConnection connection,
            string procedureName,
            Type outputType,
            IEnumerable<SqlParameter> procedureParameters = null,
            int? commandTimeout = null,
            CommandBehavior commandBehavior = CommandBehavior.Default,
            SqlTransaction transaction = null) where TReturnType : class
        {
            throw new NotImplementedException();
        }

        //public static void DoAction4<TProcedure, TReturnType, TParameterType>(TProcedure procedure)
        //    where TProcedure : IStoredProcedure<TReturnType, TParameterType>
        //    where TReturnType : class
        //    where TParameterType : class
        //{
        //    // do some work
        //}

        private static IEnumerable<SqlParameter> GetProcedureParameters<TReturnType, TParameterType>(
            IStoredProcedure<TReturnType, TParameterType> procedure)
            where TReturnType : class
            where TParameterType : class
        {
            // create mapped properties
            var mappedProperties = typeof(TParameterType).GetMappedProperties();

            // Create parameters
            var sqlParameters = mappedProperties.ToSqlParameters();

            // Populate parameters
            PopulateParameters(sqlParameters, procedure);

            // Return parameters
            return sqlParameters;
        }

        private static void PopulateParameters<TReturnType, TParameterType>(
            ICollection<SqlParameter> sqlParameters,
            IStoredProcedure<TReturnType, TParameterType> procedure)
            where TReturnType : class
            where TParameterType : class
        {
            throw new NotImplementedException();
        }

    }
}