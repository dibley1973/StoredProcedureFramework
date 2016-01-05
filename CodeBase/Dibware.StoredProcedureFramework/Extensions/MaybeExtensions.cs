using System.Linq;
using Dibware.StoredProcedureFramework.Generics;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class MaybeExtensions
    {
        /// <summary>
        /// Returns the single result from this instance of <see cref="Maybe{T}"/>
        /// if one exists, or returns the specified default value if not.
        /// </summary>
        /// <typeparam name="T">The type of the Maybe</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="defaultValue">The default value to return.</param>
        /// <returns></returns>
        public static T SingleOrDefault<T>(this Maybe<T> instance, T defaultValue)
        {
            var list = instance.ToList();

            return list.Any() ? list.Single() : defaultValue;
        }
    }
}