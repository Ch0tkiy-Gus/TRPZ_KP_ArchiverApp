using System.Collections.Generic;
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

        public void Compress(List<string> files, string archivePath)
        {
            _aceLib.PackToAce(string.Join(", ", files), archivePath);
        }
    }
}
// адаптер щоб ace працював через ICompressionStrategy