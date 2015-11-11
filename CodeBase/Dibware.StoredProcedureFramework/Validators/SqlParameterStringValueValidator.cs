using Dibware.StoredProcedureFramework.DataInfo;
using Dibware.StoredProcedureFramework.Exceptions;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Validators
{
    public class SqlParameterStringValueValidator
    {
        /// <summary>
        /// Determines whether the specific string value is valid for 
        /// the specifed SqlParameter and throws an exception if not.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="sqlParameter">The SQL parameter.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Validate(string value, SqlParameter sqlParameter)
        {
            StringInfo stringInfo = StringInfo.FromString(value);
            bool isValid = (
                stringInfo.Length <= sqlParameter.Size
                );

            if (!isValid) throw new SqlParameterOutOfRangeException(
                sqlParameter.ParameterName,
                sqlParameter.Size,
                stringInfo.Length);
        }
    }
}
