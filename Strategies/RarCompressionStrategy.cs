using System.Diagnostics;

namespace ArchiverApp.Strategies
{
    public class RarCompressionStrategy : ICompressionStrategy
    {
        public void Compress(string sourceDirectory, string archivePath)
        {
            Debug.WriteLine($"Пакування {sourceDirectory} у RAR архів {archivePath}");
        }
    }
}