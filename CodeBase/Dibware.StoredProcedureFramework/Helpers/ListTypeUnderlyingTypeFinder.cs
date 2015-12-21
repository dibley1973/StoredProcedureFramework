using System;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class ListTypeUnderlyingTypeFinder
    {
        #region Fields

        private readonly Type _listType;
        private readonly Type[] _interfaceTypes;
        private Type _underlyingTypeFound;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListTypeUnderlyingTypeFinder"/> class.
        /// </summary>
        /// <param name="listListType">Type of the underlying.</param>
        /// <exception cref="System.ArgumentNullException">listListType</exception>
        public ListTypeUnderlyingTypeFinder(Type listListType)
        {
            if (listListType == null) throw new ArgumentNullException("listListType");

            _listType = listListType;
            _interfaceTypes = _listType.GetInterfaces();
        }


        #endregion

        #region Public Members

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <returns>The current instance for fluid API</returns>
        public ListTypeUnderlyingTypeFinder CheckForUnderlyingType()
        {
            foreach (Type interfaceType in _interfaceTypes)
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    _underlyingTypeFound = interfaceType.GetGenericArguments()[0];
                    break;
                }
            }

            return this;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has found an underlying an type.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has founc an underlying type; otherwise, <c>false</c>.
        /// </value>
        public bool HasFoundUnderlyingType
        {
            get { return _underlyingTypeFound != null; }
        }

        /// <summary>
        /// Gets the underlying type found or .
        /// </summary>
        /// <value>
        /// The type of the underlying.
        /// </value>
        public Type UnderlyingTypeFound
        {
            get
            {
                if (!HasFoundUnderlyingType)
                {
                    throw new InvalidOperationException("No underlying type was found so cannot be returned. Hint: Use HasFoundUnderlyingType first.");
                }
                return _underlyingTypeFound;
            }
        }


        #endregion

        #region Private Members

        #endregion
    }
}
