
namespace Dibware.StoredProcedureFramework
{
    /// <summary>
    /// Genericized version of StoredProcedure object, which takes a .Net POCO 
    /// object type for the parameters.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoredProcedure<T> : StoredProcedure
        where T : class
    {

    }
}