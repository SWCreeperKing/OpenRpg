using System;
using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter;

namespace OpenRpg
{
    public abstract class LuaLoader : ICopyable
    {
        public static CoreModules luaImports = CoreModules.Basic | CoreModules.Math;

        public string rawLua;
        public string id;

        private Script _script;
        private Dictionary<Enum, object> scripts = new();

        public LuaLoader(string rawLua, string id)
        {
            (this.id, this.rawLua) = (id, rawLua);
            // if (rawLua == "") throw new ArgumentNullException();
            _script = new Script(luaImports);
            _script.DoString(rawLua);
            foreach (var method in GetMethods()) scripts.Add(method, ProcessScript(method.ToString()));
        }

        private object ProcessScript(string name) =>
            _script.Globals.Keys.Contains(DynValue.NewString(name)) ? _script.Globals[name] : null;

        public abstract Enum[] GetMethods();
        
        public static Enum[] Values<TEnum>() where TEnum : struct, Enum
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException($"{typeof(TEnum).Name} is not an enum");
            return Enum.GetValues<TEnum>().Select(e => (Enum)e).ToArray();
        }

        public DynValue Call(Enum method, params object[] param) =>
            scripts.ContainsKey(method) && scripts[method] != null
                ? _script.Call(scripts[method], param)
                : DynValue.Nil;

        public DynValue Call(Enum method, DynValue def, params object[] param)
        {
            var call = Call(method, param);
            return call.Equals(DynValue.Nil) ? def : call;
        }

        public double Call(Enum method, double def, params object[] param) =>
            Call(method, DynValue.NewNumber(def), param).Number;

        public object[] Arguments() => new[]{rawLua};

        public abstract string GetData();
    }
}