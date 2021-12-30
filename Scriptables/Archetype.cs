using System;

namespace OpenRpg
{
    [Index("arc", "Archetype")]
    public class Archetype : LuaLoader
    {
        public enum Methods
        {
            Init
        }

        public string className = "Unknown";
        public string weaponName = "Stick";
        public string desc = "No Description Provided";
        
        public Archetype(string rawLua, string id) : base(rawLua, id) => Init();
        public void Init() => Call(Methods.Init, this);
        public override Enum[] GetMethods() => Values<Methods>();
        public override string GetData() => $"Class Name[{className}]\nWeapon Type: [{weaponName}]\nDescription: {desc}";
    }
}