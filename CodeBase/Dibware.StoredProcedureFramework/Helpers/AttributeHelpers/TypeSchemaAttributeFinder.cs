using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.Generics;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Helpers.AttributeHelpers
{
    public class TypeSchemaAttributeFinder
    {
        #region Fields

        private readonly Type _type;
        private SchemaAttribute _attribute;

        #endregion

        #region Construtors

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSchemaAttributeFinder"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public TypeSchemaAttributeFinder(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            _type = type;

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
            IEnumerable<SchemaAttribute> attributes = _type.GetCustomAttributes<SchemaAttribute>();
            _attribute = attributes.FirstOrDefault();
        }
        #endregion
    }
}