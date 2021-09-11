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
        
        public Archetype(string rawLua) : base(rawLua) => Init();
        public void Init() => Call(Methods.Init, this);
        public override Enum[] GetMethods() => Values<Methods>();
    }
}