using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace ArchiverApp.Strategies
{
    // пакування в zip
    public class ZipCompressionStrategy : ICompressionStrategy
    {
        public void Compress(List<string> files, string archivePath)
        {
            if (File.Exists(archivePath))
            {
                File.Delete(archivePath);
            }

            using (var zip = ZipFile.Open(archivePath, ZipArchiveMode.Create))
            {
                foreach (var file in files)
                {
                    if (File.Exists(file))
                    {
                        zip.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }
            }
        }
    }
}