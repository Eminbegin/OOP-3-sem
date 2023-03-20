using Backups.Entities;
using Backups.RepositoryItems;

namespace Backups.Repositories;

public interface IRepository
{
    string Separator { get; }
    Stream GetStream(string fileName);
    Stream GetStream(string folderPath, string archiveName);
    IRepositoryItem GetRepositoryItem(IBackupItem backupItem);
    bool IsItemExists(string path);
    void DeleteItem(string path);
    void CreateFolder(string name);
    void SaveLog(string path, string log);
}