using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
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
        private List<string> _selectedFiles = new List<string>();

        public CreateArchiveWindow(SqliteArchiveRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        // вибір файлів для архіву
        private void SelectFiles_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = true,
                Title = "Оберіть файли для архіву"
            };

            if (dialog.ShowDialog() == true)
            {
                foreach (var file in dialog.FileNames)
                {
                    if (!_selectedFiles.Contains(file))
                    {
                        _selectedFiles.Add(file);
                        FilesListBox.Items.Add(Path.GetFileName(file));
                    }
                }
            }
        }
// перевірка полів і збереження архіву в бд
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введіть назву архіву.");
                return;
            }

            if (_selectedFiles.Count == 0)
            {
                MessageBox.Show("Оберіть хоча б один файл.");
                return;
            }

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

           // папка для збереження готових архівів
            string archivesDir = Path.Combine(Directory.GetCurrentDirectory(), "Archives");
            Directory.CreateDirectory(archivesDir);

            string archivePath = Path.Combine(archivesDir, $"{NameTextBox.Text}{format}");
            _compressionStrategy.Compress(_selectedFiles, archivePath);

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

            foreach (var filePath in _selectedFiles)
            {
                archive.Items.Add(new ArchiveItem
                {
                    InternalPath = Path.GetFileName(filePath),
                    IsDirectory = false,
                    OriginalSize = 1024,
                    PackedSize = 512
                });
            }

            _repo.Save(archive);
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}