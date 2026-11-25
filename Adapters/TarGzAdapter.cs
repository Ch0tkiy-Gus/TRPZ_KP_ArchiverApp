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

        public void Compress(string sourceDirectory, string archivePath)
        {
            _tarGzLib.BuildTarGzip(sourceDirectory, archivePath, 6);
        }
    }
}