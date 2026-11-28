using System.Formats.Tar;
using System.IO;
using System.IO.Compression;

namespace ArchiverApp.Adapters
{
    public class LegacyTarGzLibrary
    {
        public void BuildTarGzip(string[] files, string outputFile, int compressionLevel)
        {
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            using (var fileStream = File.Create(outputFile))
            using (var gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
            using (var tarWriter = new TarWriter(gzipStream))
            {
                foreach (var file in files)
                {
                    if (File.Exists(file))
                    {
                        tarWriter.WriteEntry(file, Path.GetFileName(file));
                    }
                }
            }
        }
    }
}
// стороння бібліотека для tar.gz