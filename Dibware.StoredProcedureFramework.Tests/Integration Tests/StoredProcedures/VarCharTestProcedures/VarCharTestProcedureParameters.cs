using System;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.VarCharTestProcedures
{
    internal class VarCharTestProcedureParameters
    {
        [Size(7)]
        public String Parameter1 { get; set; }
    }
}