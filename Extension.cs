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

        /// <summary>
        /// do not know if this works with lists, i think it does, it most likely doesn't work with IEnumberable<T>
        /// </summary>
        public static object Clone(this object obj, bool useGlobalVars = false, params object[] param)
        {
            if (obj is null) return null;
            if (obj is Array a) // catch array types, this was painful
            {
                var newA = Array.CreateInstance(a.GetType().GetElementType(), a.Length);
                for (var i = 0; i < newA.Length; i++) newA.SetValue(a.GetValue(i).Clone(useGlobalVars), i);
                return newA;
            }

            var t = obj.GetType();
            if (t.IsPrimitive || obj is string or Enum) return obj;
            var newInst = obj is ICopyable c && param.Length < 1
                ? Activator.CreateInstance(t, c.Arguments())
                : Activator.CreateInstance(t, param);
            var fields = useGlobalVars ? t.GetRuntimeFields() : t.GetFields();
            foreach (var field in fields.Where(f => !f.IsInitOnly))
                field.SetValue(newInst, field.GetValue(obj).Clone(useGlobalVars));
            return newInst;
        }

        public static T Clone<T>(this T t, bool useGlobalVars = false, params object[] param) =>
            (T)((object)t).Clone(useGlobalVars, param);

        public static T[] CloneArr<T>(this IEnumerable<T> t, bool useGlobalVars = false) =>
            t.Select(o => o.Clone(useGlobalVars)).ToArray();

        public static string Name(this Enum e) => nameof(e);
    }
}