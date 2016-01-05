using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Helpers.Base;
using Dibware.StoredProcedureFramework.Helpers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers.Base
{
    [TestClass]
    public class SqlProgrammabilityObjectExecuterBaseTests
    {
        #region Tests

        #region Constructors

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructed_WithNullConnection_ThrowsException()
        {
            // ARRANGE

            // ACT
            new TestSqlProgrammabilityObjectExecuterBase(null, "TeststoredProc");
            
            // ASSERT
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructed_WithNullProgrammabilityObjectName_ThrowsException()
        {
            // ARRANGE
            Mock<IDbConnection> mockConnection = new Mock<IDbConnection>();
            
            // ACT
            new TestSqlProgrammabilityObjectExecuterBase(mockConnection.Object, null);

            // ASSERT
        }
        
        #endregion

        #region Execute

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Execute_WhenCalledAfterDisposing_ThrowsException()
        {
            // ARRANGE
            Mock<IDbConnection> mockConnection = new Mock<IDbConnection>();
            const string programmabilityObjectName = "TeststoredProc";
            var sqlProgrammabilityObjectExecuterBase =
                new TestSqlProgrammabilityObjectExecuterBase(mockConnection.Object, programmabilityObjectName);
            sqlProgrammabilityObjectExecuterBase.Dispose();

            // ACT
            sqlProgrammabilityObjectExecuterBase.Execute();

            // ASSERT
        }

        #endregion

        #region WithParameters

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithParameters_WhenCalledwithNullParameters_ThrowsException()
        {
            // ARRANGE
            Mock<IDbConnection> mockConnection = new Mock<IDbConnection>();
            const string programmabilityObjectName = "TeststoredProc";
            var sqlProgrammabilityObjectExecuterBase =
                new TestSqlProgrammabilityObjectExecuterBase(mockConnection.Object, programmabilityObjectName);
            
            // ACT
            sqlProgrammabilityObjectExecuterBase.WithParameters(null);

            // ASSERT
        }


        #endregion

        #endregion

        #region test Objects

        private class TestResultType
        {
            public int Id { get; set; }

        }

        private class TestSqlProgrammabilityObjectExecuterBase : SqlProgrammabilityObjectExecuterBase<List<TestResultType>>
        {
            public TestSqlProgrammabilityObjectExecuterBase(IDbConnection connection, string programmabilityObjectName)
                : base(connection, programmabilityObjectName)
            {
            }

            protected override IDbCommandCreator CreateCommandCreator()
            {
                throw new NotImplementedException();
            }

            protected override void ExecuteCommand()
            {
                throw new NotImplementedException();
            }

            internal new void WithParameters(IEnumerable<SqlParameter> procedureParameters)
            {
                base.WithParameters(procedureParameters);
            }
        }

        #endregion
    }
}
