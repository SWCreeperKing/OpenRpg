using System;

namespace OpenRpg
{
    [Index("diff", "Difficulty")]
    public class Difficulty : LuaLoader
    {
        private enum Methods
        {
            Init,
            FloorModifier
        }
        
        public string name = "Unknown Difficulty";
        public double baseModifier = 1f;

        public Difficulty(string rawLua) : base(rawLua) => Init();
        public void Init() => Call(Methods.Init, this);
        public double Modifier(double floor) => Call(Methods.FloorModifier, floor);
        
        public override Enum[] GetMethods() => Values<Methods>();
    }
}