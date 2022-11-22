using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.ConsoleKey;

namespace OpenRpg;

public static class ClrCnsl
{
    public static readonly Regex RegMatch = new(@"\[(#|!)(\w+?)\]", RegexOptions.Compiled);
    public const int ListLeng = 7;

    public static int ListView(params string[] options)
    {
        var amounts = options.Select(s => CleanColors(s).Length).ToArray();
        var maxSpace = amounts.Max() + 2;
        var top = Console.GetCursorPosition().Top;
        var selected = 0;
        var isMore = options.Length > ListLeng;

        string SpaceOut(string s)
        {
            var len = s.Length;
            var remainder = maxSpace - len;
            var halfRemainder = remainder / 2f;
            var left = (int) Math.Floor(halfRemainder) + len;
            var right = (int) Math.Ceiling(halfRemainder);
            return s.PadLeft(left).PadRight(left + right);
        }

        var formatedOptions = options.Select(SpaceOut).ToArray();
        var moreLeng = new[] { "^", "v" }.Select(SpaceOut).ToArray();

        while (true)
        {
            Console.SetCursorPosition(0, top);
            if (isMore) WriteLine($"[#yellow]{moreLeng[0]}");

            for (var i = 0; i < Math.Min(formatedOptions.Length, ListLeng); i++)
            {
                var rI = i + Math.Max(0,
                    Math.Min(formatedOptions.Length - ListLeng, selected + 1 - (int) Math.Ceiling(ListLeng / 2f)));
                var isS = selected == rI;
                WriteLine(
                    $"{(isS ? "[#green] >[!darkgray][#cyan]" : "  [#blue]")}{formatedOptions[rI]}{(isS ? "[#green][!r]< " : "  ")}");
            }

            if (isMore) WriteLine($"[#yellow]{moreLeng[1]}");

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
                case Escape:
                    return -1;
            }
        }
    }

    public static string CleanColors(string text) => RegMatch.Replace(text, "");

    public static void Write(string text)
    {
        Stack<ConsoleColor> foregroundColorStack = new();
        Stack<ConsoleColor> backgroundColorStack = new();
        foregroundColorStack.Push(ConsoleColor.White);
        backgroundColorStack.Push(ConsoleColor.Black);
        void PeekColor() => SetColors(foregroundColorStack.Peek(), backgroundColorStack.Peek());

        Span<char> txt = text.ToCharArray();
        var (beforeFore, beforeBack) = GetColors();
        PeekColor();
        var index = 0;

        while (RegMatch.IsMatch(txt, index))
        {
            var match = RegMatch.Match(text, index);
            var matchValue = match.Value;
            var nIndex = text.IndexOf(matchValue, index, StringComparison.Ordinal);
            var isBack = match.Groups[1].Value == "!";

            Console.Write(txt[index..nIndex].ToString());
            var rawValue = match.Groups[2].Value;
            ConsoleColor? color = rawValue == "r" ? null : Enum.Parse<ConsoleColor>(rawValue, true);

            switch (isBack)
            {
                case true when color is null && backgroundColorStack.Count > 1:
                    backgroundColorStack.Pop();
                    break;
                case true when color is not null:
                    backgroundColorStack.Push(color.Value);
                    break;
                default:
                    if (color is null && foregroundColorStack.Count > 1) foregroundColorStack.Pop();
                    else if (color is not null) foregroundColorStack.Push(color.Value);
                    break;
            }

            PeekColor();
            index = nIndex + matchValue.Length;
        }

        if (index < txt.Length) Console.Write(txt[index..].ToString());
        SetColors(beforeFore, beforeBack);
    }

    public static void Write(object s) => Write(s.ToString());
    public static void WriteLine(string s) => Write($"{s}\n");
    public static void WriteLine(object s) => Write($"{s}\n");

    public static (ConsoleColor, ConsoleColor) GetColors() => (Console.ForegroundColor, Console.BackgroundColor);

    public static void SetColors(ConsoleColor? foreground = null, ConsoleColor? background = null)
    {
        if (foreground is not null) Console.ForegroundColor = foreground!.Value;
        if (background is not null) Console.BackgroundColor = background!.Value;
    }
}