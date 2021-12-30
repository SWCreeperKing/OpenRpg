using System;
using System.Linq;
using System.Text.RegularExpressions;
using static System.ConsoleKey;

namespace OpenRpg
{
    public static class ClrCnsl
    {
        public static readonly Regex RegMatch = new(@"\[#(\w+?)\]");
        public const int ListLeng = 7;

        public static int ListView(params string[] options)
        {
            var amounts = options.Select(s => CleanColors(s).Length).ToArray();
            var space = string.Join("", Enumerable.Repeat(" ", (int)Math.Floor(amounts.Max() / 2f) + 1));
            var end = string.Join("", Enumerable.Repeat(" ", amounts.Max() - amounts.Min()));
            var top = Console.GetCursorPosition().Top;
            var selected = 0;
            var isMore = options.Length > ListLeng;
            while (true)
            {
                Console.SetCursorPosition(0, top);
                if (isMore) WriteLine($"[#yellow]{space}^");

                for (var i = 0; i < Math.Min(options.Length, ListLeng); i++)
                {
                    var rI = i + Math.Max(0,
                        Math.Min(options.Length - ListLeng, selected + 1 - (int)Math.Ceiling(ListLeng / 2f)));
                    var isS = selected == rI;
                    WriteLine($"{(isS ? "[#green] >" : "  ")}[#blue]{options[rI]}{(isS ? "[#green]< " : "  ")}{end}");
                }

                if (isMore) WriteLine($"[#yellow]{space}v");

                switch (Console.ReadKey(true).Key)
                {
                    case W or UpArrow:
                        selected--;
                        if (selected < 0) selected = options.Length - 1;
                        break;
                    case S or DownArrow:
                        selected++;
                        if (selected >= options.Length) selected = 0;
                        break;
                    case Enter or Spacebar:
                        return selected;
                }
            }
        }

        public static string CleanColors(string text) => RegMatch.Replace(text, "");

        public static void Write(string text)
        {
            var txt = text.Replace("[#r]", "[#white]");
            var before = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            while (RegMatch.IsMatch(txt))
            {
                var match = RegMatch.Match(txt);
                var index = txt.IndexOf(match.Value, StringComparison.Ordinal);
                Console.Write(txt[..index]);
                Console.ForegroundColor = Enum.Parse<ConsoleColor>(match.Groups[1].Value, true);
                txt = txt[(index + match.Value.Length)..];
            }

            Console.Write(txt);
            Console.ForegroundColor = before;
        }

        public static void Write(object s) => Write(s.ToString());
        public static void WriteLine(string s) => Write($"{s}\n");
        public static void WriteLine(object s) => Write($"{s}\n");
    }
}