using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dibware.StoredProcedureFramework
{
    // Defines the return type of the results 
    public class ResultList : IEnumerable
    {
        // our internal object that is the list of results lists
        private List<object> _items; // = new List<object>();


        public ResultList(List<object> items)
        {
            _items = items;
        }

        /// <summary>
        /// Return an enumerator of the internal list
        /// </summary>
        /// <returns>Enumerator of the internal list</returns>
        public IEnumerator GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Return the count of result set
        /// </summary>
        public int Count
        {
            get { return _items.Count; }
        }

        /// <summary>
        /// Return the result set that contains a particular type and does a cast to that type.
        /// </summary>
        /// <typeparam name="T">Type that was listed in StoredProc object as a possible return type for the stored procedure</typeparam>
        /// <returns>List of T; if no results match, returns an empty list</returns>
        public List<T> ToList<T>()
        {
            if (_items.Count > 0)
            {
                if (typeof(T) == _items[0].GetType())
                {
                    // do cast to return type
                    return _items.Cast<T>().Select(p => p).ToList();
                }
            }

            // no matches? return empty list
            return new List<T>();
        }
    }
}