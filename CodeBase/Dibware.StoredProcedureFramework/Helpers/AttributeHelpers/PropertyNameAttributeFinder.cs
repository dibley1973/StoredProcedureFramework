using System;
using System.Reflection;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Helpers.AttributeHelpers
{
    public class PropertyNameAttributeFinder
    {
        #region Fields

        private readonly PropertyInfo _property;
        private NameAttribute _attributeFound;

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
        /// Looks for the attribute.
        /// </summary>
        /// <returns>The current instance for fluid API</returns>
        public PropertyNameAttributeFinder DetectAttribute()
        {
            SetAttributeIfExists();
            return this;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has found an attribute.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has found an attribute; otherwise, <c>false</c>.
        /// </value>
        public bool HasFoundAttribute
        {
            get { return _attributeFound != null; }
        }

        /// <summary>
        /// Gets the AttributeFound that was found.
        /// </summary>
        /// <value>
        /// The AttributeFound.
        /// </value>
        public NameAttribute AttributeFound
        {
            get
            {
                if (!HasFoundAttribute)
                {
                    throw new InvalidOperationException("No attribute type was found so cannot be returned. Hint: Use HasFoundAttribute first.");
                }
                return _attributeFound;
            }
        }

        #endregion

        #region Private Members

        private void SetAttributeIfExists()
        {
            _attributeFound = _property.GetAttribute<NameAttribute>();
        }
        #endregion
    }
}