using System;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Windows;

namespace ArchiverApp
{
    class Program
    {
        [STAThread] 
        static void Main(string[] args)
        {
            string connectionString = "Data Source=archiver.db";
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                db.Open();
                var initCmd = db.CreateCommand();
                initCmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Archives (Id TEXT PRIMARY KEY, Name TEXT, Format TEXT, Checksum TEXT);
                    CREATE TABLE IF NOT EXISTS Metadata (ArchiveId TEXT PRIMARY KEY, CreatedAt DATETIME, CompressionLevel INTEGER, IsEncrypted INTEGER);
                    CREATE TABLE IF NOT EXISTS ArchiveItems (Id TEXT PRIMARY KEY, ArchiveId TEXT, InternalPath TEXT, IsDirectory INTEGER, OriginalSize INTEGER, PackedSize INTEGER);
                ";
                initCmd.ExecuteNonQuery();
            }

            var app = new Application();
            app.Run(new MainWindow());
        }
    }
}