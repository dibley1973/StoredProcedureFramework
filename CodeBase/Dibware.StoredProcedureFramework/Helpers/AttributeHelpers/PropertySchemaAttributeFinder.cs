using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Reflection;
using Dibware.StoredProcedureFramework.Generics;

namespace Dibware.StoredProcedureFramework.Helpers.AttributeHelpers
{
    public class PropertySchemaAttributeFinder
    {
        #region Fields

        private readonly PropertyInfo _property;
        private SchemaAttribute _attribute;

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
        public Maybe<SchemaAttribute> GetResult()
        {
            if (!HasFoundAttribute)
            {
                return new Maybe<SchemaAttribute>();
            }
            return new Maybe<SchemaAttribute>(_attribute);
        }

        #endregion

        #region Private Members

        private void SetAttributeIfExists()
        {
            _attribute = _property.GetAttribute<SchemaAttribute>();
        }
        #endregion
    }
}