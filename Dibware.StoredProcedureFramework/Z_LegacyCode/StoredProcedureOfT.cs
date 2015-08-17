
using Dibware.StoredProcedureFramework.StoredProcAttributes;
using System;

namespace Dibware.StoredProcedureFramework
{
    ///// <summary>
    ///// Genericized version of StoredProcedure object, which takes a .Net POCO 
    ///// object type for the parameters.
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class StoredProcedure<T> : StoredProcedure
    //    where T : class
    //{

    //    public void InitializeFromAttributes()
    //    {
    //        //Type type = GetType();
    //        Type type = typeof(T);

    //        // Using reflection.
    //        //Attribute[] attrs = Attribute.GetCustomAttributes(type);  // Reflection. 
    //        //var attr = Attribute.GetCustomAttribute(type, typeof(NameAttribute));
    //        //SetProcedureName(Attribute.GetCustomAttribute()te<StoredProcAttributes.NameAttribute>(T));
    //        //SetSchemaName(attrs);
    //        //SetReturnType(attrs);

    //        TrySetProcedureNameFromAttribute(type);
    //        TrySetReturnTypeFromAttribute(type);
    //        TrySetSchemaNameFromAttribute(type);
    //    }

    //    private void TrySetProcedureNameFromAttribute(Type type)
    //    {
    //        var attribute = Attribute.GetCustomAttribute(type, typeof(NameAttribute)) as NameAttribute;
    //        if (attribute != null)
    //        {
    //            SetProcedureName(attribute.Value);
    //        }
    //    }

    //    private void TrySetReturnTypeFromAttribute(Type type)
    //    {
    //        ReturnTypeAttribute attribute = Attribute.GetCustomAttribute(type, typeof(ReturnTypeAttribute)) as ReturnTypeAttribute;
    //        if (attribute != null)
    //        {
    //            SetReturnType(attribute.Returns);
    //        }
    //    }

    //    private void TrySetSchemaNameFromAttribute(Type type)
    //    {
    //        SchemaAttribute attribute = Attribute.GetCustomAttribute(type, typeof(SchemaAttribute)) as SchemaAttribute;
    //        if (attribute != null)
    //        {
    //            SetSchemaName(attribute.Value);
    //        }
    //    }
    //}
}