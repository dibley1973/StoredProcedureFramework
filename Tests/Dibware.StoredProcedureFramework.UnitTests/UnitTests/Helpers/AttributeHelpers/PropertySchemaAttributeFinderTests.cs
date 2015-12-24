using System;
using System.Linq;
using System.Reflection;
using Dibware.StoredProcedureFramework.Generics;
using Dibware.StoredProcedureFramework.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers.AttributeHelpers
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
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure1");

            // ACT
            bool actual = new PropertySchemaAttributeFinder(property).HasFoundAttribute;

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure2");

            // ACT
            bool actual = new PropertySchemaAttributeFinder(property).HasFoundAttribute;

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region GetResult

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsEmptyMaybe()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure1");

            // ACT
            Maybe<SchemaAttribute> actual = new PropertySchemaAttributeFinder(property).GetResult();

            // ASSERT
            Assert.IsNull(actual.FirstOrDefault());
        }

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsMaybePopulatedWithInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Procedure2");

            // ACT
            Maybe<SchemaAttribute> actual = new PropertySchemaAttributeFinder(property).GetResult();

            // ASSERT
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual.FirstOrDefault(), typeof(SchemaAttribute));
            Assert.AreEqual("log", actual.Single().Value);
        }

        #endregion

        #region Mock object

        private class TestObject
        {
            
            public string Procedure1 { get; set; }
            [Schema("log")]
            public string Procedure2 { get; set; }
        }

        #endregion
    }
}