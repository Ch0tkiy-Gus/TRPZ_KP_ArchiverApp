using System.Diagnostics;

namespace ArchiverApp.Adapters
{
    public class LegacyTarGzLibrary
    {
        public void BuildTarGzip(string inputDir, string outputFileName, int compressionLevel)
        {
            Debug.WriteLine($"[LegacyTarGzLibrary] Пакування {inputDir} у {outputFileName} (рівень: {compressionLevel})");
        }
    }
}