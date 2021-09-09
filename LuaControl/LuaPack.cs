using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenRpg
{
    public class LuaPack
    {
        public string corePath;
        public string packName;
        public Dictionary<string, List<string>> packFiles = new();
        public Dictionary<string, List<string>> rawLua = new();

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
                    rawLuaCode.Add(sr.ReadToEnd());
                    sr.Close();
                }

                rawLua.Add(fileType, rawLuaCode);
            }
        }
    }
}