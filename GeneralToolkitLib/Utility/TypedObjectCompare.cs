using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GeneralToolkitLib.Utility
{
    public static class TypedObjectCompare
    {
        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self == null || to == null) return self == to;
            Type type = typeof(T);
            var ignoreList = new List<string>(ignore);
            return !(from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    !ignoreList.Contains(pi.Name)
                let selfValue = type.GetProperty(pi.Name)?.GetValue(self, null)
                let toValue = type.GetProperty(pi.Name)?.GetValue(to, null)
                where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                select selfValue).Any();
        }
    }
}
