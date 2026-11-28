using System.Collections.Generic;
using ArchiverApp.Strategies;

namespace ArchiverApp.Adapters
{
    public class TarGzAdapter : ICompressionStrategy
    {
        private readonly LegacyTarGzLibrary _tarGzLib;

        public TarGzAdapter(LegacyTarGzLibrary tarGzLib)
        {
            _tarGzLib = tarGzLib;
        }

        public void Compress(List<string> files, string archivePath)
        {
            _tarGzLib.BuildTarGzip(files.ToArray(), archivePath, 6);
        }
    }
}
// адаптер щоб tar.gz працював через ICompressionStrategy