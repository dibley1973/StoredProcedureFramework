
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.Fakes.Entities
{
    internal class Tenant
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Name { get; set; }

        public List<Company> Companies { get; set; }
    }
}