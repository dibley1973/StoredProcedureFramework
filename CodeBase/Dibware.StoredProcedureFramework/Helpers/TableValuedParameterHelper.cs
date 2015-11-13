using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers
{
    internal static class TableValuedParameterHelper
    {

        /// <summary>
        /// Get the underlying class type for lists, etc. that implement IEnumerable<>.
        /// </summary>
        /// <param name="listtype"></param>
        /// <returns></returns>
        private static Type GetUnderlyingType(Type listtype)
        {
            Type basetype = null;
            foreach (Type i in listtype.GetInterfaces())
                if (i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>)))
                    basetype = i.GetGenericArguments()[0];

            return basetype;
        }


        /// <summary>
        /// Do the work of converting a source data object to SqlDataRecords
        /// using the parameter attributes to create the table valued parameter definition
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        internal static IEnumerable<SqlDataRecord> TableValuedParameter(IList table)
        {
            // get the object type underlying our table
            Type t = GetUnderlyingType(table.GetType());

            // list of converted values to be returned to the caller
            List<SqlDataRecord> recordlist = new List<SqlDataRecord>();

            // get all mapped properties
            //PropertyInfo[] props = CodeFirstStoredProcHelpers.GetMappedProperties(t);
            PropertyInfo[] props = t.GetMappedProperties();

            // get the column definitions, into an array
            List<SqlMetaData> columnlist = new List<SqlMetaData>();

            // get the propery column name to property name mapping
            // and generate the SqlMetaData for each property/column
            Dictionary<String, String> mapping = new Dictionary<string, string>();
            foreach (PropertyInfo p in props)
            {
                // default name is property name, override of parameter name by attribute
                var attr = p.GetAttribute<NameAttribute>();
                String name = (null == attr) ? p.Name : attr.Value;
                mapping.Add(name, p.Name);

                // get column type
                ParameterDbTypeAttribute ct = p.GetAttribute<ParameterDbTypeAttribute>();
                SqlDbType coltype = (null == ct) ? SqlDbType.Int : ct.Value;

                //TODO; handle data type mappings from underlying CLR type!
                // dont just default to INT!

                // create metadata column definition
                SqlMetaData column;
                switch (coltype)
                {
                    case SqlDbType.Binary:
                    case SqlDbType.Char:
                    case SqlDbType.NChar:
                    case SqlDbType.Image:
                    case SqlDbType.VarChar:
                    case SqlDbType.NVarChar:
                    case SqlDbType.Text:
                    case SqlDbType.NText:
                    case SqlDbType.VarBinary:
                        // get column size
                        var sa = p.GetAttribute<SizeAttribute>();
                        int size = (null == sa) ? 50 : sa.Value;
                        column = new SqlMetaData(name, coltype, size);
                        break;

                    case SqlDbType.Decimal:
                        // get column precision and scale
                        var pa = p.GetAttribute<PrecisionAttribute>();
                        Byte precision = (null == pa) ? (byte)10 : pa.Value;
                        var sca = p.GetAttribute<ScaleAttribute>();
                        Byte scale = (null == sca) ? (byte)2 : sca.Value;
                        column = new SqlMetaData(name, coltype, precision, scale);
                        break;

                    default:
                        column = new SqlMetaData(name, coltype);
                        break;
                }

                // Add metadata to column list
                columnlist.Add(column);
            }

            // load each object in the input data table into sql data records
            foreach (object s in table)
            {
                // create the sql data record using the column definition
                SqlDataRecord record = new SqlDataRecord(columnlist.ToArray());
                for (int i = 0; i < columnlist.Count(); i++)
                {
                    // locate the value of the matching property
                    var value = props.Where(p => p.Name == mapping[columnlist[i].Name])
                        .First()
                        .GetValue(s, null);

                    // set the value
                    record.SetValue(i, value);
                }

                // add the sql data record to our output list
                recordlist.Add(record);
            }

            // return our list of data records
            return recordlist;
        }
    }

}
