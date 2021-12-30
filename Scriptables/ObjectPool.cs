using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRpg
{
    public class ObjectPool
    {
        public static Dictionary<string, LuaLoader> objs = new();

        public static void LoadPack(LuaPack pack)
        {
            foreach (var (file, luas) in pack.rawLua)
            {
                var obj = LuaIndexer.classes[file];
                foreach (var lua in luas)
                {
                    var split = pack.names[lua].Split('.');
                    var name = $"{pack.packName}.{split[^2]}.{split[0]}";
                    var initObj = (LuaLoader)Activator.CreateInstance(obj, lua, name);
                    switch (initObj)
                    {
                        case Difficulty d:
                            d.Init();
                            break;
                        case Archetype a:
                            a.Init();
                            break;
                        case Item i:
                            i.Init(1);
                            break;
                    } 
                    objs.Add(name, initObj);
                }
            }
            pack.ProcessLua();
        }

        public static T[] GetObjs<T>() where T : LuaLoader => objs.OfType<T>().CloneArr();
    }
}