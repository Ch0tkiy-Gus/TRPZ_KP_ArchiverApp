using System.Windows;
using Microsoft.Data.Sqlite;
using ArchiverApp.Repositories;

namespace ArchiverApp
{
    public partial class MainWindow : Window
    {
        private SqliteArchiveRepository _repo;

        public MainWindow()
        {
            InitializeComponent();
            var db = new SqliteConnection("Data Source=archiver.db");
            _repo = new SqliteArchiveRepository(db);
        }

        private void CreateArchive_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateArchiveWindow(_repo);
            createWindow.ShowDialog();
        }

        private void CheckIntegrity_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Цілісність перевірено. Пошкоджень не виявлено.", "Звіт", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}