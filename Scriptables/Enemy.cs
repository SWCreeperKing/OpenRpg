using System;
using System.ComponentModel;

namespace OpenRpg
{
    [Index("enemy", "Enemy")]
    public class Enemy : LuaLoader
    {
        private enum Methods
        {
            Init
        }
        
        public string name = "Unknown Enemy";
        public string desc = "No Description Provided";
        public float speed = 1;
        public float speedRegen = 1;
        public int defense = 1;
        public int attack = 1;
        public int maxHp = 100;
        public int hp;

        public Enemy(string rawLua, string id) : base(rawLua, id)
        {
        }
        
        public void Init(int enemyLevel)
        {
            Call(Methods.Init, this, enemyLevel);
            hp = maxHp;
        }

        public override Enum[] GetMethods() => Values<Methods>();

        public override string GetData() =>
           @$"Enemy Name: [{name}]
Max Hp: [#red]{maxHp}[#r] | defense: [#gray]{defense}[#r] | Attack: [#blue]{attack}[#r]
Speed: {speed} (+{speedRegen}/other turn)

Description: 
{desc}";
    }
}