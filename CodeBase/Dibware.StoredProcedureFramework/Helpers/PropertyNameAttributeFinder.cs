using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Reflection;
using Dibware.StoredProcedureFramework.Extensions;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class PropertyNameAttributeFinder
    {
        #region Fields

        private readonly PropertyInfo _property;
        private NameAttribute _attribute;

        #endregion

        #region Construtors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyNameAttributeFinder"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <exception cref="System.ArgumentNullException">property</exception>
        public PropertyNameAttributeFinder(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            _property = property;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Checks for attribute.
        /// </summary>
        /// <returns></returns>
        public PropertyNameAttributeFinder CheckForAttribute()
        {
            SetAttributeIfExists();
            return this;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has attribute.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has attribute; otherwise, <c>false</c>.
        /// </value>
        public bool HasAttribute
        {
            get { return _attribute != null; }
        }

        /// <summary>
        /// Gets the Attribute if it exists; or null.
        /// </summary>
        /// <value>
        /// The Attribute.
        /// </value>
        public NameAttribute Attribute
        {
            get { return _attribute; }
        }

        #endregion

        #region Private Members

        private void SetAttributeIfExists()
        {
            _attribute = _property.GetAttribute<NameAttribute>();
        }
        #endregion
    }
}
