using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace OpenRpg
{
    public static class FileMan
    {
        public static string GetSavePath => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/SW_CreeperKing/OpenRpg";
        
        public static void Save<T>(this T t, int iterate = 0)
        {
            var path = GetSavePath;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            using var sw = File.CreateText($"{path}/{typeof(T).Name}{iterate}.txt");
            sw.Write(JsonConvert.SerializeObject(t));
            sw.Close();
        }

        public static void Load<T>(this T t, int iterate = 0)
        {
            var path = GetSavePath;
            var filePath = $"{path}/{typeof(T).Name}{iterate}.txt";
            if (!Directory.Exists(path) || !File.Exists(filePath)) return;
            using StreamReader sr = new(filePath);
            t.Set(JsonConvert.DeserializeObject<T>(sr.ReadToEnd()));
            sr.Close();
        }
        
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