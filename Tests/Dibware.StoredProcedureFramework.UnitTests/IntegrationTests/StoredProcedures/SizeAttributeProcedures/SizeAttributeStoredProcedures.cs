using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.SizeAttributeProcedures
{
    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class CorrectSizeAttributeStoredProcedure
        : StoredProcedureBase<List<CorrectSizeAttributeReturnType>, CorrectSizeAttributeParameters>
    {
        public CorrectSizeAttributeStoredProcedure(CorrectSizeAttributeParameters parameters)
            : base(parameters)
        {
        }
    }

    //[Schema("app")]
    //[Name("SizeAttributeTest")]
    //internal class CorrectSizeAttributeStoredProcedure
    //    : StoredProcedureBase<CorrectSizeAttributeStoredProcedureResultSet, CorrectSizeAttributeParameters>
    //{
    //    public CorrectSizeAttributeStoredProcedure(CorrectSizeAttributeParameters parameters)
    //        : base(parameters)
    //    {
    //    }
    //}

    
    //internal class CorrectSizeAttributeStoredProcedureResultSet
    //{
    //    public List<CorrectSizeAttributeReturnType> RecordSet1 { get; set; }

    //    public CorrectSizeAttributeStoredProcedureResultSet()
    //    {
    //        RecordSet1 = new List<CorrectSizeAttributeReturnType>();
    //    }
    //}




    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class TooSmallSizeAttributeStoredProcedure
        : StoredProcedureBase<List<TooSmallSizeAttributeReturnType>, TooSmallSizeAttributeParameters>
    {
        public TooSmallSizeAttributeStoredProcedure(TooSmallSizeAttributeParameters parameters)
            : base(parameters)
        {
        }
    }

    //[Schema("app")]
    //[Name("SizeAttributeTest")]
    //internal class TooSmallSizeAttributeStoredProcedure
    //    : StoredProcedureBase<TooSmallSizeAttributeStoredProcedureResultSet, TooSmallSizeAttributeParameters>
    //{
    //    public TooSmallSizeAttributeStoredProcedure(TooSmallSizeAttributeParameters parameters)
    //        : base(parameters)
    //    {
    //    }
    //}

    //internal class TooSmallSizeAttributeStoredProcedureResultSet
    //{
    //    public List<TooSmallSizeAttributeReturnType> RecordSet1 { get; set; }

    //    public TooSmallSizeAttributeStoredProcedureResultSet()
    //    {
    //        RecordSet1 = new List<TooSmallSizeAttributeReturnType>();
    //    }
    //}


    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class TooLargeSizeAttributeStoredProcedure
        : StoredProcedureBase<List<TooLargeSizeAttributeReturnType>, TooLargeSizeAttributeParameters>
    {
        public TooLargeSizeAttributeStoredProcedure(TooLargeSizeAttributeParameters parameters)
            : base(parameters)
        {
        }
    }


    [Schema("app")]
    [Name("SizeAttributeTest")]
    internal class TooLargeValueButNoSizeAttributeStoredProcedure
        : StoredProcedureBase<List<TooLargeSizeAttributeReturnType>, TooLargeValueButNoSizeAttribute>
    {
        public TooLargeValueButNoSizeAttributeStoredProcedure(TooLargeValueButNoSizeAttribute parameters)
            : base(parameters)
        {
        }
    }


    //[Schema("app")]
    //[Name("SizeAttributeTest")]
    //internal class TooLargeSizeAttributeStoredProcedure
    //    : StoredProcedureBase<TooLargeSizeAttributeStoredProcedureResultSet, TooLargeSizeAttributeParameters>
    //{
    //    public TooLargeSizeAttributeStoredProcedure(TooLargeSizeAttributeParameters parameters)
    //        : base(parameters)
    //    {
    //    }
    //}

    //internal class TooLargeSizeAttributeStoredProcedureResultSet
    //{
    //    public List<TooLargeSizeAttributeReturnType> RecordSet1 { get; set; }
    //}
}
