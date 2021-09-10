using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MoonSharp.Interpreter;

namespace OpenRpg
{
    public static class LuaIndexer
    {
        public static List<LuaPack> packs = new();
        public static Dictionary<string, Type> classes = new();
        public static Dictionary<string, string> classNames = new();

        public static void Init()
        {
            FileMan.MakeDir("AddonPacks");
            var packDirs = FileMan.GetDirs("AddonPacks").Where(s => s.ToLower().EndsWith(".ap")).ToArray();
            if (packDirs.Length < 1)
            {
                ClrCnsl.WriteLine("[#red]No AddonPacks Detected");
                return;
            }
            
            foreach (var t in Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttributes<IndexAttribute>().Any() && t.IsSubclassOf(typeof(LuaLoader))))
            {
                var att = t.GetCustomAttributes<IndexAttribute>().First();
                UserData.RegisterType(t);
                classes.Add(att.ToString(), t);
                classNames.Add(att.ToString(), att.name);
            }

            foreach (var pack in packDirs) packs.Add(new LuaPack(pack.Replace("\\", "/")));
            foreach (var pack in packs) ObjectPool.LoadPack(pack);
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class IndexAttribute : Attribute
    {
        public string extension;
        public string name;

        public IndexAttribute(string extension, string name) => (this.extension, this.name) = (extension, name);

        public override string ToString() => $".{extension}";
    }
}