using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.EfSpecific;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFrameworkForEF;
using Dibware.StoredProcedureFrameworkForEF.Generic;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Context
{
    internal class ApplicationDbContext : StoredProcedureDbContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        protected ApplicationDbContext() : base("ApplicationDbContext") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        public ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // Set the chosen database initializer and initialize the database
            IDatabaseInitializer<ApplicationDbContext> databaseInitializer =
                new CreateDatabaseIfNotExists<ApplicationDbContext>();
            Database.SetInitializer(databaseInitializer);

            // We do not need to explicitly instantiate all of the Stored 
            // procedures properties using "this.InitializeStoredProcedureProperties();"
            // as this is carried out for us by the "Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext"
            // class constructors
        }

        /// <summary>
        /// Constructs a new <see cref="ApplicationDbContext" /> instance using the existing connection to connect to a database.
        /// The connection will not be disposed when the context is disposed if <paramref name="contextOwnsConnection" />
        /// is <c>false</c>.
        /// </summary>
        /// <param name="existingConnection"> An existing connection to use for the new context. </param>
        /// <param name="contextOwnsConnection">
        /// If set to <c>true</c> the connection is disposed when the context is disposed, otherwise the caller must dispose the connection.
        /// </param>
        public ApplicationDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            // Set the chosen database initializer and initialize the database
            IDatabaseInitializer<ApplicationDbContext> databaseInitializer =
                new CreateDatabaseIfNotExists<ApplicationDbContext>();
            Database.SetInitializer(databaseInitializer);

            // We do not need to explicitly instantiate all of the Stored 
            // procedures properties using "this.InitializeStoredProcedureProperties();"
            // as this is carried out for us by the "Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext"
            // class constructors
        }

        #endregion

        #region Stored Procedures (SIMPLIFIED)

        [Schema("app")]
        internal AccountGetAllForCompanyId AccountGetAllForCompanyId { get; set; }

        #endregion

        #region Stored Procedures (SIMPLIFIED INLINE)

        [Schema("app")]
        internal StoredProcedure<List<TenantDto>> TenantGetAll { get; set; }

        [Schema("app")]
        internal StoredProcedure<List<TenantDto>> TenantGetForId { get; set; }

        [Schema("app")]
        internal StoredProcedure TenantDeleteForId { get; set; }

        [Schema("app")]
        internal StoredProcedure TenantMarkAllInactive { get; set; }

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
    }
}
