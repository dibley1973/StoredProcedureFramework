using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public static class SqlParameterHelper
    {
        public static ICollection<SqlParameter> CreateSqlParametersFromPropertyInfoArray(
            PropertyInfo[] propertyInfoArray)
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
