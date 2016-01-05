using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.Generics;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Helpers.AttributeHelpers
{
    public class TypeNameAttributeFinder
    {
        #region Fields

        private readonly Type _type;
        private NameAttribute _attribute;

        #endregion

        #region Construtors

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeNameAttributeFinder"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public TypeNameAttributeFinder(Type type)
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
            IEnumerable<NameAttribute> attributes = _type.GetCustomAttributes<NameAttribute>();
            _attribute = attributes.FirstOrDefault();
        }
        #endregion
    }
}