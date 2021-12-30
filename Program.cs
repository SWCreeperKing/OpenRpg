using System;
using System.Linq;
using static OpenRpg.ClrCnsl;

namespace OpenRpg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            LuaIndexer.Init();

            // foreach (var item in ObjectPool.GetObjs<Item>()) WriteLine(item.Init(5));
            // foreach (var arc in ObjectPool.GetObjs<Archetype>()) 
            //     WriteLine($"{arc.className} uses {arc.weaponName}");

            while (true)
            {
                WriteLine("[#cyan]OpenRpg v.0.1\n");

                switch (ListView("Start Game", "Content Packs", "Exit"))
                {
                    case 0:
                        break;
                    case 1:
                        ContentPacks();
                        break;
                    case 2:
                        return;
                }

                Console.Clear();
            }
        }

        public static void ContentPacks()
        {
            var names = LuaIndexer.packs.Select(p => p.packName).Concat(new[] { "Back To Main Menu" }).ToArray();
            while (true)
            {
                Console.Clear();
                WriteLine($"There are [#yellow]{LuaIndexer.packs.Count}[#r] Content Pack(s)\n");

                var choice = ListView(names);
                if (choice == names.Length - 1) return;
                ListPack(LuaIndexer.packs[choice]);
            }
        }

        public static void ListPack(LuaPack pack)
        {
            var each = pack.processedLuaIds.Select(kv => $"[{LuaIndexer.classNames[kv.Key]}]: {kv.Value.Count}")
                .Concat(new[] { "Back To Content Packs" }).ToArray();

            while (true)
            {
                Console.Clear();
                WriteLine($"You are viewing the [#yellow]{pack.packName}[#r] Content Pack\n");

                var choice = ListView(each);
                if (choice == each.Length - 1) return;
                var id = pack.processedLuaIds.Keys.ToArray()[choice];
                var items = pack.processedLuaIds[id].Concat(new[] {"Back to Type Selection"}).ToArray();

                while (true)
                {
                    Console.Clear();
                    WriteLine($"You are viewing the [#green]{LuaIndexer.classNames[id]}[#r] types from the [#yellow]{pack.packName}[#r] Content Pack");

                    var item = ListView(items);
                    if (item == items.Length - 1) break;
                    WriteLine(ObjectPool.objs[items[item]].GetData());
                    WriteLine("\nPress Any Key to go back to Type Selection");
                    Console.ReadKey();
                }
            }

            
            WriteLine(
                $"Files: \n{string.Join("\n", pack.processedLuaIds.Select(kv => $"[{LuaIndexer.classNames[kv.Key]}]\n  -{string.Join("\n  -", kv.Value)}"))}");
            Console.ReadKey();
        }
    }
}