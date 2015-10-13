using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class DbContextTests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    public class TestContext : DbContext
    {
        //public MostBasicEFStoredProcedure MostBasicStoredProcedure { get; set; }

        //public TestContext()
        //{
        //    MostBasicStoredProcedure = new MostBasicEFStoredProcedure(this);
        //}
    }
}
