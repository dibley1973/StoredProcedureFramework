using System.Data.Entity;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Context
{
    internal class TestContext : DbContext
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestContext"/> class.
        /// </summary>
        protected TestContext() : base("TestContext") { }


        /// <summary>
        /// Initializes a new instance of the <see cref="TestContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        public TestContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // Set the chosen database initializer and initialize the database
            //IDatabaseInitializer<CommandContext> databaseInitializer = new CreateDatabaseIfNotExists<TestContext>();
            IDatabaseInitializer<TestContext> databaseInitializer = new DropCreateDatabaseIfModelChanges<TestContext>();
            //IDatabaseInitializer<CommandContext> databaseInitializer = new DropCreateDatabaseAlways<TestContext>();
            //IDatabaseInitializer<CommandContext> databaseInitializer = new SchoolDBInitializer();
            Database.SetInitializer(databaseInitializer);
        }


        #endregion

    }
}
