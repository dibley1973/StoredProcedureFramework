using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Generics;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers.AttributeHelpers
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

            SetAttributeIfExists();
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Gets a value indicating whether this instance has found an attribute.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has found an attribute; otherwise, <c>false</c>.
        /// </value>
        public bool HasFoundAttribute
        {
            get { return _attribute != null; }
        }

        /// <summary>
        /// Gets the result containing the attribute if one was found.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Maybe<NameAttribute> GetResult()
        {
            if (!HasFoundAttribute)
            {
                return new Maybe<NameAttribute>();
            }
            return new Maybe<NameAttribute>(_attribute);  
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