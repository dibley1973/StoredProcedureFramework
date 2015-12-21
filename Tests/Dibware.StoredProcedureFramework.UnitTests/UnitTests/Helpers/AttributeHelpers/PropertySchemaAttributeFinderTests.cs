using System;
using System.Reflection;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class PropertySchemaAttributeFinderTests
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            new PropertySchemaAttributeFinder(null);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure1");

            // ACT
            new PropertySchemaAttributeFinder(property);

            // ASSERT
        }

        #endregion

        #region HasFoundAttribute

        [TestMethod]
        public void HasAttribute_WhenCalledAfterCheckAttributeAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure1");

            // ACT
            bool actual = new PropertySchemaAttributeFinder(property)
                .CheckForAttribute()
                .HasFoundAttribute;

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasAttribute_WhenCalledAfterCheckAttributeAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure2");

            // ACT
            bool actual = new PropertySchemaAttributeFinder(property)
                .CheckForAttribute()
                .HasFoundAttribute;

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region AttributeFound

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Attribute_WhenCalledAfterCheckAttributeAndPropertyDoesNotHaveAtrribute_ThrowsException()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure1");

            // ACT
            SchemaAttribute actual = new PropertySchemaAttributeFinder(property)
                .CheckForAttribute()
                .AttributeFound;

            // ASSERT
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Attribute_WhenCalledAfterCheckAttributeAndPropertyDoesHaveAtrribute_ReturnsInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure2");

            // ACT
            SchemaAttribute actual = new PropertySchemaAttributeFinder(property)
                .CheckForAttribute()
                .AttributeFound;

            // ASSERT
            Assert.IsNotNull(actual);
        }

        #endregion

        #region Mock object

        public class TestObject
        {
            
            public string Procedure1 { get; set; }
            [Schema("app")]
            public string Procedure2 { get; set; }
        }

        #endregion
    }
}