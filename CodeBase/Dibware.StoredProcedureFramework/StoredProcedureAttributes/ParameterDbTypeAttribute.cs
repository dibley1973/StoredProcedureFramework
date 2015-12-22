using System;

namespace Dibware.StoredProcedureFramework.StoredProcedureAttributes
{
    [Obsolete("This attribute has been superseeded by the {DbTypeAttribute} attribute. Please update your code accordingly", true)]
    public class ParameterDbTypeAttribute : Attribute
    {
    }
}