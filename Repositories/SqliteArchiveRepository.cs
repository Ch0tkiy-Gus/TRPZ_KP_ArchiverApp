using System.Data;
using Dapper;
using System.Linq;
using ArchiverApp.Models;

namespace ArchiverApp.Repositories
{
    public interface IArchiveRepository
    {
        void Save(Archive archive);
        Archive GetById(string id);
    }

    public class SqliteArchiveRepository : IArchiveRepository
    {
        private readonly IDbConnection _db;

        public SqliteArchiveRepository(IDbConnection db)
        {
            _db = db;
        }

        public void Save(Archive archive)
        {
            _db.Open();
            using var transaction = _db.BeginTransaction();
            try
            {
                string sqlArchive = "INSERT INTO Archives (Id, Name, Format, Checksum) VALUES (@Id, @Name, @Format, @Checksum)";
                _db.Execute(sqlArchive, archive, transaction);

                string sqlMeta = "INSERT INTO Metadata (ArchiveId, CreatedAt, CompressionLevel, IsEncrypted) VALUES (@ArchiveId, @CreatedAt, @CompressionLevel, @IsEncrypted)";
                _db.Execute(sqlMeta, new { ArchiveId = archive.Id, archive.Meta.CreatedAt, archive.Meta.CompressionLevel, archive.Meta.IsEncrypted }, transaction);

                if (archive.Items.Any())
                {
                    string sqlItems = "INSERT INTO ArchiveItems (Id, ArchiveId, InternalPath, IsDirectory, OriginalSize, PackedSize) VALUES (@Id, @ArchiveId, @InternalPath, @IsDirectory, @OriginalSize, @PackedSize)";
                    var itemsToInsert = archive.Items.Select(i => new { i.Id, ArchiveId = archive.Id, i.InternalPath, i.IsDirectory, i.OriginalSize, i.PackedSize });
                    _db.Execute(sqlItems, itemsToInsert, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                _db.Close();
            }
        }

        public Archive GetById(string id)
        {
            string sql = "SELECT * FROM Archives WHERE Id = @Id; SELECT * FROM Metadata WHERE ArchiveId = @Id; SELECT * FROM ArchiveItems WHERE ArchiveId = @Id;";
            using var multi = _db.QueryMultiple(sql, new { Id = id });
            
            var archive = multi.ReadSingleOrDefault<Archive>();
            if (archive != null)
            {
                archive.Meta = multi.ReadSingleOrDefault<Metadata>();
                archive.Items = multi.Read<ArchiveItem>().ToList();
            }
            return archive;
        }
    }
}