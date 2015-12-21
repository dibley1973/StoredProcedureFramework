using System;
using System.Collections;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class IListTypeDefinitionFinder
    {
        #region Fields

        private readonly Type _listType;
        private Type _listGenericTypeFound;

        #endregion

        #region Public Members

        public IListTypeDefinitionFinder(IList list)
        {
            if (list == null) throw new ArgumentNullException("list");

            _listType = list.GetType();
            SetListTypeUnderlyingType();
        }

        //public Type ListType
        //{
        //    get { return _listType; }
        //}

        /// <summary>
        /// Gets the generic list type found.
        /// </summary>
        /// <value>
        /// The generic list type found.
        /// </value>
        public Type GenericListTypeFound
        {
            get { return _listGenericTypeFound; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has found an underlying a generic list type.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has found a generic list type; otherwise, <c>false</c>.
        /// </value>
        public bool HasFoundGenericListTypeFound
        {
            get { return _listGenericTypeFound != null; }
        }

        #endregion

        #region Private Methods

        private void SetListTypeUnderlyingType()
        {
            var listTypeUnderlyingTypeFinder = new ListTypeUnderlyingTypeFinder(_listType);
            listTypeUnderlyingTypeFinder.CheckForUnderlyingType();
            if (listTypeUnderlyingTypeFinder.HasFoundUnderlyingType)
            {
                _listGenericTypeFound = listTypeUnderlyingTypeFinder.UnderlyingTypeFound;
            }
        }

        #endregion
    }
}