using System.Collections.Generic;

namespace ArchiverApp.Strategies
{
    // спільний інтерфейс для всіх форматів архівів
    public interface ICompressionStrategy
    {
        void Compress(List<string> files, string archivePath);
    }
}