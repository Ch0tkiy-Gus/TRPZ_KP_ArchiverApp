using System.Collections.Generic;
using System.Diagnostics;

namespace ArchiverApp.Strategies
{
    public class RarCompressionStrategy : ICompressionStrategy
    {
        public void Compress(List<string> files, string archivePath)
        {
            Debug.WriteLine($"[RAR] Пакування {files.Count} файлів у {archivePath}");
        }
    }
}
// пакування в rar (заглушка)