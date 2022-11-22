using System;
using System.Collections.Generic;
using AutoModApi;
using OpenRpg.Scriptables;
using static OpenRpg.EnumBank;

namespace OpenRpg;

public class Player : ApiScript
{
    public static Random r = new();

    public string Name { get; set; }
    public float Hp { get; set; }
    public float Xp { get; set; }
    public int Level { get; set; } = 1;
    public Archetype Archetype { get; set; }
    public Dictionary<Consumables, float> Inventory { get; set; } = new();

    public void GainXp(float xp)
    {
        Xp += xp;
        while (Xp >= RequiredXp())
        {
            Xp -= RequiredXp();
            Level++;
        }
    }

    public long CurrentFloor()
    {
        return 0;
    }

    public float RequiredXp() => 20 + 10 * Level;
    public float GetFromInv(string consumable) => Inventory[consumable.ToEnum<Consumables>()];
    public void AddToInv(string consumable, float amount) => Inventory[consumable.ToEnum<Consumables>()] += amount;
    public void Heal(float amount) => Hp = Math.Max(Hp + amount, Archetype.MaxHp + Level * 10);
    public int Random(int min, int max) => r.Next(min, max + 1);
}