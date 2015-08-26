using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Extensions
{
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// The default parameter direction
        /// </summary>
        private const ParameterDirection DefaultParameterDirection = ParameterDirection.Input;


        /// <summary>
        /// Get an attribute for a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyinfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this PropertyInfo propertyinfo)
            where T : Attribute
        {
            var attributes = propertyinfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            return (T)attributes;
        }

        public static ICollection<SqlParameter> ToSqlParameters(
            this PropertyInfo[] propertyInfoArray)
        {
            // Validate arguments
            if (propertyInfoArray == null) throw new ArgumentNullException("propertyInfoArray");

            // Create a lsit of SqlParameters to return
            List<SqlParameter> results = new List<SqlParameter>();

            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                // create parameter and store default name - property name
                SqlParameter sqlParameter = new SqlParameter();

                // Get the name of the parameter. Attributes override the name so try and get this first
                NameAttribute nameAttribute = propertyInfo.GetAttribute<NameAttribute>();
                sqlParameter.ParameterName = (nameAttribute != null ? nameAttribute.Value : propertyInfo.Name);

                //TODO: complete this below!
                //// save direction (default is input)
                var directionAttribute = propertyInfo.GetAttribute<DirectionAttribute>();
                if (null != directionAttribute)
                sqlParameter.Direction = directionAttribute.Value;
                //sqlParameter.Direction = DefaultParameterDirection;

                // save size
                var sizeAttribute = propertyInfo.GetAttribute<SizeAttribute>();
                if (sizeAttribute != null) sqlParameter.Size = sizeAttribute.Value;

                // Set database type of parameter
                var typeAttribute = propertyInfo.GetAttribute<ParameterDbTypeAttribute>();
                if (typeAttribute != null) sqlParameter.SqlDbType = typeAttribute.Value;

                //// save user-defined type name
                //var typename = propertyInfo.GetAttribute<StoredProcAttributes.TypeName>();
                //if (null != typename)
                //    sqlParameter.TypeName = typename.Value;

                // Get parameter precision
                var precisionAttribute = propertyInfo.GetAttribute<PrecisionAttribute>();
                if (null != precisionAttribute) sqlParameter.Precision = precisionAttribute.Value;

                // Get parameter  scale
                var scaleAttribute = propertyInfo.GetAttribute<ScaleAttribute>();
                if (null != scaleAttribute) sqlParameter.Scale = scaleAttribute.Value;

                // add the parameter
                results.Add(sqlParameter);
            }

            return results;
        }
    }
}