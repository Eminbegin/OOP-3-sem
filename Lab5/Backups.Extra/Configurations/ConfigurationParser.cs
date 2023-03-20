// using Backups.Algorithms;
// using Backups.Archivers;
// using Backups.Entities;
// using Backups.Extra.Entities;
// using Backups.Extra.Logging;
// using Backups.Repositories;
// using Microsoft.Extensions.Configuration;
// using Zio.FileSystems;
//
// namespace Backups.Extra.Configurations;
//
// public class ConfigurationParser
// {
//     private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
//         .AddJsonFile("appsettings.json", optional: true)
//         .Build();
//
//     public static IAlgorithm SelectAlgorithm()
//     {
//         return Configuration.GetSection("Algorithm").Value switch
//         {
//             "SingleAlgorithm" => new SingleAlgorithm(),
//             "SplitAlgorithm" => new SplitAlgorithm(),
//             _ => throw new Exception()
//         };
//     }
//
//     public static IArchiver SelectArchiver()
//     {
//         return Configuration.GetSection("Archiver").Value switch
//         {
//             "ZipArchiver" => new ZipArchiver("zip"),
//             _ => throw new Exception()
//         };
//     }
//
//     public static IBackup SelectBackup()
//     {
//         switch (Configuration.GetSection("Backup").GetSection("Type").Value)
//         {
//             case "Simple":
//                 return new SimpleBackup();
//             case "Kal":
//                 return new ExtraBackup();
//             default:
//                 throw new Exception();
//         }
//     }
//
//     public static ILogger SelectLogger()
//     {
//         switch (Configuration.GetSection("Logger").GetSection("Type").Value)
//         {
//             case "Console":
//                 return new ConsoleLogger();
//             default:
//                 throw new Exception();
//         }
//     }
//
//     public static IRepository SelectRepository()
//     {
//         switch (Configuration.GetSection("Repository").GetSection("Type").Value)
//         {
//             case "FileSystem":
//                 string? mainPath = Configuration.GetSection("Repository").GetSection("Path").Value;
//                 ArgumentNullException.ThrowIfNull(mainPath, "no path in configuration");
//                 return new FileSystemRepository(mainPath);
//             case "InMemory":
//                 return new InMemoryRepository(new MemoryFileSystem());
//             default:
//                 throw new Exception();
//         }
//     }
//
//     public static string SelectName()
//     {
//         string? name = Configuration.GetSection("Name").Value;
//         ArgumentNullException.ThrowIfNull(name, "name != null");
//         return name;
//     }
// }
