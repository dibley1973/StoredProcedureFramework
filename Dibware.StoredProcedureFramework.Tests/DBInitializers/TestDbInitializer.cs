using System.Data.Entity;
using System.Linq;
using Dibware.StoredProcedureFramework.Tests.Context;
using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;

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
            context.Database.ExecuteSqlCommand(
                TransactionalBehavior.DoNotEnsureTransaction,
                string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                context.Database.Connection.Database));

            base.InitializeDatabase(context);
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
            context.Tenants.First().Companies.Add(new Company() { Active = true, Id = 1, Name = "Acme" });
            context.Tenants.First().Companies.Add(new Company() { Active = true, Id = 2, Name = "BetterCo" });
            context.Tenants.First().Companies.Add(new Company() { Active = true, Id = 3, Name = "CoastToCoast" });
            context.Tenants.Last().Companies.Add(new Company() { Active = true, Id = 4, Name = "Duplex" });
        }

        private static void AddTenants(IntegrationTestContext context)
        {
            context.Tenants.Add(new Tenant() { Active = true, Id = 1, Name = "Me" });
            context.Tenants.Add(new Tenant() { Active = true, Id = 2, Name = "You" });
        }
    }
}
