
namespace Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes
{
    /// <summary>
    /// Represenst a parameter table type for the transaction test
    /// </summary>
    internal class TransactionTestParameterTableType
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }
}