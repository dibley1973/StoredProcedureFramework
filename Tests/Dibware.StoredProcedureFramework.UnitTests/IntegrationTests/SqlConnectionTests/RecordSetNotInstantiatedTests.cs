using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class RecordSetNotInstantiatedTests : BaseIntegrationTest
    {
        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void CallStoredProcedure_WithRecordSetNotInstantiatedInResultSet_ThrowsNullReferenceException()
        //{
        //    // ARRANGE
        //    var parameters = new NullStoredProcedureParameters();
        //    var procedure = new NotInstantiatedRecordSetStoredProcedure(parameters);

        //    // ACT
        //    Connection.Open();
        //    Connection.ExecuteStoredProcedure(procedure);
        //    Connection.Close();

        //    // ASSERT
        //}
    }
}
