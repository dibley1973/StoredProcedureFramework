using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class DateReaderRecordToObjectMapperTests
    {
        #region Constructors

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_WhenConstructedWithNullDataReader_ThrowsExecption()
        {
            // ARRANGE
            Type expectedType = typeof(TestObject);

            // ACT
            new DateReaderRecordToObjectMapper(null, expectedType);

            // ASSERT
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_WhenConstructedWithNullTargetType_ThrowsExecption()
        {
            // ARRANGE
            var dataReaderMock = new Mock<IDataReader>();
            IDataReader expectedReader = dataReaderMock.Object;

            // ACT
            new DateReaderRecordToObjectMapper(expectedReader, null);

            // ASSERT
        }

        #endregion

        #region Build

        [TestMethod]
        [Ignore] // Need to write some meaningful tests for this class!
        public void Build_WhenWhat_DoesWhat()
        {
            // ARRANGE
            var dataReaderMock = new Mock<IDataReader>();
            IDataReader expectedReader = dataReaderMock.Object;
            Type expectedType = typeof(TestObject);

            // ACT
            new DateReaderRecordToObjectMapper(expectedReader, expectedType)
                .PopulateMappedTargetFromReaderRecord();

            // ASSERT
        }

        #endregion

        internal class TestObject
        {
            public int id { get; set; }
            public string Name { get; set; }
        }
    }
}
