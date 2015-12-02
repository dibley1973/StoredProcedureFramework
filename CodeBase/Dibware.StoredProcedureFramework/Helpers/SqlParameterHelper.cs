//using System;
//using System.Collections.Generic;
//using System.Data;

//namespace Dibware.StoredProcedureFramework.Helpers
//{
//    public static class SqlParameterHelper
//    {
//        #region Fields

//        //private const int DefaultStringSize = 8000;
//        private static Dictionary<SqlDbType, Type> _sqlToClrTypeMaps; // = new Dictionary<SqlDbType, Type>();
//        private static Dictionary<Type, SqlDbType> _clrToSqlTypeMaps; // = new 

//        #endregion

//        #region Constructors

//        /// <summary>
//        /// Initializes the <see cref="SqlParameterHelper"/> class.
//        /// </summary>
//        static SqlParameterHelper()
//        {
//            CreateSqlToClrTypeMaps();
//            CreateClrToSqlTypeMaps();
//        }

//        #endregion

//        ///// <summary>
//        ///// Creates the SQL parameters from property information array.
//        ///// </summary>
//        ///// <param name="propertyInfoArray">The property information array.</param>
//        ///// <returns></returns>
//        ///// <exception cref="System.ArgumentNullException">propertyInfoArray</exception>
//        //public static ICollection<SqlParameter> CreateSqlParametersFromPropertyInfoArray(
//        //    PropertyInfo[] propertyInfoArray)
//        //{
//        //    if (propertyInfoArray == null) throw new ArgumentNullException("propertyInfoArray");

//        //    List<SqlParameter> results = new List<SqlParameter>();

//        //    foreach (PropertyInfo propertyInfo in propertyInfoArray)
//        //    {
//        //        SqlParameter sqlParameter = new SqlParameter();

//        //        NameAttribute nameAttribute = propertyInfo.GetAttribute<NameAttribute>();
//        //        sqlParameter.ParameterName = nameAttribute != null
//        //            ? nameAttribute.Value
//        //            : propertyInfo.Name;

//        //        var typeAttribute = propertyInfo.GetAttribute<ParameterDbTypeAttribute>();
//        //        sqlParameter.SqlDbType = typeAttribute != null
//        //            ? typeAttribute.Value
//        //            : GetSqlDbType(propertyInfo.PropertyType);

//        //        TrySetSqlParameterDirectionFromAttribute(propertyInfo, sqlParameter);
//        //        TrySetSqlParameterSizeFromAttribute(propertyInfo, sqlParameter);
//        //        TrySetSqlParameterPrecisionFromAttribute(propertyInfo, sqlParameter);
//        //        TrySetSqlParameterScaleFromAttribute(propertyInfo, sqlParameter);

//        //        results.Add(sqlParameter);
//        //    }

//        //    return results;
//        //}

//        //private static void TrySetSqlParameterDirectionFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
//        //{
//        //    var directionAttribute = propertyInfo.GetAttribute<DirectionAttribute>();
//        //    if (null != directionAttribute) sqlParameter.Direction = directionAttribute.Value;

//        //    // TODO: investigate if default direction needs to be set.
//        //    // default appears to be input any way
//        //    // sqlParameter.Direction = DefaultParameterDirection;
//        //}

//        //private static void TrySetSqlParameterPrecisionFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
//        //{
//        //    var precisionAttribute = propertyInfo.GetAttribute<PrecisionAttribute>();
//        //    if (null != precisionAttribute) sqlParameter.Precision = precisionAttribute.Value;
//        //}

//        //private static void TrySetSqlParameterScaleFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
//        //{
//        //    var scaleAttribute = propertyInfo.GetAttribute<ScaleAttribute>();
//        //    if (null != scaleAttribute) sqlParameter.Scale = scaleAttribute.Value;
//        //}

//        //private static void TrySetSqlParameterSizeFromAttribute(PropertyInfo propertyInfo, SqlParameter sqlParameter)
//        //{
//        //    // TODO: DW-2015-11-18 - Investigate a better solution than the default 
//        //    // size for string parameters when no SizeAttribute has been set. 
//        //    // Previously the framework used to default to ZERO, but this is not 
//        //    // very useful to most callers.
//        //    var sizeAttribute = propertyInfo.GetAttribute<SizeAttribute>();
//        //    sqlParameter.Size = sizeAttribute != null
//        //        ? sizeAttribute.Value
//        //        : DefaultStringSize;
//        //}

//        /// <summary>
//        /// Creates the SQL to color type maps.
//        /// </summary>
//        private static void CreateSqlToClrTypeMaps()
//        {
//            _sqlToClrTypeMaps = new Dictionary<SqlDbType, Type>
//            {
//                {SqlDbType.BigInt, typeof (Int64)},
//                {SqlDbType.Binary, typeof (Byte[])},
//                {SqlDbType.Bit, typeof (Boolean)},
//                {SqlDbType.Char, typeof (String)},
//                {SqlDbType.Date, typeof (DateTime)},
//                {SqlDbType.DateTime, typeof (DateTime)},
//                {SqlDbType.DateTime2, typeof (DateTime)},
//                {SqlDbType.DateTimeOffset, typeof (DateTimeOffset)},
//                {SqlDbType.Decimal, typeof (Decimal)},
//                {SqlDbType.Float, typeof (Double)},
//                {SqlDbType.Image, typeof (Byte[])},
//                {SqlDbType.Int, typeof (Int32)},
//                {SqlDbType.Money, typeof (Decimal)},
//                {SqlDbType.NChar, typeof (String)},
//                {SqlDbType.NText, typeof (String)},
//                {SqlDbType.NVarChar, typeof (String)},
//                {SqlDbType.Real, typeof (Single)},
//                {SqlDbType.SmallDateTime, typeof (DateTime)},
//                {SqlDbType.SmallInt, typeof (Int16)},
//                {SqlDbType.SmallMoney, typeof (Decimal)},
//                {SqlDbType.Text, typeof (String)},
//                {SqlDbType.Time, typeof (TimeSpan)},
//                {SqlDbType.Timestamp, typeof (Byte[])},
//                {SqlDbType.TinyInt, typeof (Byte)},
//                {SqlDbType.UniqueIdentifier, typeof (Guid)},
//                {SqlDbType.VarBinary, typeof (Byte[])},
//                {SqlDbType.VarChar, typeof (String)}
//            };
//        }

//        private static void CreateClrToSqlTypeMaps()
//        {
//            _clrToSqlTypeMaps = new Dictionary<Type, SqlDbType>
//            {
//                {typeof (Boolean), SqlDbType.Bit},
//                {typeof (Byte), SqlDbType.TinyInt},
//                {typeof (String), SqlDbType.NVarChar},
//                {typeof (DateTime), SqlDbType.DateTime},
//                {typeof (Int16), SqlDbType.SmallInt},
//                {typeof (Int32), SqlDbType.Int},
//                {typeof (Int64), SqlDbType.BigInt},
//                {typeof (Decimal), SqlDbType.Decimal},
//                {typeof (Double), SqlDbType.Float},
//                {typeof (Single), SqlDbType.Real},
//                {typeof (TimeSpan), SqlDbType.Time},
//                {typeof (Guid), SqlDbType.UniqueIdentifier},
//                {typeof (Byte[]), SqlDbType.Binary},
//                {typeof (char[]), SqlDbType.Char}
//            };
//        }

//        //internal static Type GetType(SqlDbType sqltype)
//        //{
//        //    // Ref:
//        //    // http://stackoverflow.com/a/1058348/254215
//        //    Type result;
//        //    _sqlToClrTypeMaps.TryGetValue(sqltype, out result);
//        //    return result;
//        //}

//        internal static SqlDbType GetSqlDbType(Type clrType)
//        {
//            // TODO add a catch for unknown types rather than assume NVarchar
//            SqlDbType result = SqlDbType.NVarChar;
//            _clrToSqlTypeMaps.TryGetValue(clrType, out result);
//            return result;
//        }
//    }
//}