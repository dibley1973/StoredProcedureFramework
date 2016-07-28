
using Dibware.StoredProcedureFramework.Helpers;
using System.Dynamic;

namespace Dibware.StoredProcedureFrameworkForEF.PackageLoadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test_1_0_2();
        }

        private static void Test_1_0_2()
        {
            dynamic dynamicObject = new ExpandoObject();
            DynamicObjectHelper.HasProperty(dynamicObject, "Firstname");
        }
    }
}
