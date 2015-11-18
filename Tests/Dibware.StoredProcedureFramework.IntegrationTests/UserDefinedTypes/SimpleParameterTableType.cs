
namespace Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes
{
    /// <summary>
    /// Represenst a simple table type
    /// </summary>
    internal class SimpleParameterTableType
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }
}
