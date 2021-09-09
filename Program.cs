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
        }
    }
}