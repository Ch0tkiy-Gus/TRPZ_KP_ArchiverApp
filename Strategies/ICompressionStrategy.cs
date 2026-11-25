namespace ArchiverApp.Strategies
{
    public interface ICompressionStrategy
    {
        void Compress(string sourceDirectory, string archivePath);
    }
}