
//namespace Dibware.StoredProcedureFramework.Generics
//{
//    public interface IMaybe<T> { }

//    public class Nothing<T> : IMaybe<T>
//    {
//        /// <summary>
//        /// Returns a <see cref="System.String" /> that represents this instance.
//        /// </summary>
//        /// <returns>
//        /// A <see cref="System.String" /> that represents this instance.
//        /// </returns>
//        public override string ToString()
//        {
//            return "Nothing";
//        }
//    }

//    public class Just<T> : IMaybe<T>
//    {
//        public T Value { get; private set; }

//        public Just(T value)
//        {
//            Value = value;
//        }

//        /// <summary>
//        /// Returns a <see cref="System.String" /> that represents this instance.
//        /// </summary>
//        /// <returns>
//        /// A <see cref="System.String" /> that represents this instance.
//        /// </returns>
//        public override string ToString()
//        {
//            return Value.ToString();
//        }
//    }

//    public static class MaybeExtensions
//    {
//        /// <summary>
//        /// Converts the specified instance or null in to a Maybe.
//        /// </summary>
//        /// <param name="instance">The instance.</param>
//        /// <returns></returns>
//        public static IMaybe<T> ToMaybe<T>(this T instance)
//        {
//            if (instance == null) return new Nothing<T>();

//            return new Just<T>(instance);
//        }

//        /// <summary>
//        /// If this instance has a instance returns this; otherwise 
//        /// returns the other specified.
//        /// </summary>
//        /// <param name="instance">This instance</param>
//        /// <param name="other">The other instance.</param>
//        /// <returns></returns>
//        public static IMaybe<T> Or<T>(this IMaybe<T> instance, IMaybe<T> other)
//        {
//            return instance is Nothing<T>
//                ? other
//                : instance;
//        }
//    }
//}