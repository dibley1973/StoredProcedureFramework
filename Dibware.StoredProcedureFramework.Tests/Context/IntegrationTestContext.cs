using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

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

        #region DbContext Member Overrides

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove Conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        #endregion

        #region DBSets

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Company> Companies { get; set; }

        #endregion

    }
}
