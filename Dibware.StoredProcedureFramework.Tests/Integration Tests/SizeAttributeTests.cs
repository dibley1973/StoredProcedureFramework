using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.SizeAttributeProcedures;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.VarCharTestProcedures;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class SizeAttributeTests : BaseIntegrationTest
    {
        [TestMethod]
        [ExpectedException(typeof(SqlParameterOutOfRangeException))]
        public void CallVarcharProcedureWithIncorrectParameterSize_ThrowsSqlParameterOutOfRangeException()
        {
            // ARRANGE
            const string initialvalue = @"12345678901234567890";
            var parameters = new VarCharTestProcedureParameters
            {
                Parameter1 = initialvalue
            };
            var procedure = new VarCharTestProcedureStoredProcedure(parameters);

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }


        [TestMethod]
        public void CallProcedureWithSameSizeAttributeAsData_ResultsInNoLossOfData()
        {
            // ARRANGE
            const string initialvalue = @"12345678901234567890";
            var parameters = new CorrectSizeAttributeParameters
            {
                Value1 = initialvalue
            };

            // ACT


            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        public void CallProcedureWithDataSmallerThanSizeAttribute_ResultsInNoLossOfData()
        {
            // ARRANGE
            const string initialvalue = @"1234567890";
            var parameters = new CorrectSizeAttributeParameters
            {
                Value1 = initialvalue
            };

            // ACT

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        public void CallProcedureWithDataLargerThanSizeAttribute_ThrowsException()
        {
            // ARRANGE
            const string initialvalue = @"123456789012345678901234567890";
            var parameters = new CorrectSizeAttributeParameters
            {
                Value1 = initialvalue
            };

            // ACT

            // ASSERT
            Assert.Fail();
        }



        [TestMethod]
        public void CallProcedureWithSizeAttributeSmallerThanProcedure_ThrowsException()
        {
            // ARRANGE
            const string initialvalue = @"1234567890";
            var parameters = new TooSmallSizeAttributeParameters
            {
                Value1 = initialvalue
            };

            // ACT

            // ASSERT
            Assert.Fail();
        }


        [TestMethod]
        public void CallProcedureWithSizeAttributeLargerThanProcedure_ThrowsException()
        {
            // ARRANGE
            const string initialvalue = @"123456789012345678901234567890";
            var parameters = new TooLargeSizeAttributeParameters
            {
                Value1 = initialvalue
            };

            // ACT

            // ASSERT
            Assert.Fail();
        }
    }
}