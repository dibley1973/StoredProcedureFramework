using System;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public static class SqlDbTypeToClrTypeMapper
    {
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="SqlDbTypeToClrTypeMapper"/> class.
        /// </summary>
        static SqlDbTypeToClrTypeMapper()
        {
            CreateSqlTypeToClrTypeMaps();
        }

        #endregion

        #region Public  Members

        /// <summary>
        /// Gets the mapped CLR type for the specified SqlDbType.
        /// </summary>
        /// <param name="sqlDbType">The SqlDbType to get mapped type for.</param>
        /// <returns></returns>
        public static Type GetClrTypeFromSqlDbType(SqlDbType sqlDbType)
        {
            Type result;
            _sqlTypeToClrTypeMaps.TryGetValue(sqlDbType, out result);
            return result;
        }

        #endregion

        #region Private Members

        private static void CreateSqlTypeToClrTypeMaps()
        {
            _sqlTypeToClrTypeMaps = new Dictionary<SqlDbType, Type>
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
                {SqlDbType.VarChar, typeof (String)},
                {SqlDbType.Xml, typeof (String)}
            };
        }

        private static Dictionary<SqlDbType, Type> _sqlTypeToClrTypeMaps;

        #endregion
    }
}