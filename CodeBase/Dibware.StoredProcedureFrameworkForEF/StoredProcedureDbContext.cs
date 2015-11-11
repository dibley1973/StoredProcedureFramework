using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Dibware.StoredProcedureFrameworkForEF.Extensions;

namespace Dibware.StoredProcedureFrameworkForEF
{
    /// <summary>
    /// An inheritable custom DbContext that in herits from <see cref="System.Data.Entity.DbContext" />
    /// but automatically calls the <see cref="DbContextExtensions.InitializeStoredProcedureProperties"/> 
    /// extension method to initialise the stored procedures in an inherited DbContext class.
    /// </summary>
    public class StoredProcedureDbContext : DbContext
    {
        /// <summary>
        /// Constructs a new <see cref="Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext" />
        /// instance using conventions to create the name of the database to
        /// which a connection will be made, and initializes it from the given model.
        /// The by-convention name is the full name (namespace + class name) of the derived context class.
        /// See the class remarks for how this is used to create a connection.
        /// </summary>
        /// <param name="model"> The model that will back this context. </param>
        protected StoredProcedureDbContext(DbCompiledModel model)
            : base(model)
        {
            this.InitializeStoredProcedureProperties();
        }

        /// <summary>
        /// Constructs a new <see cref="Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext" /> 
        /// instance using the given string as the name or connection string for 
        /// the database to which a connection will be made.  
        /// </summary>
        /// <param name="nameOrConnectionString"> Either the database name or a connection string. </param>
        public StoredProcedureDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.InitializeStoredProcedureProperties();
        }

        /// <summary>
        /// Constructs a new <see cref="Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext" /> 
        /// instance using the given string as the name or connection string for 
        /// the  database to which a connection will be made, and initializes it 
        /// from the given model.
        /// </summary>
        /// <param name="nameOrConnectionString"> Either the database name or a connection string. </param>
        /// <param name="model"> The model that will back this context. </param>
        public StoredProcedureDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
            this.InitializeStoredProcedureProperties();
        }

        /// <summary>
        /// Constructs a new <see cref="Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext" />
        /// instance using the existing connection to connect to a database.
        /// The connection will not be disposed when the context is disposed if <paramref name="contextOwnsConnection" />
        /// is <c>false</c>.
        /// </summary>
        /// <param name="existingConnection"> An existing connection to use for the new context. </param>
        /// <param name="contextOwnsConnection">
        /// If set to <c>true</c> the connection is disposed when the context is disposed, otherwise the caller must dispose the connection.
        /// </param>
        public StoredProcedureDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            this.InitializeStoredProcedureProperties();
        }

        /// <summary>
        /// Constructs a new <see cref="Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext" /> 
        /// instance using the existing connection to connect to a database,
        /// and initializes it from the given model. The connection will not be 
        /// disposed when the context is disposed if <paramref name="contextOwnsConnection" />
        /// is <c>false</c>.
        /// </summary>
        /// <param name="existingConnection"> An existing connection to use for the new context. </param>
        /// <param name="model"> The model that will back this context. </param>
        /// <param name="contextOwnsConnection">
        ///     If set to <c>true</c> the connection is disposed when the context is disposed, otherwise the caller must dispose the connection.
        /// </param>
        public StoredProcedureDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
            this.InitializeStoredProcedureProperties();
        }

        /// <summary>
        /// Constructs a new <see cref="Dibware.StoredProcedureFrameworkForEF.StoredProcedureDbContext" /> 
        /// instance around an existing ObjectContext.
        /// </summary>
        /// <param name="objectContext"> An existing ObjectContext to wrap with the new context. </param>
        /// <param name="dbContextOwnsObjectContext">
        ///     If set to <c>true</c> the ObjectContext is disposed when the DbContext is disposed, otherwise the caller must dispose the connection.
        /// </param>
        public StoredProcedureDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
            this.InitializeStoredProcedureProperties();
        }
    }
}