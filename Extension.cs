using System;
using System.Reflection;

namespace OpenRpg
{
    public static class Extension
    {
        public static T ToEnum<T>(this string s) where T : struct => Enum.Parse<T>(s, true);
        
        public static object Clone(this object obj, params object[] param)
        {
            if (obj.GetType().IsPrimitive) return obj;
            var t = obj.GetType();
            var newInst = Activator.CreateInstance(t, param);
            foreach (var field in t.GetRuntimeFields())
                field.SetValue(newInst,  field.GetValue(obj).Clone());
            return newInst;
        }

        public static T Clone<T>(this T t, params object[] param) => (T)((object)t).Clone(param);

        public static string Name(this Enum e) => nameof(e);
    }
}