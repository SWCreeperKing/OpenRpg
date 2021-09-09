using System.Collections.Generic;
using System.IO;

namespace OpenRpg
{
    public static class FileMan
    {
        public static bool MakeDir(string dir)
        {
            if (Directory.Exists(dir)) return true;
            Directory.CreateDirectory(dir);
            return false;
        }

        public static string[] GetFiles(string dir)
        {
            List<string> files = new();
            files.AddRange(Directory.GetFiles(dir));
            foreach (var d in GetDirs(dir))
                files.AddRange(GetFiles(d));
            return files.ToArray();
        }

        public static string[] GetDirs(string dir) => Directory.GetDirectories(dir);
    }
}