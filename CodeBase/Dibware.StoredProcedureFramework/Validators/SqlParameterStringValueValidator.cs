using Dibware.StoredProcedureFramework.DataInfo;
using Dibware.StoredProcedureFramework.Exceptions;
using System;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Validators
{
    public class SqlParameterStringValueValidator
    {

        private readonly SqlParameter _sqlParameter;
        private readonly string _value;

        public SqlParameterStringValueValidator(SqlParameter sqlParameter, string value)
        {
            if (sqlParameter == null) throw new ArgumentNullException("sqlParameter");
            if (value == null) throw new ArgumentNullException("value");

            _sqlParameter = sqlParameter;
            _value = value;
        }

        /// <summary>
        /// Determines whether the specific string value is valid for 
        /// the specifed SqlParameter and throws an exception if not.
        /// </summary>
        /// <exception cref="SqlParameterOutOfRangeException"></exception>
        public void Validate()
        {
            StringInfo stringInfo = StringInfo.FromString(_value);
            bool isValid = (
                stringInfo.Length <= _sqlParameter.Size
                );

            if (!isValid) throw new SqlParameterOutOfRangeException(
                _sqlParameter.ParameterName,
                _sqlParameter.Size,
                stringInfo.Length);
        }
    }
}