using System;
using static OpenRpg.ClrCnsl;

namespace OpenRpg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            WriteLine("[#cyan]OpenRpg v.0.1");
            LuaIndexer.Init();

            foreach (var item in ObjectPool.GetObjs<Item>()) WriteLine(item.Init(5));
            Console.WriteLine();
            Console.WriteLine();
            foreach (var arc in ObjectPool.GetObjs<Archetype>()) 
                WriteLine($"{arc.className} uses {arc.weaponName}");

            Console.ReadLine();
        }
    }
}