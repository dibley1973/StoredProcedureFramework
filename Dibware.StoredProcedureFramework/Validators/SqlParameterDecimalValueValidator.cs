using Dibware.StoredProcedureFramework.DataInfo;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Exceptions;

namespace Dibware.StoredProcedureFramework.Validators
{
    public class SqlParameterDecimalValueValidator
    {
        /// <summary>
        /// Determines whether the specified decimal value is valid for the specifed SqlParameter.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="sqlParameter">The SQL parameter.</param>
        public void Validate(decimal value, SqlParameter sqlParameter)
        {
            DecimalInfo decimalInfo = DecimalInfo.FromDecimal(value);
            bool isValid = (
                decimalInfo.Precision <= sqlParameter.Precision &&
                decimalInfo.Scale <= sqlParameter.Scale
            );
            if (!isValid)
                throw new SqlParameterOutOfRangeException(
                    sqlParameter.Precision, sqlParameter.Scale,
                    decimalInfo.Precision, decimalInfo.Scale
                    );
        }
    }
}