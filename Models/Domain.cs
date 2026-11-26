using System;
using System.Collections.Generic;

namespace ArchiverApp.Models
{
    // основна модель архіву
    public class Archive
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Format { get; set; }
        public string Checksum { get; set; }
        
        public Metadata Meta { get; set; }
        public List<ArchiveItem> Items { get; set; } = new List<ArchiveItem>();
    }
// метадані архіву (рівень стиснення, шифрування)
    public class Metadata
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CompressionLevel { get; set; }
        public bool IsEncrypted { get; set; }
    }
// окремий файл або папка всередині архіву
    public class ArchiveItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string InternalPath { get; set; }
        public bool IsDirectory { get; set; }
        public long OriginalSize { get; set; }
        public long PackedSize { get; set; }
    }
}