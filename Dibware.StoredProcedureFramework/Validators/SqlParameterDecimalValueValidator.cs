using Dibware.StoredProcedureFramework.DataInfo;
using Dibware.StoredProcedureFramework.Exceptions;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Validators
{
    public class SqlParameterDecimalValueValidator
    {
        /// <summary>
        /// Determines whether the specified decimal value is valid for 
        /// the specifed SqlParameter and throws an exception if not.
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
            if (!isValid) throw new SqlParameterOutOfRangeException(
                sqlParameter,
                decimalInfo.Precision, decimalInfo.Scale);
        }
    }
}