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
        public string desc = "No Description Provided";
        public double baseModifier = 1f;

        public Difficulty(string rawLua, string id) : base(rawLua, id) => Init();
        public void Init() => Call(Methods.Init, this);
        public double Modifier(double floor) => Call(Methods.FloorModifier, floor);

        public override Enum[] GetMethods() => Values<Methods>();
        public override string GetData() => @$"Difficulty Name: {name}
Modifier: {baseModifier}

Description:
{desc}";
    }
}