using System;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class PropertyNameHelper
    {
        #region Fields

        private readonly PropertyInfo _property;
        private string _name;
        private NameAttribute _nameAttribute;

        #endregion

        #region Construtors

        public PropertyNameHelper(PropertyInfo property)
        {
            if(property == null) throw new ArgumentNullException("property");

            _property = property;
        }

        #endregion

        #region Public Members

        public PropertyNameHelper Build()
        {
            SetNameAttributeIfExists();
            SetName();
            return this;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return _name; }
        }

        #endregion

        #region Private members

        private bool HasNameAttribute
        {
            get { return _nameAttribute != null; }
        }

        private void SetName()
        {
            if (HasNameAttribute)
            {
                SetNameFromAttribute();
            }
            else
            {
                SetNameFromPropertyName();
            }
        }

        private void SetNameFromAttribute()
        {
            _name = _nameAttribute.Value;
        }

        private void SetNameFromPropertyName()
        {
            _name = _property.Name;
        }

        private void SetNameAttributeIfExists()
        {
            _nameAttribute = _property.GetAttribute<NameAttribute>();
        }

        #endregion
    }
}
