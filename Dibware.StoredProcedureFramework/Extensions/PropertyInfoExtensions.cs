using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.StoredProcAttributes;

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
            if(propertyInfoArray == null) throw new ArgumentNullException("propertyInfoArray");

            // Create a lsit of SqlParameters to return
            List<SqlParameter> results = new List<SqlParameter>();

            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                // create parameter and store default name - property name
                SqlParameter sqlParameter = new SqlParameter();

                // Get the name of the parameter. Attributes override the name so try and get this first
                NameAttribute nameAttribute = propertyInfo.GetAttribute<StoredProcAttributes.NameAttribute>();
                sqlParameter.ParameterName = (nameAttribute != null ? nameAttribute.Value : propertyInfo.Name);

                //TODO: complete this below!
                //// save direction (default is input)
                //var dir = propertyInfo.GetAttribute<StoredProcAttributes.Direction>();
                //if (null != dir)
                //sqlParameter.Direction = dir.Value;
                sqlParameter.Direction = DefaultParameterDirection;

                //// save size
                //var size = propertyInfo.GetAttribute<StoredProcAttributes.Size>();
                //if (null != size)
                //    sqlParameter.Size = size.Value;

                // Set database type of parameter
                var typeAttribute = propertyInfo.GetAttribute<StoredProcAttributes.ParameterTypeAttribute>();
                if (typeAttribute != null)
                {
                    sqlParameter.SqlDbType = typeAttribute.Value;
                }

                //// save user-defined type name
                //var typename = propertyInfo.GetAttribute<StoredProcAttributes.TypeName>();
                //if (null != typename)
                //    sqlParameter.TypeName = typename.Value;

                //// save precision
                //var precision = propertyInfo.GetAttribute<StoredProcAttributes.Precision>();
                //if (null != precision)
                //    sqlParameter.Precision = precision.Value;

                //// save scale
                //var scale = propertyInfo.GetAttribute<StoredProcAttributes.Scale>();
                //if (null != scale)
                //    sqlParameter.Scale = scale.Value;

                // add the parameter
                results.Add(sqlParameter);
            }

            return results;
        }
    }
}