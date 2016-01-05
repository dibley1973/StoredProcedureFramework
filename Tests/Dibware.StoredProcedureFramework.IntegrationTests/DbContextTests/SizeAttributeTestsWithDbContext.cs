using System.Linq;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class SizeAttributeTestsWithDbContext : BaseDbContextIntegrationTest
    {
        [TestMethod]
        public void CallProcedureWithSameSizeAttributeAsData_ResultsInNoLossOfData()
        {
            // ARRANGE
            const string initialValue = @"12345678901234567890";
            var parameters = new CorrectSizeAttributeStoredProcedure.Parameter
            {
                Value1 = initialValue
            };
            var procedure = new CorrectSizeAttributeStoredProcedure(parameters);

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            var result = resultSet.First();

            // ASSERT
            Assert.AreEqual(initialValue, result.Value1);
        }

        [TestMethod]
        public void CallProcedureWithDataSmallerThanSizeAttribute_ResultsInNoLossOfData()
        {
            // ARRANGE
            const string initialValue = @"1234567890";
            var parameters = new CorrectSizeAttributeStoredProcedure.Parameter
            {
                Value1 = initialValue
            };
            var procedure = new CorrectSizeAttributeStoredProcedure(parameters);

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            var result = resultSet.First();

            // ASSERT
            Assert.AreEqual(initialValue, result.Value1);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterOutOfRangeException))]
        public void CallProcedureWithDataLargerThanSizeAttribute_ThrowsException()
        {
            // ARRANGE
            const string initialValue = @"123456789012345678901234567890";
            var parameters = new CorrectSizeAttributeStoredProcedure.Parameter
            {
                Value1 = initialValue
            };
            var procedure = new CorrectSizeAttributeStoredProcedure(parameters);

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            // Exception should be thrown by now
        }

        [TestMethod]
        public void CallProcedureWithSizeAttributeSmallerThanProcedure_ReturnsCorrectData()
        {
            // ARRANGE
            const string initialValue = @"1234567890";
            var parameters = new TooSmallSizeAttributeStoredProcedure.Parameter
            {
                Value1 = initialValue
            };
            var procedure = new TooSmallSizeAttributeStoredProcedure(parameters);

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            //var results = resultSet.RecordSet1;
            var result = resultSet.First();

            // ASSERT
            Assert.AreEqual(initialValue, result.Value1);
        }

        // DEV NOTE: To be able to write code for this test to pass we would need to 
        // call SqlCommand.DeriveParameters. This can have quite a performance hit 
        // without some level of parameter caching.
        [TestMethod]
        [Ignore]
        [ExpectedException(typeof(SqlParameterOutOfRangeException))]
        public void CallProcedureWithSizeAttributeLargerThanProcedure_ThrowsException()
        {
            // ARRANGE
            const string initialValue = @"123456789012345678901234567890";
            var parameters = new TooLargeSizeAttributeStoredProcedure.Parameter
            {
                Value1 = initialValue
            };
            var procedure = new TooLargeSizeAttributeStoredProcedure(parameters);

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            // Exception should have been thrown by here!
        }

        [TestMethod]
        public void CallProcedureWithParameterValueAndSizeAttributeLongerThanProcedureParameterLength_TruncatesValueOncedPassedToStoredProcedure()
        {
            // ARRANGE
            const int expectedTruncatedLength = 20;
            const string initialValue = @"123456789012345678901234567890";
            var parameters = new TooLargeSizeAttributeStoredProcedure.Parameter
            {
                Value1 = initialValue
            };
            var procedure = new TooLargeSizeAttributeStoredProcedure(parameters);

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            var result = resultSet.First();

            // ASSERT
            Assert.AreEqual(expectedTruncatedLength, result.Value1.Length);
        }

        // TODO: Investigate if we can set VARCHAR size from the value of the parameters,
        // and where is best to perform this... Refer to Issue #1
        [TestMethod]
        //[Ignore]
        public void CallProcedureWithParameterValueLongerThanProcedureParameterLengthAndNoSizeAttributeSet_TruncatesValueOncedPassedToStoredProcedure()
        {
            // ARRANGE
            const int expectedTruncatedLength = 20;
            const string initialValue = @"123456789012345678901234567890";
            var parameters = new TooLargeValueButNoSizeAttributeStoredProcedure.Parameter
            {
                Value1 = initialValue
            };
            var procedure = new TooLargeValueButNoSizeAttributeStoredProcedure(parameters);

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            var result = resultSet.First();

            // ASSERT
            Assert.AreEqual(expectedTruncatedLength, result.Value1.Length);
        }
    }
}