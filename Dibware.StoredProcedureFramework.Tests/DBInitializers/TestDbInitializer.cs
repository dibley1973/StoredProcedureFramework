using Dibware.StoredProcedureFramework.Tests.Context;
using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using System.Data.Entity;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.DBInitializers
{
    internal class TestDbInitializer<T> : DropCreateDatabaseAlways<IntegrationTestContext>
    {
        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void InitializeDatabase(IntegrationTestContext context)
        {
            base.InitializeDatabase(context);

            // http://patrickdesjardins.com/blog/entity-framework-database-initialization
            context.Database.ExecuteSqlCommand(
                TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                context.Database.Connection.Database));
        }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(IntegrationTestContext context)
        {
            AddTenants(context);
            AddCompanies(context);
            base.Seed(context);
        }

        private static void AddCompanies(IntegrationTestContext context)
        {
            //context.Tenant.First().Company.Add(new Company() { IsActive = true, Id = 1, Name = "Acme" });
            //context.Tenant.First().Company.Add(new Company() { IsActive = true, Id = 2, Name = "BetterCo" });
            //context.Tenant.First().Company.Add(new Company() { IsActive = true, Id = 3, Name = "CoastToCoast" });
            //context.Tenant.Last().Company.Add(new Company() { IsActive = true, Id = 4, Name = "Duplex" });
        }

        private static void AddTenants(IntegrationTestContext context)
        {
            //context.Tenant.Add(new Tenant() { IsActive = true, Id = 1, Name = "Me" });
            //context.Tenant.Add(new Tenant() { IsActive = true, Id = 2, Name = "You" });
        }
    }
}
