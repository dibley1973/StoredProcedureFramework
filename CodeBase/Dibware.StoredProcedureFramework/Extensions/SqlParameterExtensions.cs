using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class SqlParameterExtensions
    {
        /// <summary>
        /// Requireses the precision and scale validation.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static bool RequiresPrecisionAndScaleValidation(
            this SqlParameter instance)
        {
            DbType instanceType = instance.DbType;
            return instanceType == DbType.Currency ||
                   instanceType == DbType.Decimal;
        }

        /// <summary>
        /// Requireses the length validation.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static bool RequiresLengthValidation(
            this SqlParameter instance)
        {
            DbType instanceType = instance.DbType;
            return instanceType == DbType.AnsiString ||
                   instanceType == DbType.AnsiStringFixedLength ||
                   instanceType == DbType.String ||
                   instanceType == DbType.StringFixedLength;
        }

        /// <summary>
        /// Determines whether this instance is string DbType or is nullable.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static bool IsStringOrIsNullable(
            this SqlParameter instance)
        {
            DbType instanceType = instance.DbType;
            return instanceType == DbType.AnsiString ||
                   instanceType == DbType.AnsiStringFixedLength ||
                   instanceType == DbType.String ||
                   instanceType == DbType.StringFixedLength ||
                   instance.IsNullable;
        }
    }
}