using System;
using System.Windows;
using System.Windows.Controls;
using ArchiverApp.Models;
using ArchiverApp.Repositories;

namespace ArchiverApp
{
    public partial class CreateArchiveWindow : Window
    {
        private readonly SqliteArchiveRepository _repo;

        public CreateArchiveWindow(SqliteArchiveRepository repo)
        {
            InitializeComponent();
            _repo = repo;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var archive = new Archive
            {
                Name = NameTextBox.Text,
                Format = (FormatComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
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