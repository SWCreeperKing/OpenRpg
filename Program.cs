using System;
using System.Collections.Generic;
using System.Linq;
using AutoModApi;
using OpenRpg.Scriptables;
using static OpenRpg.ClrCnsl;

namespace OpenRpg;

class Program
{
    public const string Notes = """
                            ==== Weighted Chances ==== 
    When randomly picking stuff with weighted chances all weights are added. 
    Then a random number is picked between 0 and the total weight of the list. 
    The one picked is the first one in the list thats weight is bigger than the picked number
    """; 
    
    public static void Main()
    {
        Console.Clear();
        Console.CursorVisible = false;
        var compile = false;

        Api.projectName = "Open Rpg";
        Api.ReadDir("AddonPacks");
        Api.Initialize("AddonPacks", Notes);
        Api.OnRegister += s =>
        {
            if (!s.StartsWith("Enemy")) return;
            var name = string.Join(".", s.Split(".")[1..]);
            var enemy = Api.CreateType<Enemy>(name);
            enemy.Init(1);
            foreach (var tag in enemy.Tags.Select(s => s.ToLower()))
            {
                if (!Enemy.EnemyTags.ContainsKey(tag)) Enemy.EnemyTags.Add(tag, new List<string>());
                Enemy.EnemyTags[tag].Add(name);
            }
            if (!Enemy.EnemyTypes.ContainsKey(enemy.EnemyType)) Enemy.EnemyTypes.Add(enemy.EnemyType, new List<string>());
            Enemy.EnemyTypes[enemy.EnemyType].Add(name);
        };

        while (true)
        {
            if (!compile)
            {
                Enemy.EnemyTags.Clear();
                Api.CompileWithLoading();
                compile = true;
            }

            WriteLine("[#cyan]OpenRpg v.0.1\n");

            switch (ListView("Start Game", "Scour Game Scripts", "Recompile", "Exit"))
            {
                case 0:
                    break;
                case 1:
                    ContentList();
                    break;
                case 2:
                    Console.Clear();
                    WriteLine("Are you sure you want to recompile?");
                    if (ListView("Yes", "No") == 0) compile = false;
                    break;
                case 3:
                    Console.Clear();
                    WriteLine(
                        "[#red]Goodbye[#r], and [#green]Thanks for Playing[#r]! [#cyan]Please comeback later[#r]!");
                    Environment.Exit(0);
                    return;
            }

            Console.Clear();
        }
    }

    public static void ContentList()
    {
        var groups = Api.ObjectPool.Select(kv => kv.Key).GroupBy(s => s[..s.IndexOf('.')]).ToArray();
        var names = groups.Select(g => g.Key).Select(p => p).Concat(new[] { "Back To Main Menu" }).ToArray();
        while (true)
        {
            Console.Clear();
            WriteLine($"There are [#yellow]{groups.Length}[#r] Script type(s)\n");

            var choice = ListView(names);
            if (choice == names.Length - 1 || choice == -1) return;

            var category = groups[choice];
            var subNames = category.Select(s => s[(s.IndexOf('.') + 1)..]).Concat(new[] { "Back To Script type(s)" })
                .ToArray();

            while (true)
            {
                Console.Clear();
                WriteLine($"There are [#yellow]{category.Count()}[#r] types of [#green]{category.Key}[#r]\n");

                var subChoice = ListView(subNames);
                if (subChoice == subNames.Length - 1 || subChoice == -1) break;

                if (!Api.TypeDictionary[category.Key].IsAssignableTo(typeof(Scriptable))) return;
                var item = (Scriptable) Api.CreateType(Api.TypeDictionary[category.Key], subNames[subChoice]);

                switch (item)
                {
                    case Archetype a:
                        a.Init();
                        break;
                    case Difficulty d:
                        d.Init();
                        break;
                    case Enemy e:
                        e.Init(1);
                        break;
                    case Item i:
                        i.Init(1);
                        break;
                    case Room r:
                        r.Init(1);
                        break;
                }

                DisplayType(item);
            }
        }
    }

    public static void DisplayType(Scriptable scriptable)
    {
        Console.Clear();
        WriteLine(scriptable.GetData());
        WriteLine("\nPress Any Key to go back to Type Selection");
        Console.ReadKey();
    }
}