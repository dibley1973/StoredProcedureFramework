
namespace Dibware.StoredProcedureFramework.Tests.Resources
{
    internal static class SqlStatements
    {
        public const string DeleteAllFromTable = @"DELETE FROM {0};";
        public const string TruncateTable = @"TRUNCATE TABLE {0};";
    }
}