using System;
using MoonSharp.Interpreter;

namespace OpenRpg
{
    public abstract class LuaLoader<T>
    {
        static LuaLoader() => UserData.RegisterType<T>();
    }
}