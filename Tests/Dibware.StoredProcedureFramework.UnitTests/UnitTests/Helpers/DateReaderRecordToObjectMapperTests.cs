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

        #region PopulateMappedTargetFromReaderRecord

        [TestMethod]
        //[Ignore] // Need to write some meaningful tests for this class!
        public void PopulateMappedTargetFromReaderRecord_WhenWhat_DoesWhat()
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

        [TestMethod]
        [ExpectedException(typeof(MissingMethodException))]
        public void PopulateMappedTargetFromReaderRecord_WithObjectThatHasNodefaultConstructor_ThrowsException()
        {
            // ARRANGE
            var dataReaderMock = new Mock<IDataReader>();
            IDataReader expectedReader = dataReaderMock.Object;
            Type expectedType = typeof(TestObjectWithoutConstructor);

            // ACT
            new DateReaderRecordToObjectMapper(expectedReader, expectedType)
                .PopulateMappedTargetFromReaderRecord();

            // ASSERT
        }

        #endregion

        private class TestObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class TestObjectWithoutConstructor
        {
            public TestObjectWithoutConstructor(int id)
            {
                Id = id;
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
