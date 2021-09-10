using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            foreach (var item in ObjectPool.GetObjs<Item>())
            {
                item.Init(5);
                Console.WriteLine(item.name);
            }
        }
    }
}