
namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class IntegerExtensions
    {
        public static int IncrementByOne(this int instance)
        {
            return instance += 1;
        }
    }
}
