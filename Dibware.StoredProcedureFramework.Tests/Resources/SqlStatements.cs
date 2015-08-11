
namespace Dibware.StoredProcedureFramework.Tests.Resources
{
    internal static class SqlStatements
    {
        public const string DeleteAllFromTable = @"DELETE FROM {0};";
        public const string DeleteAllFromTableWithSchema = @"DELETE FROM {0}.{1};";
        public const string TruncateTable = @"TRUNCATE TABLE {0};";
        public const string TruncateTableWithSchema = @"TRUNCATE TABLE {0}.{1};";
    }
}