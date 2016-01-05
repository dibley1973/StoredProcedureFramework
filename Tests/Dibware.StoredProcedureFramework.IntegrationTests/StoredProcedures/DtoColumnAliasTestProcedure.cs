using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    [Name("DtoColumnAliasTestProcedure")]
    internal class DtoColumnWithAliasAndWithMatchedNamesTestProcedure
        : NoParametersStoredProcedureBase<List<DtoColumnWithAliasAndWithMatchedNamesTestProcedure.Return>>
    {
        internal class Return
        {
            [Name("Identity")]
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Active { get; set; }
        }
    }

    [Name("DtoColumnAliasTestProcedure")]
    internal class DtoColumnNoAliasButMatchedNamesTestProcedure
        : NoParametersStoredProcedureBase<List<DtoColumnNoAliasButMatchedNamesTestProcedure.Return>>
    {
        internal class Return
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Active { get; set; }
        }
    }

    [Name("DtoColumnAliasTestProcedure")]
    internal class DtoColumnWithoutAliasAndWithoutMatchingNamesTestProcedure
        : NoParametersStoredProcedureBase<List<DtoColumnWithoutAliasAndWithoutMatchingNamesTestProcedure.Return>>
    {
        internal class Return
        {
            public int Identity { get; set; }
            public string Fullname { get; set; }
            public bool IsActive { get; set; }
        }
    }

    [Name("DtoColumnAliasTestProcedure")]
    internal class DtoColumnWithAliasAndWithNamesMatchingTestProcedure
        : NoParametersStoredProcedureBase<List<DtoColumnWithAliasAndWithNamesMatchingTestProcedure.Return>>
    {
        internal class Return
        {
            [Name("Id")]
            public int Identity { get; set; }
            [Name("Name")]
            public string Fullname { get; set; }
            [Name("Active")]
            public bool IsActive { get; set; }
        }
    }
}
