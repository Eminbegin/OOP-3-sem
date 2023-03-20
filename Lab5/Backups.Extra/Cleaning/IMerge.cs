using Backups.Algorithms;
using Backups.Archivers;
using Backups.Entities;
using Backups.Repositories;

namespace Backups.Extra.Cleaning;

public interface IMerge : IResolver
{
    IRepository Repository { get; }
    IAlgorithm Algorithm { get; }
    IArchiver Archiver { get; }
}