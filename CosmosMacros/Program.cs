using System.IO.Compression;

namespace CosmosMacros {
    internal class Program {
        static void Main(string[] args) {
            if (args.Length != 2) {
                Console.WriteLine("Usage: CosmosMacros.exe <path to directory> <apply/restore>");
                return;
            }

            if (!Directory.Exists(args[0])) {
                Console.WriteLine($"Directory {args[0]} does not exist");
                return;
            }

            if (args[1] == "apply") {
                Dictionary<string, BackupDir> backupTable = RecursiveCreateBackupTable(args[0]);

                if (!Directory.Exists(Path.Join(args[0], "backup")))
                    Directory.CreateDirectory(Path.Join(args[0], "backup"));
                
                using (ZipArchive archive = ZipFile.Open(Path.Join(args[0], "backup/backup-" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".zip"), ZipArchiveMode.Create)) {
                    RecursiveCreateBackup(archive, backupTable, args[0]);
                }

                Console.WriteLine("Backup created incase restore fails");

                RecursiveMacroReplace(args[0]);
            } else if (args[1] == "restore") {
                RecursiveRestore(args[0]);
            } else {
                Console.WriteLine("Usage: CosmosMacros.exe <path to directory> <apply/restore>");
            }
        }

        static Dictionary<string, BackupDir> RecursiveCreateBackupTable(string path) {
            var dirs = new Dictionary<string, BackupDir>();
            var dir = new BackupDir();
            dir.Path = path;
            dir.Files = Directory.GetFiles(path, "*.cs");
            dir.SubDirs = new BackupDir[0];
            dirs.Add(path, dir);

            foreach (var subDir in Directory.GetDirectories(path)) {
                var subDirs = RecursiveCreateBackupTable(subDir);
                foreach (var subDirEntry in subDirs) {
                    dirs.Add(subDirEntry.Key, subDirEntry.Value);
                }
            }

            return dirs;
        }

        static void RecursiveCreateBackup(ZipArchive archive, Dictionary<string, BackupDir> backupTable, string path) {
            var dir = backupTable[path];
            foreach (var file in dir.Files) {
                archive.CreateEntryFromFile(file, file);
            }

            foreach (var subDir in dir.SubDirs) {
                RecursiveCreateBackup(archive, backupTable, subDir.Path);
            }
        }

        static void RecursiveMacroReplace(string dir) {
            foreach (var file in Directory.GetFiles(dir, "*.cs")) {
                if (File.Exists(file + ".restore")) {
                    Console.WriteLine($"Skipping {file} as it has already been processed - you may need to restore");
                    return;
                }
                
                File.Copy(file, file + ".restore");
                CSharpMacros.Program.Main(new string[] { file });
            }

            foreach (var subDir in Directory.GetDirectories(dir)) {
                RecursiveMacroReplace(subDir);
            }
        }

        static void RecursiveRestore(string dir) {
            foreach (var file in Directory.GetFiles(dir, "*.cs")) {
                if (File.Exists(file)) {
                    if (!File.Exists(file + ".restore")) {
                        Console.WriteLine($"Skipping {file} as it has already been restored or was not processed");
                        continue;
                    }

                    File.Delete(file);
                }
                
                File.Copy(file + ".restore", file);
                File.Delete(file + ".restore");
            }

            foreach (var subDir in Directory.GetDirectories(dir)) {
                RecursiveRestore(subDir);
            }
        }
    }

    struct BackupDir {
        public string Path;
        public string[] Files;
        public BackupDir[] SubDirs;
    }
}