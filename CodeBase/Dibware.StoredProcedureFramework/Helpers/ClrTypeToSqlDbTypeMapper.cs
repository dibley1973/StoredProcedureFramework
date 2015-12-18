using System;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.Helpers
{
    internal static class ClrTypeToSqlDbTypeMapper
    {
        #region Fields

        private static Dictionary<Type, SqlDbType> _clrTypeToSqlTypeMaps;

        #endregion
        
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="ClrTypeToSqlDbTypeMapper"/> class.
        /// </summary>
        static ClrTypeToSqlDbTypeMapper()
        {
            CreateClrTypeToSqlTypeMaps();
        }

        #endregion

        #region Public  Members

        /// <summary>
        /// Gets the mapped SqlDbType for the specified CLR type.
        /// </summary>
        /// <param name="clrType">The CLR Type to get mapped SqlDbType for.</param>
        /// <returns></returns>
        public static SqlDbType GetSqlDbTypeFromClrType(Type clrType)
        {
            EnsureTypeExists(clrType);

            SqlDbType result;
            _clrTypeToSqlTypeMaps.TryGetValue(clrType, out result);
            return result;
        }

        #endregion

        #region Private Members

        private static void CreateClrTypeToSqlTypeMaps()
        {
            _clrTypeToSqlTypeMaps = new Dictionary<Type, SqlDbType>
            {
                {typeof (Boolean), SqlDbType.Bit},
                {typeof (Boolean?), SqlDbType.Bit},
                {typeof (Byte), SqlDbType.TinyInt},
                {typeof (Byte?), SqlDbType.TinyInt},
                {typeof (String), SqlDbType.NVarChar},
                {typeof (DateTime), SqlDbType.DateTime},
                {typeof (DateTime?), SqlDbType.DateTime},
                {typeof (Int16), SqlDbType.SmallInt},
                {typeof (Int16?), SqlDbType.SmallInt},
                {typeof (Int32), SqlDbType.Int},
                {typeof (Int32?), SqlDbType.Int},
                {typeof (Int64), SqlDbType.BigInt},
                {typeof (Int64?), SqlDbType.BigInt},
                {typeof (Decimal), SqlDbType.Decimal},
                {typeof (Decimal?), SqlDbType.Decimal},
                {typeof (Double), SqlDbType.Float},
                {typeof (Double?), SqlDbType.Float},
                {typeof (Single), SqlDbType.Real},
                {typeof (Single?), SqlDbType.Real},
                {typeof (TimeSpan), SqlDbType.Time},
                {typeof (Guid), SqlDbType.UniqueIdentifier},
                {typeof (Guid?), SqlDbType.UniqueIdentifier},
                {typeof (Byte[]), SqlDbType.Binary},
                {typeof (Byte?[]), SqlDbType.Binary},
                {typeof (Char[]), SqlDbType.Char},
                {typeof (Char?[]), SqlDbType.Char}
            };
        }

        private static void EnsureTypeExists(Type clrType)
        {
            if (!_clrTypeToSqlTypeMaps.ContainsKey(clrType))
            {
                throw new ArgumentOutOfRangeException("clrType", @"No mapped type found for " + clrType);
            }
        }

        #endregion
    }
}