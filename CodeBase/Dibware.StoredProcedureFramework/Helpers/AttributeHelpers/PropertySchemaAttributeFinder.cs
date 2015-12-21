using System;
using System.Reflection;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Helpers.AttributeHelpers
{
    public class PropertySchemaAttributeFinder
    {
        #region Fields

        private readonly PropertyInfo _property;
        private SchemaAttribute _attributeFound;

        #endregion

        #region Construtors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySchemaAttributeFinder"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <exception cref="System.ArgumentNullException">property</exception>
        public PropertySchemaAttributeFinder(PropertyInfo property)
        {
            if (property == null) throw new ArgumentNullException("property");

            _property = property;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Checks for attribute.
        /// </summary>
        /// <returns>The current instance for fluid API</returns>
        public PropertySchemaAttributeFinder CheckForAttribute()
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
        public SchemaAttribute AttributeFound
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
            _attributeFound = _property.GetAttribute<SchemaAttribute>();
        }
        #endregion
    }
}