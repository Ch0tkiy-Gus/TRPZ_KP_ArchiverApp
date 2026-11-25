using System;
using System.Windows;
using System.Windows.Controls;
using ArchiverApp.Models;
using ArchiverApp.Repositories;
using ArchiverApp.Strategies;
using ArchiverApp.Adapters;

namespace ArchiverApp
{
    public partial class CreateArchiveWindow : Window
    {
        private readonly SqliteArchiveRepository _repo;
        private ICompressionStrategy _compressionStrategy;

        public CreateArchiveWindow(SqliteArchiveRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var format = (FormatComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            switch (format)
            {
                case ".rar":
                    _compressionStrategy = new RarCompressionStrategy();
                    break;
                case ".tar.gz":
                    _compressionStrategy = new TarGzAdapter(new LegacyTarGzLibrary());
                    break;
                case ".ace":
                    _compressionStrategy = new AceAdapter(new LegacyAceLibrary());
                    break;
                case ".zip":
                default:
                    _compressionStrategy = new ZipCompressionStrategy();
                    break;
            }

            _compressionStrategy.Compress("data/source_folder", $"archive{format}");

            var archive = new Archive
            {
                Name = NameTextBox.Text,
                Format = format,
                Checksum = Guid.NewGuid().ToString().Substring(0, 8),
                Meta = new Metadata
                {
                    CompressionLevel = int.Parse(CompressionTextBox.Text),
                    IsEncrypted = false
                }
            };

            archive.Items.Add(new ArchiveItem
            {
                InternalPath = "data/test.txt",
                IsDirectory = false,
                OriginalSize = 1024,
                PackedSize = 512
            });

            _repo.Save(archive);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}