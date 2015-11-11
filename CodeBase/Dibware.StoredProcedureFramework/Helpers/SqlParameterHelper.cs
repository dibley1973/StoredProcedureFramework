using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public static class SqlParameterHelper
    {
        #region Fields

        private static Dictionary<SqlDbType, Type> _sqlToClrTypeMaps; // = new Dictionary<SqlDbType, Type>();
        private static Dictionary<Type, SqlDbType> _clrToSqlTypeMaps; // = new 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="SqlParameterHelper"/> class.
        /// </summary>
        static SqlParameterHelper()
        {
            CreateSqlToClrTypeMaps();
            CreateClrToSqlTypeMaps();
        }

        #endregion

        /// <summary>
        /// Creates the SQL parameters from property information array.
        /// </summary>
        /// <param name="propertyInfoArray">The property information array.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">propertyInfoArray</exception>
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

                // Set database type of parameter from attribute if available
                var typeAttribute = propertyInfo.GetAttribute<ParameterDbTypeAttribute>();
                if (typeAttribute != null)
                {
                    sqlParameter.SqlDbType = typeAttribute.Value;
                }
                else
                {
                    // or fall back on CLR type
                    sqlParameter.SqlDbType = GetSqlDbType(propertyInfo.PropertyType);
                }

                TrySetSqlParameterDirectionFromAttribute(propertyInfo, sqlParameter);
                TrySetSqlParameterSizeFromAttribute(propertyInfo, sqlParameter);
                TrySetSqlParameterPrecisionFromAttribute(propertyInfo, sqlParameter);
                TrySetSqlParameterScaleFromAttribute(propertyInfo, sqlParameter);

                //// save user-defined type name
                //var typename = propertyInfo.GetAttribute<StoredProcAttributes.TypeName>();
                //if (null != typename)
                //    sqlParameter.TypeName = typename.Value;

                // add the parameter
                results.Add(sqlParameter);
            }

            return results;
        }

        private static void TrySetSqlParameterDirectionFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
        {
            var directionAttribute = propertyInfo.GetAttribute<DirectionAttribute>();
            if (null != directionAttribute)
                sqlParameter.Direction = directionAttribute.Value;

            // TODO: investigate if default direction needs to be set.
            // default appears to be input any way
            //sqlParameter.Direction = DefaultParameterDirection;
        }

        private static void TrySetSqlParameterPrecisionFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
        {
            var precisionAttribute = propertyInfo.GetAttribute<PrecisionAttribute>();
            if (null != precisionAttribute) sqlParameter.Precision = precisionAttribute.Value;
        }

        private static void TrySetSqlParameterScaleFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
        {
            var scaleAttribute = propertyInfo.GetAttribute<ScaleAttribute>();
            if (null != scaleAttribute) sqlParameter.Scale = scaleAttribute.Value;
        }

        private static void TrySetSqlParameterSizeFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
        {
            var sizeAttribute = propertyInfo.GetAttribute<SizeAttribute>();
            if (sizeAttribute != null) sqlParameter.Size = sizeAttribute.Value;
        }

        /// <summary>
        /// Creates the SQL to color type maps.
        /// </summary>
        private static void CreateSqlToClrTypeMaps()
        {
            _sqlToClrTypeMaps = new Dictionary<SqlDbType, Type>
            {
                {SqlDbType.BigInt, typeof (Int64)},
                {SqlDbType.Binary, typeof (Byte[])},
                {SqlDbType.Bit, typeof (Boolean)},
                {SqlDbType.Char, typeof (String)},
                {SqlDbType.Date, typeof (DateTime)},
                {SqlDbType.DateTime, typeof (DateTime)},
                {SqlDbType.DateTime2, typeof (DateTime)},
                {SqlDbType.DateTimeOffset, typeof (DateTimeOffset)},
                {SqlDbType.Decimal, typeof (Decimal)},
                {SqlDbType.Float, typeof (Double)},
                {SqlDbType.Image, typeof (Byte[])},
                {SqlDbType.Int, typeof (Int32)},
                {SqlDbType.Money, typeof (Decimal)},
                {SqlDbType.NChar, typeof (String)},
                {SqlDbType.NText, typeof (String)},
                {SqlDbType.NVarChar, typeof (String)},
                {SqlDbType.Real, typeof (Single)},
                {SqlDbType.SmallDateTime, typeof (DateTime)},
                {SqlDbType.SmallInt, typeof (Int16)},
                {SqlDbType.SmallMoney, typeof (Decimal)},
                {SqlDbType.Text, typeof (String)},
                {SqlDbType.Time, typeof (TimeSpan)},
                {SqlDbType.Timestamp, typeof (Byte[])},
                {SqlDbType.TinyInt, typeof (Byte)},
                {SqlDbType.UniqueIdentifier, typeof (Guid)},
                {SqlDbType.VarBinary, typeof (Byte[])},
                {SqlDbType.VarChar, typeof (String)}
            };
        }



        private static void CreateClrToSqlTypeMaps()
        {
            _clrToSqlTypeMaps = new Dictionary<Type, SqlDbType>
            {
                {typeof (Boolean), SqlDbType.Bit},
                {typeof (String), SqlDbType.NVarChar},
                {typeof (DateTime), SqlDbType.DateTime},
                {typeof (Int16), SqlDbType.Int},
                {typeof (Int32), SqlDbType.Int},
                {typeof (Int64), SqlDbType.Int},
                {typeof (Decimal), SqlDbType.Float},
                {typeof (Double), SqlDbType.Float},
                {typeof (char[]), SqlDbType.NVarChar}
            };
        }



        internal static Type GetType(SqlDbType sqltype)
        {
            // TODO: Consider something more like this:
            // http://stackoverflow.com/a/1058348/254215

            Type result;
            //Dictionary<SqlDbType, Type> sqlToClrTypeMaps = new Dictionary<SqlDbType, Type>();
            //sqlToClrTypeMaps.Add(SqlDbType.BigInt, typeof(Int64));
            //sqlToClrTypeMaps.Add(SqlDbType.Binary, typeof(Byte[]));
            //sqlToClrTypeMaps.Add(SqlDbType.Bit, typeof(Boolean));
            //sqlToClrTypeMaps.Add(SqlDbType.Char, typeof(String));
            //sqlToClrTypeMaps.Add(SqlDbType.Date, typeof(DateTime));
            //sqlToClrTypeMaps.Add(SqlDbType.DateTime, typeof(DateTime));
            //sqlToClrTypeMaps.Add(SqlDbType.DateTime2, typeof(DateTime));
            //sqlToClrTypeMaps.Add(SqlDbType.DateTimeOffset, typeof(DateTimeOffset));
            //sqlToClrTypeMaps.Add(SqlDbType.Decimal, typeof(Decimal));
            //sqlToClrTypeMaps.Add(SqlDbType.Float, typeof(Double));
            //sqlToClrTypeMaps.Add(SqlDbType.Image, typeof(Byte[]));
            //sqlToClrTypeMaps.Add(SqlDbType.Int, typeof(Int32));
            //sqlToClrTypeMaps.Add(SqlDbType.Money, typeof(Decimal));
            //sqlToClrTypeMaps.Add(SqlDbType.NChar, typeof(String));
            //sqlToClrTypeMaps.Add(SqlDbType.NText, typeof(String));
            //sqlToClrTypeMaps.Add(SqlDbType.NVarChar, typeof(String));
            //sqlToClrTypeMaps.Add(SqlDbType.Real, typeof(Single));
            //sqlToClrTypeMaps.Add(SqlDbType.SmallDateTime, typeof(DateTime));
            //sqlToClrTypeMaps.Add(SqlDbType.SmallInt, typeof(Int16));
            //sqlToClrTypeMaps.Add(SqlDbType.SmallMoney, typeof(Decimal));
            //sqlToClrTypeMaps.Add(SqlDbType.Text, typeof(String));
            //sqlToClrTypeMaps.Add(SqlDbType.Time, typeof(TimeSpan));
            //sqlToClrTypeMaps.Add(SqlDbType.Timestamp, typeof(Byte[]));
            //sqlToClrTypeMaps.Add(SqlDbType.TinyInt, typeof(Byte));
            //sqlToClrTypeMaps.Add(SqlDbType.UniqueIdentifier, typeof(Guid));
            //sqlToClrTypeMaps.Add(SqlDbType.VarBinary, typeof(Byte[]));
            //sqlToClrTypeMaps.Add(SqlDbType.VarChar, typeof(String));
            _sqlToClrTypeMaps.TryGetValue(sqltype, out result);
            return result;
        }

        internal static SqlDbType GetSqlDbType(Type clrType)
        {
            SqlDbType result = SqlDbType.NVarChar;
            //Dictionary<Type, SqlDbType> clrToSqlTypeMaps = new Dictionary<Type, SqlDbType>();
            //clrToSqlTypeMaps.Add(typeof(Boolean), SqlDbType.Bit);
            //clrToSqlTypeMaps.Add(typeof(String), SqlDbType.NVarChar);
            //clrToSqlTypeMaps.Add(typeof(DateTime), SqlDbType.DateTime);
            //clrToSqlTypeMaps.Add(typeof(Int16), SqlDbType.Int);
            //clrToSqlTypeMaps.Add(typeof(Int32), SqlDbType.Int);
            //clrToSqlTypeMaps.Add(typeof(Int64), SqlDbType.Int);
            //clrToSqlTypeMaps.Add(typeof(Decimal), SqlDbType.Float);
            //clrToSqlTypeMaps.Add(typeof(Double), SqlDbType.Float);
            //clrToSqlTypeMaps.Add(typeof(char[]), SqlDbType.NVarChar);
            _clrToSqlTypeMaps.TryGetValue(clrType, out result);

            // TODO add a catch for unknown types rather than assume NVarchar

            return result;
        }
    }
}
