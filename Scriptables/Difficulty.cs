using System;

namespace OpenRpg
{
    [Index("diff", "Difficulties")]
    public class Difficulty : LuaLoader
    {
        public string name = "Unknown Difficulty";
        public double baseModifier = 1f;
        
        private enum DiffMethods
        {
            Init,
            FloorModifier
        }

        public Difficulty(string rawLua) : base(rawLua)
        {
        }
        
        public override Enum[] GetMethods() => Values<DiffMethods>();

        public void Init() => Call(DiffMethods.Init, this);
        public double Modifier(double floor) => Call(DiffMethods.FloorModifier, floor);
    }
}