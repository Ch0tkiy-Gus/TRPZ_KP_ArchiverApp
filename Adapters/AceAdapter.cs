using ArchiverApp.Strategies;

namespace ArchiverApp.Adapters
{
    public class AceAdapter : ICompressionStrategy
    {
        private readonly LegacyAceLibrary _aceLib;

        public AceAdapter(LegacyAceLibrary aceLib)
        {
            _aceLib = aceLib;
        }

        public void Compress(string sourceDirectory, string archivePath)
        {
            _aceLib.PackToAce(sourceDirectory, archivePath);
        }
    }
}