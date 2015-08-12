using System;
using System.Collections.Generic;


namespace Dibware.StoredProcedureFramework
{
    public class ResultList<TR> : ResultList where TR : class
    {
        public ResultList(List<object> items) : base(items)
        {
            
        }

        /// <summary>
        /// Return the result set that contains a particular type and does a cast to that type.
        /// </summary>
        /// <typeparam name="T">Type that was listed in StoredProc object as a possible return type for the stored procedure</typeparam>
        /// <returns>List of T; if no results match, returns an empty list</returns>
        public List<TR> Results
        {
            get { return ToList<TR>(); }

        }
    }
}
