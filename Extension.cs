using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenRpg
{
    public static class Extension
    {
        public static T ToEnum<T>(this string s) where T : struct => Enum.Parse<T>(s, true);

        public static object Clone(this object obj, params object[] param)
        {
            if (obj is null) return null;
            if (obj is Array a) // catch array types, this was painful
            {
                var newA = Array.CreateInstance(a.GetType().GetElementType(), a.Length);
                for (var i = 0; i < newA.Length; i++) newA.SetValue(a.GetValue(i).Clone(), i);
                return newA;
            }
            
            var t = obj.GetType();
            if (t.IsPrimitive || obj is string or Enum) return obj;
            var newInst = obj is ICopyable c
                ? Activator.CreateInstance(t, c.Arguments())
                : Activator.CreateInstance(t, param);
            foreach (var field in t.GetFields().Where(f => !f.IsInitOnly))
                field.SetValue(newInst, field.GetValue(obj).Clone());
            return newInst;
        }

        public static T Clone<T>(this T t, params object[] param) => (T)((object)t).Clone(param);

        public static T[] CloneArr<T>(this IEnumerable<T> t, params object[] param) => t.Select(o => o.Clone()).ToArray();
        public static string Name(this Enum e) => nameof(e);
    }
}