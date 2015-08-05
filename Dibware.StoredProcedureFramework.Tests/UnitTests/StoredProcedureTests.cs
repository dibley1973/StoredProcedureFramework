﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Dibware.StoredProcedureFramework.Exceptions;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests
{
    [TestClass]
    public class StoredProcedureTests
    {
        #region Constructor Tests

        [TestMethod]
        public void ConstructWithoutParameters_CheckSchemaNameProperty_ReturnsDefault()
        {
            // ARRANGE
            const string expectedSchemaName = StoredProcedure.DefaultSchema;
            var procedure = new StoredProcedure();

            // ACT
            var actualSchemaValue = procedure.SchemaName;

            // ASSERT
            Assert.AreEqual(expectedSchemaName, actualSchemaValue);
        }

        [TestMethod]
        public void ConstructWithProcedureNameParameter_CheckSchemaNameProperty_ReturnsDefault()
        {
            // ARRANGE
            const string expectedSchemaName = StoredProcedure.DefaultSchema;
            const string expectedProcedureName = "GetAllMonkeys";
            var procedure = new StoredProcedure(expectedProcedureName);

            // ACT
            var actualSchemaName = procedure.SchemaName;

            // ASSERT
            Assert.AreEqual(expectedSchemaName, actualSchemaName);
        }

        [TestMethod]
        public void ConstructWithProcedureNameParameter_CheckProcedureNameProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedProcedureName = "GetAllMonkeys";
            var procedure = new StoredProcedure(expectedProcedureName);

            // ACT
            var actualprocedureName = procedure.ProcedureName;

            // ASSERT
            Assert.AreEqual(expectedProcedureName, actualprocedureName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructWithemptyProcedureNameParameter_ThrowsException()
        {
            // ARRANGE

            // ACT
            var procedure = new StoredProcedure(string.Empty);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullProcedureNameParameter_ThrowsException()
        {
            // ARRANGE

            // ACT
            var procedure = new StoredProcedure(null);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        public void ConstructWithProcedureAndSchemaNameParameters_CheckSchemaNameProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedSchemaName = "application";
            const string expectedProcedureName = "GetAllMonkeys";
            var procedure = new StoredProcedure(expectedProcedureName, expectedSchemaName);

            // ACT
            var actualSchemaName = procedure.SchemaName;
            var actualprocedureName = procedure.ProcedureName;

            // ASSERT
            Assert.AreEqual(expectedSchemaName, actualSchemaName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructWithEmptyProcedureAndSchemaNameParameters_ThrowsArgumentOutOfRangeException()
        {
            // ARRANGE

            // ACT
            var procedure = new StoredProcedure(string.Empty, string.Empty);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructWithProcedureAndEmptySchemaNameParameters_ThrowsArgumentOutOfRangeException()
        {
            // ARRANGE
            const string expectedProcedureName = "GetAllMonkeys";

            // ACT
            var procedure = new StoredProcedure(expectedProcedureName, string.Empty);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNullProcedureAndSchemaNameParameters_ThrowsArgumentNullException()
        {
            // ARRANGE

            // ACT
            var procedure = new StoredProcedure(null, null);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithProcedureAndNullSchemaNameParameters_ThrowsArgumentNullException()
        {
            // ARRANGE
            const string expectedProcedureName = "GetAllMonkeys";

            // ACT
            var procedure = new StoredProcedure(expectedProcedureName, null);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        public void ConstructWithProcedureAndSchemaNameParameters_CheckProcedureNameProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedSchemaValue = "application";
            const string expectedProcedureName = "GetAllMonkeys";
            var procedure = new StoredProcedure(expectedProcedureName, expectedSchemaValue);

            // ACT
            var actualProcedureName = procedure.ProcedureName;

            // ASSERT
            Assert.AreEqual(expectedProcedureName, actualProcedureName);
        }

        #endregion

        #region EnsureFullyConstrucuted

        [TestMethod]
        [ExpectedException(typeof(StoredProcedureConstructionException))]
        public void EnsureFullyConstrucuted_WhenNotFullyConstructed_ThrowsException()
        {
            // ARRANGE
            var procedure = new StoredProcedure();

            // ACT
            procedure.EnsureFullyConstrucuted();

            // ASSERT
            Assert.Fail();
        }

        #endregion

        #region GetTwoPartName

        [TestMethod]
        public void GetTwoPartName_WhenConstructedWithProcedureName_ReturnsCorrectly()
        {
            // ARRANGE
            const string expectedSchemaName = StoredProcedure.DefaultSchema;
            const string expectedProcedureName = "GetAllMonkeys";
            string expectedTwoPartName = String.Concat(
                expectedSchemaName,
                StoredProcedure.DotIdentifier,
                expectedProcedureName);

            var procedure = new StoredProcedure(expectedProcedureName);

            // ACT
            var actualTwoPartName = procedure.GetTwoPartName();

            // ASSERT
            Assert.AreEqual(expectedTwoPartName, actualTwoPartName);
        }

        [TestMethod]
        [ExpectedException(typeof(StoredProcedureConstructionException))]
        public void GetTwoPartName_WhenConstructedWithoutProcedureName_ThrowsException()
        {
            // ARRANGE

            var procedure = new StoredProcedure();

            // ACT
            procedure.GetTwoPartName();

            // ASSERT
            Assert.Fail();
        }

        #endregion

        #region IsValid

        [TestMethod]
        public void IsFullyConstructed_WhenProcedureNameNotSet_ReturnsFalse()
        {
            // ARRANGE
            const bool expectedValue = false;
            var procedure = new StoredProcedure();

            // ACT
            var actualvalue = procedure.IsFullyConstructed();

            // ASSERT
            Assert.AreEqual(expectedValue, actualvalue);
        }

        [TestMethod]
        public void IsFullyConstructed_WhenContextNotSet_ReturnsFalse()
        {
            // ARRANGE
            const string schemaValue = "application";
            const string procedureName = "GetAllMonkeys";
            const bool expectedValue = false;
            var procedure = new StoredProcedure(procedureName, schemaValue);

            // ACT
            var actualvalue = procedure.IsFullyConstructed();

            // ASSERT
            Assert.AreEqual(expectedValue, actualvalue);
        }


        #endregion

        #region SetName

        [TestMethod]
        public void SetName_WithValidValue_CheckNameProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedValue = "TestName";
            var procedure = new StoredProcedure();

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
            var procedure = new StoredProcedure();

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
            var procedure = new StoredProcedure();

            // ACT
            procedure.SetProcedureName(null);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        #endregion

        #region SetSchema

        [TestMethod]
        public void SetSchema_WithValidValue_CheckSchemaProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedValue = "TestSchema";
            var procedure = new StoredProcedure();

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
            var procedure = new StoredProcedure();

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
            var procedure = new StoredProcedure();

            // ACT
            procedure.SetSchemaName(null);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        #endregion
    }
}
