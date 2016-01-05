using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.AssertExtensions
{
    internal static class AssertExtension
    {
        /// <summary>
        /// Verifies the two specified Byte arrays are equal
        /// for the purpose of unit tests. The assertion fails
        /// if teh condition is false.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="a2">The a2.</param>
        public static void ByteArrayEqual(byte[] a1, byte[] a2)
        {
            bool isEqual = ByteArrayCompare(a1, a2);
            Assert.IsTrue(isEqual);
        }

        /// <summary>
        /// Compares the Bytes in the specified arrays.
        /// </summary>
        /// <param name="a1">The a1.</param>
        /// <param name="a2">The a2.</param>
        /// <returns></returns>
        private static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(a1, a2);
        }
    }
}