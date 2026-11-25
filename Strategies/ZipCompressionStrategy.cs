using System.Diagnostics;

namespace ArchiverApp.Strategies
{
    public class ZipCompressionStrategy : ICompressionStrategy
    {
        public void Compress(string sourceDirectory, string archivePath)
        {
            Debug.WriteLine($"Пакування {sourceDirectory} у ZIP архів {archivePath}");
        }
    }
}