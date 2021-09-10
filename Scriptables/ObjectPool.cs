using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRpg
{
    public class ObjectPool
    {
        public static List<LuaLoader> objs = new();

        public static void LoadPack(LuaPack pack)
        {
            foreach (var (file, luas) in pack.rawLua)
            {
                var obj = LuaIndexer.classes[file];
                foreach (var lua in luas) objs.Add((LuaLoader)Activator.CreateInstance(obj, lua));
            }
        }

        public static T[] GetObjs<T>() where T : LuaLoader => objs.OfType<T>().CloneArr();
    }
}