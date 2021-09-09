using System;
using System.Text.RegularExpressions;

namespace OpenRpg
{
    public static class ClrCnsl
    {
        public const string RegMatch = @"\[#(\w+?)\]";

        public static void Write(string text)
        {
            var txt = text;
            var before = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            while (Regex.IsMatch(txt, RegMatch))
            {
                var match = Regex.Match(txt, RegMatch);
                var index = txt.IndexOf(match.Value, StringComparison.Ordinal); 
                Console.Write(txt[..index]);
                Console.ForegroundColor = Enum.Parse<ConsoleColor>(match.Groups[1].Value, true);
                txt = txt[(index + match.Value.Length)..];
            }

            Console.Write(txt);
            Console.ForegroundColor = before;
        }

        public static void WriteLine(string s) => Write($"{s}\n");
    }
}