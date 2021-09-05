using System;
using MoonSharp.Interpreter;

namespace OpenRpg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ClrCnsl.WriteLine("[#cyan]OpenRpg v.0.1");
            ClrCnsl.WriteLine("[#red]Hel[#yello]lo Wor[#darkcyan]ld");

            foreach (var t in UserData.GetRegisteredTypes())
                Console.WriteLine($"T: {t.Name}");
        }
    }
}