using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Generics
{
    /// <summary>
    /// A collection of zero or one elements of T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Maybe<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _values;

        public Maybe()
        {
            _values = new T[0];
        }

        public Maybe(T value)
        {
            if (value == null) throw new ArgumentNullException("value");

            _values = new[] { value };
        }

        /// <summary>
        /// If this instance has a value returns this; otherwise 
        /// returns the other specified.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public Maybe<T> Or(Maybe<T> other)
        {
            return HasItem ? this : other;
        }

        public bool HasItem
        {
            get { return this.Count() == 1; }
        }

        /// <summary>
        /// Converts the specified value or null in to a Maybe.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static Maybe<T> ToMaybe(T value)
        {
            return value == null
                ? new Maybe<T>()
                : new Maybe<T>(value);
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
