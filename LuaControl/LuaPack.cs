using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenRpg
{
    public class LuaPack
    {
        public string corePath;
        public string packName;
        public Dictionary<string, List<string>> processedLuaIds = new();
        public Dictionary<string, List<string>> packFiles = new();
        public Dictionary<string, List<string>> rawLua = new();
        public Dictionary<string, string> names = new();

        public LuaPack(string corePath)
        {
            this.corePath = corePath;
            packName = corePath.Split('/')[^1].Split('.')[0];
            LoadFiles();
            ClrCnsl.WriteLine(packFiles.Keys.Count < 1
                ? $"[#yellow]Pack: [{packName}] loaded 0 files"
                : $"[#blue]Pack: [{packName}] loaded {string.Join(" ", packFiles.Select(kv => $"[{LuaIndexer.classNames[kv.Key]}(s): {kv.Value.Count}]"))}");
            ProcessFiles();
        }

        private void LoadFiles()
        {
            var files = FileMan.GetFiles(corePath);
            if (files.Length < 1) return;
            foreach (var file in files.Where(s => LuaIndexer.classNames.Keys.Any(s.Contains)))
            {
                var key = LuaIndexer.classNames.Keys.First(file.Contains);
                if (packFiles.ContainsKey(key)) packFiles[key].Add(file);
                else packFiles.Add(key, new List<string> { file });
            }
        }

        private void ProcessFiles()
        {
            foreach (var (fileType, files) in packFiles)
            {
                List<string> rawLuaCode = new();
                foreach (var file in files)
                {
                    using StreamReader sr = new(file);
                    var raw = sr.ReadToEnd();
                    rawLuaCode.Add(raw);
                    names.Add(raw, file.Replace("\\", "/").Split('/')[^1]);
                    sr.Close();
                }

                rawLua.Add(fileType, rawLuaCode);
            }
        }

        public void ProcessLua()
        {
            foreach (var (type, list) in rawLua)
            foreach (var name in list.Select(item =>
                     {
                         var split = names[item].Split('.');
                         return $"{packName}.{split[^2]}.{split[0]}";
                     }))
            {
                if (processedLuaIds.ContainsKey(type)) processedLuaIds[type].Add(name);
                else processedLuaIds.Add(type, new List<string> { name });
            }
        }
    }
}