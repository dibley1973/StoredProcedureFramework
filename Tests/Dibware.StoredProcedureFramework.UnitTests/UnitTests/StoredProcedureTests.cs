using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using MostBasicStoredProcedure = Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures.MostBasicStoredProcedure;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests
{
    [TestClass]
    public class StoredProcedureTests
    {
        #region Constructor Tests

        // NOTE:
        //  This test is no longer valid as the EF style specific stored procedures
        //  require the ability to not set the parameters until execution time
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void ConstructWithNullProcedureParameters_ThrowsArgumentNullException()
        //{
        //    // ARRANGE

        //    // ACT
        //    new StoredProcedureWithParameters(null);

        //    // ASSERT
        //    Assert.Fail();
        //}


        [TestMethod]
        public void ConstructWithProcedureName_CheckSchemaNameProperty_ReturnsDefault()
        {
            // ARRANGE
            const string expectedSchemaName = StoredProcedureDefaults.DefaultSchemaName;
            var procedure = new MostBasicStoredProcedure();

            // ACT
            var actualSchemaValue = procedure.SchemaName;

            // ASSERT
            Assert.AreEqual(expectedSchemaName, actualSchemaValue);
        }

        [TestMethod]
        public void ConstructWithProcedureName_CheckProcedureNameProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedSchemaName = StoredProcedureDefaults.DefaultSchemaName;
            const string expectedProcedureName = "GetAllMonkeys";
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(expectedProcedureName, parameters);

            // ACT
            var actualSchemaName = procedure.SchemaName;

            // ASSERT
            Assert.AreEqual(expectedSchemaName, actualSchemaName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructWithEmptyProcedureName_ThrowsArgumentOutOfRangeException()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();

            // ACT
            new StoredProcedureWithParameters(string.Empty, parameters);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullProcedureName_ThrowsException()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();

            // ACT
            new StoredProcedureWithParameters(null, parameters);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        public void ConstructWithProcedureAndSchemaName_CheckSchemaNameProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedSchemaName = "application";
            const string expectedProcedureName = "GetAllMonkeys";
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(expectedSchemaName, expectedProcedureName, parameters);

            // ACT
            var actualSchemaName = procedure.SchemaName;

            // ASSERT
            Assert.AreEqual(expectedSchemaName, actualSchemaName);
        }

        [TestMethod]
        public void ConstructWithProcedureAndSchemaName_CheckProcedureNameProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedSchemaName = "application";
            const string expectedProcedureName = "GetAllMonkeys";
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(expectedSchemaName, expectedProcedureName, parameters);

            // ACT
            var actualProcedureName = procedure.ProcedureName;

            // ASSERT
            Assert.AreEqual(expectedProcedureName, actualProcedureName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructWithEmptyProcedureAndSchemaName_ThrowsArgumentOutOfRangeException()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();

            // ACT
            new StoredProcedureWithParameters(string.Empty, string.Empty, parameters);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructWithProcedureAndEmptySchemaName_ThrowsArgumentOutOfRangeException()
        {
            // ARRANGE
            const string expectedProcedureName = "GetAllMonkeys";
            var parameters = new StoredProcedureWithParameters.BasicParameters();

            // ACT
            new StoredProcedureWithParameters(string.Empty, expectedProcedureName, parameters);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullProcedureAndSchemaName_ThrowsArgumentNullException()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();

            // ACT
            new StoredProcedureWithParameters(null, null, parameters);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithProcedureAndNullSchemaName_ThrowsArgumentNullException()
        {
            // ARRANGE
            const string expectedProcedureName = "GetAllMonkeys";
            var parameters = new StoredProcedureWithParameters.BasicParameters();

            // ACT
            new StoredProcedureWithParameters(null, expectedProcedureName, parameters);

            // ASSERT
            Assert.Fail();
        }

        #endregion

        #region EnsureIsFullyConstructed

        [TestMethod]
        //[ExpectedException(typeof(StoredProcedureConstructionException))]
        public void EnsureFullyConstructed_WhenConstructedWithParamerters_DoesNotThrowException()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(parameters);

            // ACT
            procedure.EnsureIsFullyConstructed();

            // ASSERT

        }

        [TestMethod]
        [Ignore] // Until we can get back to a way to fake no procedure name
        [ExpectedException(typeof(StoredProcedureConstructionException))]
        public void EnsureFullyConstructed_WhenNotFullyConstructed_ThrowsException()
        {
            // ARRANGE
            var procedure = new NotFullyConstructedStoredProcedure();

            // ACT
            procedure.EnsureIsFullyConstructed();

            // ASSERT
        }

        #endregion

        #region GetTwoPartName

        [TestMethod]
        [Ignore] // Until we can get back to a way to fake no procedure name
        [ExpectedException(typeof(StoredProcedureConstructionException))]
        public void GetTwoPartNamed_WhenNotFullyConstructed_ThrowsException()
        {
            // ARRANGE
            var procedure = new NotFullyConstructedStoredProcedure();

            // ACT
            procedure.GetTwoPartName();

            // ASSERT
        }

        [TestMethod]
        public void GetTwoPartName_WhenConstructedWithoutProcedureName_ReturnsCorrectly()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(parameters);
            const string expectedSchemaName = StoredProcedureDefaults.DefaultSchemaName;
            string expectedProcedureName = typeof(StoredProcedureWithParameters).Name;
            string expectedTwoPartName = String.Concat(
                expectedSchemaName,
                StoredProcedureDefaults.DotIdentifier,
                expectedProcedureName);

            // ACT
            var actualTwoPartName = procedure.GetTwoPartName();

            // ASSERT
            Assert.AreEqual(expectedTwoPartName, actualTwoPartName);
        }

        [TestMethod]
        public void GetTwoPartName_WhenConstructedWithProcedureName_ReturnsCorrectly()
        {
            // ARRANGE
            const string expectedSchemaName = StoredProcedureDefaults.DefaultSchemaName;
            const string expectedProcedureName = "GetAllMonkeys";
            string expectedTwoPartName = String.Concat(
                expectedSchemaName,
                StoredProcedureDefaults.DotIdentifier,
                expectedProcedureName);
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(expectedProcedureName, parameters);

            // ACT
            var actualTwoPartName = procedure.GetTwoPartName();

            // ASSERT
            Assert.AreEqual(expectedTwoPartName, actualTwoPartName);
        }

        #endregion

        #region HasNullStoredProcedureParameters

        [TestMethod]
        public void HasNullStoredProcedureParameters_WhenProcedureHasNullStoredProcedureParameters_ReturnsTrue()
        {
            // ARRANGE
            var procedure = new MostBasicStoredProcedure();

            // ACT
            var actualresult = procedure.HasNullStoredProcedureParameters;

            // ASSERT
            Assert.IsTrue(actualresult);
        }

        [TestMethod]
        public void HasNullStoredProcedureParameters_WhenProceduredDoesNotHaveNullStoredProcedureParameters_ReturnsFalse()
        {
            // ARRANGE
            var parameters = new NormalStoredProcedureParameters { Id = 1 };
            var procedure = new NormalStoredProcedure(parameters);

            // ACT
            var actualresult = procedure.HasNullStoredProcedureParameters;

            // ASSERT
            Assert.IsFalse(actualresult);
        }

        #endregion

        #region IsFullyConstructed

        [TestMethod]
        public void IsFullyConstructed_WhenNameSet_ReturnsTrue()
        {
            // ARRANGE
            const string procedureName = "GetAllMonkeys";
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(procedureName, parameters);

            // ACT
            var actualvalue = procedure.IsFullyConstructed();

            // ASSERT
            Assert.IsTrue(actualvalue);
        }

        [TestMethod]
        public void IsFullyConstructed_WhenProcedureNameNotSet_ReturnsTrue()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(parameters);

            // ACT
            var actualvalue = procedure.IsFullyConstructed();

            // ASSERT
            Assert.IsTrue(actualvalue);
        }

        #endregion

        #region IsFullyConstructed Tests

        [TestMethod]
        public void StoredProcedureWithAttributes_IsFullyConstructed()
        {
            // ARRANGE
            const bool expectedValue = true;
            var parameters = new NullStoredProcedureParameters();
            var procedure = new TenantGetAllWithAttributes(parameters);

            // ACT
            var actualValue = procedure.IsFullyConstructed();

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void StoredProcedureConstructuedWithName_IsFullyConstructed()
        {
            // ARRANGE
            const bool expectedValue = true;
            const string expectedProcedureName = "GetAll";
            const string expectedSchemaName = "dbo";
            var procedure = new TenantGetAllNoAttributes(expectedProcedureName);

            // ACT
            var actualValue = procedure.IsFullyConstructed();

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
            Assert.AreEqual(expectedProcedureName, procedure.ProcedureName);
            Assert.AreEqual(expectedSchemaName, procedure.SchemaName);
        }

        [TestMethod]
        public void StoredProcedureConstructuedWithNameAndSchema_IsFullyConstructed()
        {
            // ARRANGE
            const bool expectedValue = true;
            const string expectedProcedureName = "GetAll";
            const string expectedSchemaName = "app";
            var procedure = new TenantGetAllNoAttributes(expectedSchemaName,
                expectedProcedureName);

            // ACT
            var actualValue = procedure.IsFullyConstructed();

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
            Assert.AreEqual(expectedProcedureName, procedure.ProcedureName);
            Assert.AreEqual(expectedSchemaName, procedure.SchemaName);
        }

        [TestMethod]
        public void StoredProcedureWithoutAttributesAndNameNotSet_IsFullyConstructed()
        {
            // ARRANGE
            var procedure = new TenantGetAllNoAttributes();

            // ACT
            var actualValue = procedure.IsFullyConstructed();

            // ASSERT
            Assert.IsTrue(actualValue);
        }

        #endregion

        #region ParametersType

        [TestMethod]
        public void ParametersType_WhenProcedureConstructed_ReturnsCorrectType()
        {
            // ARRANGE
            var expectedReturnType = typeof(NullStoredProcedureParameters);
            var procedure = new DecimalTestStoredProcedure();

            // ACT
            var actualReturnType = procedure.ParametersType;

            // ASSERT
            Assert.AreEqual(actualReturnType, expectedReturnType);
        }

        #endregion

        #region ReturnType

        [TestMethod]
        public void ReturnType_WhenProcedureConstructed_ReturnsCorrectType()
        {
            // ARRANGE
            var expectedReturnType = typeof(List<DecimalTestStoredProcedure.DecimalTestExtendedReturnType>);
            var procedure = new DecimalTestStoredProcedure();

            // ACT
            Type actualReturnType = procedure.ReturnType;

            // ASSERT
            Assert.AreEqual(actualReturnType, expectedReturnType);
        }

        #endregion

        #region SetName

        [TestMethod]
        public void SetName_WithValidValue_CheckNameProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedValue = "TestName";
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(parameters);

            // ACT
            procedure.SetProcedureName(expectedValue);
            var actualValue = procedure.ProcedureName;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetName_WithInvalidValue_ThrowsArgumentOutOfRangeException()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(parameters);

            // ACT
            procedure.SetProcedureName(string.Empty);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetName_WithNullValue_ThrowsArgumentNullException()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(parameters);

            // ACT
            procedure.SetProcedureName(null);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        #endregion

        #region SetParametersType Tests

        //[TestMethod]
        //public void SetParametersType_NotCalled_ResultsInEmptyParametersCollection()
        //{
        //    // ARRANGE
        //    var procedure = new MostBasicStoredProcedure();

        //    // ACT
        //    ICollection<SqlParameter> parameters = procedure.Parameters;

        //    // ASSERT
        //    Assert.IsNull(parameters);
        //}

        //[TestMethod]
        //public void SetParametersType_CalledWith_ResultsInPopulatedParametersProperty()
        //{
        //    // ARRANGE
        //    var procedure = new MostBasicStoredProcedure();
        //    const int expectedParameterCount = 2;
        //    var parameters = new FakeParameters.ParametersWithoutNameMapping();

        //    // ACT
        //    procedure.SetParametersType(parameters.GetType());
        //    ICollection<SqlParameter> parameterList = procedure.Parameters;

        //    // ASSERT
        //    Assert.AreEqual(expectedParameterCount, parameterList.Count);
        //}

        #endregion

        #region SetSchema

        [TestMethod]
        public void SetSchema_WithValidValue_CheckSchemaProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedValue = "TestSchema";
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(parameters);

            // ACT
            procedure.SetSchemaName(expectedValue);
            var actualValue = procedure.SchemaName;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetSchema_WithInvalidValue_ThrowsArgumentOutOfRangeException()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(parameters);

            // ACT
            procedure.SetSchemaName(string.Empty);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetSchema_WithNullValue_ThrowsArgumentNullException()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParameters.BasicParameters();
            var procedure = new StoredProcedureWithParameters(parameters);

            // ACT
            procedure.SetSchemaName(null);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        #endregion
    }
}
