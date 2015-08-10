using System;

namespace Dibware.StoredProcedureFramework.StoredProcAttributes
{
    /// <summary>
    /// Allows the setting of the user defined table type name for table valued parameters
    /// </summary>
    public class ReturnTypes : Attribute
    {
        public Type[] Returns { get; set; }

        public ReturnTypes(params Type[] values)
        {
            Returns = values;
        }
    }
}