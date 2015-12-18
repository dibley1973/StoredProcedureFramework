using System;
using System.Collections;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public class IListTypeDefinitionFinder
    {
        #region Fields

        private readonly Type _listType;
        private readonly Type _listGenericType;

        #endregion

        public IListTypeDefinitionFinder(IList list)
        {
            if (list == null) throw new ArgumentNullException("list");

            _listType = list.GetType();
            _listGenericType = _listType.GetListTypeUnderlyingType();
        }

        public Type ListType
        {
            get { return _listType; }
        }

        public Type GenericListType
        {
            get { return _listGenericType; }
        }

    }
}
