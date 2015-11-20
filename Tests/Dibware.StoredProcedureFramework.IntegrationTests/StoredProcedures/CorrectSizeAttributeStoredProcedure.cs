﻿using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    [Name("SizeAttributeStoredProcedure")]
    internal class CorrectSizeAttributeStoredProcedure
        : StoredProcedureBase<List<CorrectSizeAttributeStoredProcedure.Return>, CorrectSizeAttributeStoredProcedure.Parameter>
    {
        public CorrectSizeAttributeStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            [Size(20)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string Value1 { get; set; }
        }

        internal class Return
        {
            [Size(255)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string Value1 { get; set; }
        }
    }
}
