using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using System.Data.Entity;

namespace Dibware.StoredProcedureFramework.Tests.Context
{
    internal class IntegrationTestContext : DbContext
    {
        #region Constructors

        static IntegrationTestContext()
        {
            //// Set the chosen database initializer and initialize the database
            ////IDatabaseInitializer<IntegrationTestContext> databaseInitializer = new CreateDatabaseIfNotExists<IntegrationTestContext>();
            ////IDatabaseInitializer<IntegrationTestContext> databaseInitializer = new DropCreateDatabaseIfModelChanges<IntegrationTestContext>();
            ////IDatabaseInitializer<IntegrationTestContext> databaseInitializer = new DropCreateDatabaseAlways<IntegrationTestContext>();
            //IDatabaseInitializer<IntegrationTestContext> databaseInitializer = new TestDbInitializer<IntegrationTestContext>();
            //Database.SetInitializer(databaseInitializer);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestContext"/> class.
        /// </summary>
        protected IntegrationTestContext() : base("IntegrationTestContext") { }


        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        public IntegrationTestContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // Set the chosen database initializer and initialize the database
            IDatabaseInitializer<IntegrationTestContext> databaseInitializer = new CreateDatabaseIfNotExists<IntegrationTestContext>();
            //IDatabaseInitializer<IntegrationTestContext> databaseInitializer = new DropCreateDatabaseIfModelChanges<IntegrationTestContext>();
            //IDatabaseInitializer<IntegrationTestContext> databaseInitializer = new DropCreateDatabaseAlways<IntegrationTestContext>();
            //IDatabaseInitializer<IntegrationTestContext> databaseInitializer = new TestDbInitializer<IntegrationTestContext>();
            Database.SetInitializer(databaseInitializer);

        }


        #endregion

        #region DBSets

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Company> Companies { get; set; }

        #endregion

    }
}
