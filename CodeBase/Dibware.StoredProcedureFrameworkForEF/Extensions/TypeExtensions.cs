using System;
using System.Linq;

namespace Dibware.StoredProcedureFrameworkForEF.Extensions
{
    /// <summary>
    /// provides extension methods for the <see cref="System.Type"/>
    /// </summary>
    internal static class TypeExtensions
    {
        public static string GetPropertName(this Type instance, string propertyInfoName)
        {
            var memberInfos = instance.GetMember(propertyInfoName);
            var memberInfo = memberInfos.FirstOrDefault();
            return memberInfo == null ? null : memberInfo.Name;
        }
    }
}