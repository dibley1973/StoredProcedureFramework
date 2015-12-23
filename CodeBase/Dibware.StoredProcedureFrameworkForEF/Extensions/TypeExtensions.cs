using System;
using System.Linq;
using System.Reflection;

namespace Dibware.StoredProcedureFrameworkForEF.Extensions
{
    /// <summary>
    /// provides extension methods for the <see cref="System.Type"/>
    /// </summary>
    internal static class TypeExtensions
    {
        public static string GetPropertyName(this Type instance, string propertyInfoName)
        {
            //TODO: this is ripe for returning a Maybe<string>

            var memberInfos = instance.GetMember(propertyInfoName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var memberInfo = memberInfos.FirstOrDefault();
            return memberInfo == null ? null : memberInfo.Name;
        }
    }
}