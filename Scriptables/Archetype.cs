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
        public float speed = 1;
        public float speedRegen = 1;
        public int defense = 1;
        public int attack = 1;
        public int maxHp = 100;

        public Archetype(string rawLua, string id) : base(rawLua, id) => Init();
        public void Init() => Call(Methods.Init, this);
        public override Enum[] GetMethods() => Values<Methods>();
        public override string GetData() => @$"Class Name: [{className}]
Weapon Type: [{weaponName}]
Max Hp: [#red]{maxHp}[#r] | defense: [#gray]{defense}[#r] | Attack: [#blue]{attack}[#r]
Speed: {speed} (+{speedRegen}/other turn)

Description:
{desc}";
    }
}