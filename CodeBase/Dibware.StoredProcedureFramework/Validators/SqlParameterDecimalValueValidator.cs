using System;
using Dibware.StoredProcedureFramework.DataInfo;
using Dibware.StoredProcedureFramework.Exceptions;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Validators
{
    public class SqlParameterDecimalValueValidator
    {
        private readonly SqlParameter _sqlParameter;
        private readonly decimal _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlParameterDecimalValueValidator"/> 
        /// class using the SqlParameter to validate against and the value to validat.
        /// </summary>
        /// <param name="sqlParameter">
        /// The Sql Parameter to validate against.
        /// </param>
        /// <param name="value">The value to validate.</param>
        public SqlParameterDecimalValueValidator(SqlParameter sqlParameter, decimal value)
        {
            if(sqlParameter == null) throw new ArgumentNullException("sqlParameter");

            _sqlParameter = sqlParameter;
            _value = value;
        }

        /// <summary>
        /// Determines whether the specified decimal value is valid for 
        /// the specifed SqlParameter and throws an exception if not.
        /// </summary>
        public void Validate()
        {
            DecimalInfo decimalInfo = DecimalInfo.FromDecimal(_value);
            bool isValid = (
                decimalInfo.Precision <= _sqlParameter.Precision &&
                decimalInfo.Scale <= _sqlParameter.Scale
            );
            if (!isValid) throw new SqlParameterOutOfRangeException(
                _sqlParameter,
                decimalInfo.Precision, decimalInfo.Scale);
        }
    }
}