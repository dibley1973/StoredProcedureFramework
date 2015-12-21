using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Data;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class ColumnSqlDbTypeHelper
    {
        #region Fields

        private readonly PropertyInfo _property;
        private SqlDbType _sqlDbType;
        private DbTypeAttribute _sqlDbTypeAttribute;

        #endregion

        #region Construtors

        public ColumnSqlDbTypeHelper(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            _property = property;
        }

        #endregion

        #region Public Members

        public ColumnSqlDbTypeHelper Build()
        {
            SetParameterDbTypeAttributeIfExists();
            SetSqlDbType();
            return this;
        }

        /// <summary>
        /// Gets the SQL database type.
        /// </summary>
        /// <value>
        /// The SqlDbType.
        /// </value>
        public SqlDbType SqlDbType
        {
            get { return _sqlDbType; }
        }

        #endregion

        #region Private members

        private bool HasParameterDbTypeAttribute
        {
            get { return _sqlDbTypeAttribute != null; }
        }

        private void SetSqlDbType()
        {
            if (HasParameterDbTypeAttribute)
            {
                SetSqlDbTypeFromAttribute();
            }
            else
            {
                SetSqlDbTypeFromPropertyClrType();
            }
        }

        private void SetSqlDbTypeFromAttribute()
        {
            _sqlDbType = _sqlDbTypeAttribute.Value;
        }

        private void SetSqlDbTypeFromPropertyClrType()
        {
            _sqlDbType = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(_property.PropertyType);
        }

        private void SetParameterDbTypeAttributeIfExists()
        {
            _sqlDbTypeAttribute = _property.GetAttribute<DbTypeAttribute>();
        }

        #endregion
    }
}