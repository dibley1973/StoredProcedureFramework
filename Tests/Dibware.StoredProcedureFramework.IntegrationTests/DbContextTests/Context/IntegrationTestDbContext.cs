using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFrameworkForEF;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests.Context
{
    internal class IntegrationTestDbContext : StoredProcedureDbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestDbContext"/> class.
        /// </summary>
        protected IntegrationTestDbContext() : base("IntegrationTestContext") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        public IntegrationTestDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // Set the chosen database initializer and initialize the database
            IDatabaseInitializer<IntegrationTestDbContext> databaseInitializer =
                new CreateDatabaseIfNotExists<IntegrationTestDbContext>();
            Database.SetInitializer(databaseInitializer);

            // We do not need to explicitly instantiate all of the Stored 
            // procedures properties using "this.InitializeStoredProcedureProperties();"
            // as this os carried out for us by the "Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext"
            // class constructors
        }

        /// <summary>
        /// Constructs a new <see cref="IntegrationTestDbContext" /> instance using the existing connection to connect to a database.
        /// The connection will not be disposed when the context is disposed if <paramref name="contextOwnsConnection" />
        /// is <c>false</c>.
        /// </summary>
        /// <param name="existingConnection"> An existing connection to use for the new context. </param>
        /// <param name="contextOwnsConnection">
        /// If set to <c>true</c> the connection is disposed when the context is disposed, otherwise the caller must dispose the connection.
        /// </param>
        public IntegrationTestDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            // Set the chosen database initializer and initialize the database
            IDatabaseInitializer<IntegrationTestDbContext> databaseInitializer =
                new CreateDatabaseIfNotExists<IntegrationTestDbContext>();
            Database.SetInitializer(databaseInitializer);

            // We do not need to explicitly instantiate all of the Stored 
            // procedures properties using "this.InitializeStoredProcedureProperties();"
            // as this is carried out for us by the "Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext"
            // class constructors
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

        //public DbSet<Tenant> Tenants { get; set; }
        //public DbSet<Company> Companies { get; set; }

        #endregion

        #region Stored Procedures

        public MostBasicStoredProcedureForEf MostBasicStoredProcedure { get; private set; }
        public NormalStoredProcedureForEf NormalStoredProcedure { get; private set; }
        public AnonParamNormalStoredProcedureForEf AnonymousParameterStoredProcedure { get; private set; }
        [Name("NormalStoredProcedure")]
        internal NormalStoredProcedureForEf InternalMostBasicStoredProcedure { get; private set; }


        #endregion
    }
}
