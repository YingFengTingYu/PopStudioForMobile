using System;
using System.IO;
using System.Threading.Tasks;
using PopStudio.Plugin;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PopStudio.PlatformAPI
{
    public static class YFFileSystem
    {
        public static string NormalizeName(string value)
        {
            if (string.IsNullOrEmpty(value)) return "_";
            return value.Replace("\\", "_").Replace("/", "_");
        }

        public interface IFileDirectory
        {
            public string Name { get; set; }
            public DateTime Time { get; set; }
            public YFDirectory Parent { get; set; }
        }

        public class YFFile : IFileDirectory
        {
            [JsonPropertyName("name")]
            public string Name { get => _name ??= "_"; set => _name = NormalizeName(value); }

            [JsonPropertyName("native_name")]
            public string RealFileName { get; set; }

            [JsonPropertyName("create_time")]
            public DateTime Time { get; set; }

            [JsonIgnore]
            public YFDirectory Parent { get; set; }

            private string _name;

            public YFDirectory DeleteSelf()
            {
                Parent.DeleteYFFile(this);
                return Parent;
            }

            public bool Rename(string name)
            {
                return Parent.RenameYFFile(this, NormalizeName(name));
            }

            public string GetPath()
            {
                string str = "/" + this.Name;
                YFDirectory currentDirectory = this.Parent;
                while (!currentDirectory.IsRoot)
                {
                    str = "/" + currentDirectory.Name + str;
                    currentDirectory = currentDirectory.Parent;
                }
                str = "/home" + str;
                return str;
            }

            public string GetNativePath()
            {
                return Path.Combine(
                    Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                    RealFileName);
            }

            public Stream OpenAsStream()
            {
                return new FileStream(GetNativePath(), FileMode.OpenOrCreate);
            }

            public Stream CreateAsStream()
            {
                return new FileStream(GetNativePath(), FileMode.Create);
            }

            public BinaryStream OpenAsBinaryStream()
            {
                FileStream fs = new FileStream(GetNativePath(), FileMode.OpenOrCreate);
                return new BinaryStream(fs);
            }

            public BinaryStream CreateAsBinaryStream()
            {
                FileStream fs = new FileStream(GetNativePath(), FileMode.Create);
                return new BinaryStream(fs);
            }

            public long GetSizeAsLong()
            {
                FileInfo info = new FileInfo(this.GetNativePath());
                return info.Length;
            }

            public string GetSizeAsString()
            {
                double size = this.GetSizeAsLong();
                if (size <= 0)
                {
                    return "0B";
                }
                else if (size >= 1024 * 1024 * 1024 * 0.9D)
                {
                    return string.Format("{0:0.00}GB", size / (1024 * 1024 * 1024));
                }
                else if (size >= 1024 * 1024 * 0.9D)
                {
                    return string.Format("{0:0.00}MB", size / (1024 * 1024));
                }
                else if (size >= 1024 * 0.9D)
                {
                    return string.Format("{0:0.00}KB", size / 1024);
                }
                else
                {
                    return string.Format("{0:0}B", size);
                }
            }
        }

        public class YFDirectory : IFileDirectory
        {
            [JsonPropertyName("name")]
            public string Name { get => _name ??= "_"; set => _name = NormalizeName(value); }

            [JsonPropertyName("subfolders")]
            public List<YFDirectory> DirectoryMap { get => _directoryMap ??= new List<YFDirectory>(); set => _directoryMap = value; }

            [JsonPropertyName("subfiles")]
            public List<YFFile> FileMap { get => _fileMap ??= new List<YFFile>(); set => _fileMap = value; }

            [JsonPropertyName("create_time")]
            public DateTime Time { get; set; }

            [JsonIgnore]
            public YFDirectory Parent { get; set; }

            [JsonIgnore]
            public bool IsRoot => Parent == null;

            [JsonIgnore]
            public bool CanEnter { get; set; } = true;

            private string _name;
            private List<YFDirectory> _directoryMap;
            private List<YFFile> _fileMap;

            public bool Exist(string name)
            {
                name = NormalizeName(name);
                return FileExist(name) || DirectoryExist(name);
            }

            public bool FileExist(string name) => GetYFFile(NormalizeName(name)) is not null;

            public bool DirectoryExist(string name) => GetYFDirectory(NormalizeName(name)) is not null;

            public bool RenameYFFile(string oldName, string newName)
            {
                oldName = NormalizeName(oldName);
                newName = NormalizeName(newName);
                YFFile yfFile = GetYFFile(oldName);
                if (yfFile is null)
                {
                    return false;
                }
                if (!Exist(newName))
                {
                    yfFile.Name = newName;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool RenameYFDirectory(string oldName, string newName)
            {
                oldName = NormalizeName(oldName);
                newName = NormalizeName(newName);
                YFDirectory yfDirectory = GetYFDirectory(oldName);
                if (yfDirectory is null)
                {
                    return false;
                }
                if (!Exist(newName))
                {
                    yfDirectory.Name = newName;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool RenameYFFile(YFFile yfFile, string newName)
            {
                newName = NormalizeName(newName);
                if (!Exist(newName))
                {
                    yfFile.Name = newName;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool RenameYFDirectory(YFDirectory yfDirectory, string newName)
            {
                newName = NormalizeName(newName);
                if (!Exist(newName))
                {
                    yfDirectory.Name = newName;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public YFFile GetYFFile(string name)
            {
                name = NormalizeName(name);
                foreach (YFFile yfFile in this.FileMap)
                {
                    if (yfFile.Name == name)
                    {
                        return yfFile;
                    }
                }
                return null;
            }

            public YFDirectory GetYFDirectory(string name)
            {
                name = NormalizeName(name);
                foreach (YFDirectory yfFile in this.DirectoryMap)
                {
                    if (yfFile.Name == name)
                    {
                        return yfFile;
                    }
                }
                return null;
            }

            public YFDirectory DeleteSelf()
            {
                if (Parent is null)
                {
                    return this;
                }
                else
                {
                    Parent.DeleteYFDirectory(this);
                    return Parent;
                }
            }

            public async Task<YFDirectory> DeleteSelfAsync()
            {
                if (Parent is null)
                {
                    return this;
                }
                else
                {
                    await Parent.DeleteYFDirectoryAsync(this);
                    return Parent;
                }
            }

            public void DeleteYFDirectory(string name)
            {
                DeleteYFDirectory(GetYFDirectory(NormalizeName(name)));
            }

            public void DeleteYFDirectory(YFDirectory yfDirectory)
            {
                if (yfDirectory?.Parent is not null)
                {
                    yfDirectory.CanEnter = false;
                    if (yfDirectory.Parent == this)
                    {
                        // 删除文件
                        while (yfDirectory.FileMap.Count > 0)
                        {
                            yfDirectory.DeleteYFFile(yfDirectory.FileMap[0]);
                        }
                        // 删除文件夹
                        while (yfDirectory.DirectoryMap.Count > 0)
                        {
                            yfDirectory.DeleteYFDirectory(yfDirectory.DirectoryMap[0]);
                        }
                        DirectoryMap.Remove(yfDirectory);
                        Save();
                    }
                    else
                    {
                        yfDirectory.Parent.DeleteYFDirectory(yfDirectory);
                    }
                }
            }

            public async Task DeleteYFDirectoryAsync(string name)
            {
                await DeleteYFDirectoryAsync(GetYFDirectory(NormalizeName(name)));
            }

            public async Task DeleteYFDirectoryAsync(YFDirectory yfDirectory)
            {
                if (yfDirectory?.Parent is not null)
                {
                    yfDirectory.CanEnter = false;
                    if (yfDirectory.Parent == this)
                    {
                        // 删除文件
                        while (yfDirectory.FileMap.Count > 0)
                        {
                            await yfDirectory.DeleteYFFileAsync(yfDirectory.FileMap[0]);
                        }
                        // 删除文件夹
                        while (yfDirectory.DirectoryMap.Count > 0)
                        {
                            await yfDirectory.DeleteYFDirectoryAsync(yfDirectory.DirectoryMap[0]);
                        }
                        DirectoryMap.Remove(yfDirectory);
                        Save();
                    }
                    else
                    {
                        await yfDirectory.Parent.DeleteYFDirectoryAsync(yfDirectory);
                    }
                }
            }

            public void DeleteYFFile(string name)
            {
                DeleteYFFile(GetYFFile(NormalizeName(name)));
            }

            public void DeleteYFFile(YFFile yfFile)
            {
                if (yfFile is not null)
                {
                    if (yfFile.Parent == this)
                    {
                        File.Delete(yfFile.GetNativePath());
                        RealNameList.Remove(yfFile.RealFileName);
                        FileMap.Remove(yfFile);
                        Save();
                    }
                    else
                    {
                        yfFile.Parent.DeleteYFFile(yfFile);
                    }
                }
            }

            public async void DeleteYFFileAsync(string name)
            {
                await DeleteYFFileAsync(GetYFFile(NormalizeName(name)));
            }

            public async Task DeleteYFFileAsync(YFFile yfFile)
            {
                if (yfFile is not null)
                {
                    if (yfFile.Parent == this)
                    {
                        await Task.Run(() => File.Delete(yfFile.GetNativePath()));
                        RealNameList.Remove(yfFile.RealFileName);
                        FileMap.Remove(yfFile);
                        Save();
                    }
                    else
                    {
                        await yfFile.Parent.DeleteYFFileAsync(yfFile);
                    }
                }
            }

            public YFFile CreateYFFile(string name)
            {
                name = NormalizeName(name);
                if (this.DirectoryExist(name)) return null;
                YFFile yfFile = this.GetYFFile(name);
                if (yfFile is null)
                {
                    string newFileName = GetNewFileName();
                    RealNameList.Add(newFileName);
                    yfFile = new YFFile
                    {
                        Name = name,
                        RealFileName = newFileName,
                        Time = DateTime.Now,
                        Parent = this
                    };
                    this.FileMap.Add(yfFile);
                }
                using (FileStream fs = new FileStream(yfFile.GetNativePath(), FileMode.Create))
                {
                }
                Save();
                return yfFile;
            }

            public YFFile GetOrCreateYFFile(string name)
            {
                name = NormalizeName(name);
                if (this.DirectoryExist(name)) return null;
                YFFile yfFile = this.GetYFFile(name) ?? this.CreateYFFile(name);
                Save();
                return yfFile;
            }

            public YFDirectory CreateYFDirectory(string name)
            {
                name = NormalizeName(name);
                if (this.FileExist(name)) return null;
                YFDirectory yfDirectory = this.GetYFDirectory(name);
                if (yfDirectory is null)
                {
                    yfDirectory = new YFDirectory
                    {
                        Name = name,
                        Time = DateTime.Now,
                        Parent = this
                    };
                    this.DirectoryMap.Add(yfDirectory);
                }
                Save();
                return yfDirectory;
            }

            public string GetPath()
            {
                string str = string.Empty;
                YFDirectory currentDirectory = this;
                while (!currentDirectory.IsRoot)
                {
                    str = "/" + currentDirectory.Name + str;
                    currentDirectory = currentDirectory.Parent;
                }
                str = "/home" + str;
                return str;
            }

            public void AddAllName(List<string> list)
            {
                foreach (YFFile m_file in FileMap)
                {
                    list.Add(m_file.RealFileName);
                }
                foreach (YFDirectory directory in DirectoryMap)
                {
                    directory.AddAllName(list);
                }
            }

            public void SetParent()
            {
                foreach (YFFile m_file in FileMap)
                {
                    m_file.Parent = this;
                }
                foreach (YFDirectory yFDirectory in DirectoryMap)
                {
                    yFDirectory.Parent = this;
                    yFDirectory.SetParent();
                }
            }
        }

        public static YFDirectory Home;

        private static List<string> RealNameList;

        public static void Save()
        {
            string path = Path.Combine(
                    Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                    "PopStudioSetting(Type(YFDirectory)_Name(Home))");
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, Home, YFDirectoryJsonContext.Default.YFDirectory);
            }
        }

        public static void Init()
        {
            Home = null;
            string path = Path.Combine(
                    Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                    "PopStudioSetting(Type(YFDirectory)_Name(Home))");
            if (File.Exists(path))
            {
                try
                {
                    Home ??= JsonSerializer.Deserialize(File.ReadAllText(path), YFDirectoryJsonContext.Default.YFDirectory);
                    if (Home is not null)
                    {
                        Home.Name = "home";
                        // 赋值Parent
                        Home.SetParent();
                    }
                }
                catch (Exception)
                {

                }
            }
            Home ??= new YFDirectory
            {
                Name = "home",
                DirectoryMap = new List<YFDirectory>(),
                FileMap = new List<YFFile>(),
                Time = DateTime.Now,
                Parent = null
            };
            RealNameList = new List<string>();
            Home.AddAllName(RealNameList);
        }

        public static YFFile CreateYFFileFromPath(string path)
        {
            if (path.Length < 4) return null;
            path = path.Replace('\\', '/');
            if (path[0] == '/')
            {
                path = path[1..];
            }
            if (path[^1] == '/')
            {
                path = path[..^1];
            }
            int index = path.LastIndexOf('/');
            YFDirectory directory = CreateYFDirectoryFromPath(path[..index]);
            return directory?.CreateYFFile(path[(index + 1)..]);
        }

        public static YFFile GetOrCreateYFFileFromPath(string path)
        {
            if (path.Length < 4) return null;
            path = path.Replace('\\', '/');
            if (path[0] == '/')
            {
                path = path[1..];
            }
            if (path[^1] == '/')
            {
                path = path[..^1];
            }
            int index = path.LastIndexOf('/');
            YFDirectory directory = CreateYFDirectoryFromPath(path[..index]);
            return directory?.GetOrCreateYFFile(path[(index + 1)..]);
        }

        public static YFDirectory CreateYFDirectoryFromPath(string path)
        {
            if (path.Length < 4) return null;
            if (path[0] == '/' || path[0] == '\\')
            {
                path = path[1..];
            }
            if (path[^1] == '/' || path[^1] == '\\')
            {
                path = path[..^1];
            }
            string[] filenamelist = path.Split('/', '\\');
            int l = filenamelist.Length;
            if (filenamelist[0] != "home") return null;
            YFDirectory currentDirectory = Home;
            for (int i = 1; i < l; i++)
            {
                string nowName = filenamelist[i];
                if (nowName.Length < 1) return null;
                currentDirectory = currentDirectory?.CreateYFDirectory(nowName);
            }
            return currentDirectory;
        }

        public static void DeleteYFFileFromPath(string path)
        {
            GetYFFileFromPath(path)?.DeleteSelf();
        }

        public static void DeleteYFDirectoryFromPath(string path)
        {
            GetYFDirectoryFromPath(path)?.DeleteSelf();
        }

        public static YFFile GetYFFileFromPath(string path)
        {
            if (path.Length < 4) return null;
            if (path[0] == '/' || path[0] == '\\')
            {
                path = path[1..];
            }
            if (path[^1] == '/' || path[^1] == '\\')
            {
                path = path[..^1];
            }
            string[] filenamelist = path.Split('/', '\\');
            int l = filenamelist.Length - 1;
            if (filenamelist[0] != "home") return null;
            YFDirectory currentDirectory = Home;
            for (int i = 1; i < l; i++)
            {
                string nowName = filenamelist[i];
                if (nowName.Length < 1) return null;
                currentDirectory = currentDirectory.GetYFDirectory(nowName);
                if (currentDirectory == null)
                {
                    return null;
                }
            }
            return currentDirectory.GetYFFile(filenamelist[l]);
        }

        public static YFDirectory GetYFDirectoryFromPath(string path)
        {
            if (path.Length < 4) return null;
            if (path[0] == '/' || path[0] == '\\')
            {
                path = path[1..];
            }
            if (path[^1] == '/' || path[^1] == '\\')
            {
                path = path[..^1];
            }
            string[] filenamelist = path.Split('/', '\\');
            int l = filenamelist.Length;
            if (filenamelist[0] != "home") return null;
            YFDirectory currentDirectory = Home;
            for (int i = 1; i < l; i++)
            {
                string nowName = filenamelist[i];
                if (nowName.Length < 1) return null;
                currentDirectory = currentDirectory?.GetYFDirectory(nowName);
            }
            return currentDirectory;
        }

        public static bool CopyYFFileFromPath(string inPath, string outPath)
        {
            YFFile inFile = GetYFFileFromPath(inPath);
            if (inFile is not null)
            {
                YFFile outFile = GetOrCreateYFFileFromPath(outPath);
                if (outFile is not null)
                {
                    if (inFile.GetPath() != outFile.GetPath())
                    {
                        File.Copy(inFile.GetNativePath(), outFile.GetNativePath(), true);
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool MoveYFFileFromPath(string inPath, string outPath)
        {
            if (CopyYFFileFromPath(inPath, outPath))
            {
                DeleteYFFileFromPath(inPath);
                return true;
            }
            return false;
        }

        public static async Task<YFFile> ImportFileAsync(Windows.Storage.StorageFile m_file, YFDirectory directory, string name = null)
        {
            YFFile yfFile = directory.CreateYFFile(name ?? m_file.Name);
            using (Stream stream = await m_file.OpenStreamForReadAsync())
            {
                using (Stream fs = yfFile.OpenAsStream())
                {
                    await stream.CopyToAsync(fs);
                }
            }
            return yfFile;
        }

        public static async Task<YFDirectory> ImportDirectoryAsync(
            Windows.Storage.StorageFolder m_folder,
            YFDirectory directory,
            string name = null
            )
        {
            IFileDirectory ans = null;
            await foreach (IFileDirectory folder in ImportDirectoryAsyncEnumerable(m_folder, directory, name))
            {
                ans ??= folder;
            }
            return ans as YFDirectory;
        }

        public static async IAsyncEnumerable<IFileDirectory> ImportDirectoryAsyncEnumerable(
            Windows.Storage.StorageFolder m_folder,
            YFDirectory directory,
            string name = null
            )
        {
            YFDirectory yfDirectory = directory.CreateYFDirectory(name ?? m_folder.Name);
            yield return yfDirectory;
            foreach (Windows.Storage.StorageFile sfile in await m_folder.GetFilesAsync())
            {
                string nName = NormalizeName(sfile.Name);
                if (directory.DirectoryExist(nName))
                {
                    string name_filename, name_fileextension;
                    int index = nName.IndexOf('.');
                    if (index >= 0)
                    {
                        name_filename = nName[..index];
                        name_fileextension = nName[index..];
                    }
                    else
                    {
                        name_filename = nName;
                        name_fileextension = string.Empty;
                    }
                    int i = 2;
                    do
                    {
                        nName = name_filename + " (" + (i++) + ")" + name_fileextension;
                    }
                    while (yfDirectory.Exist(nName));
                }
                yield return await ImportFileAsync(sfile, yfDirectory, nName);
            }
            foreach (Windows.Storage.StorageFolder sfolder in await m_folder.GetFoldersAsync())
            {
                string nName = NormalizeName(sfolder.Name);
                if (directory.FileExist(nName))
                {
                    string name_filename = nName;
                    int i = 2;
                    do
                    {
                        nName = name_filename + " (" + (i++) + ")";
                    }
                    while (yfDirectory.Exist(nName));
                }
                await foreach (IFileDirectory fd in ImportDirectoryAsyncEnumerable(sfolder, yfDirectory, nName))
                {
                    yield return fd;
                }
            }
        }

        public static async Task ExportDirectoryAsync(YFDirectory yfDirectory, Windows.Storage.StorageFolder m_folder)
        {
            foreach (YFFile m_file in yfDirectory.FileMap)
            {
                await ExportFileAsync(m_file, await m_folder.CreateFileAsync(m_file.Name));
            }
            foreach (YFDirectory dir in yfDirectory.DirectoryMap)
            {
                await ExportDirectoryAsync(dir, await m_folder.CreateFolderAsync(dir.Name));
            }
        }

        public static async Task ExportFileAsync(YFFile yfFile, Windows.Storage.StorageFile m_file)
        {
            Windows.Storage.CachedFileManager.DeferUpdates(m_file);
            using (Stream stream = await m_file.OpenStreamForWriteAsync())
            {
                stream.SetLength(0);
                using (Stream fs = yfFile.OpenAsStream())
                {
                    await fs.CopyToAsync(stream);
                }
            }
            await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(m_file);
        }

        private static string GetNewFileName()
        {
            Random rnd = new Random();
            string name;
            DateTime time = DateTime.Now;
            do
            {
                name = $"PopStudioFileSystem(Time({time.Year}_{time.Month}_{time.Day}_{time.Hour}_{time.Minute}_{time.Second}_{time.Millisecond})_Random({rnd.NextInt64()}))";
            }
            while (RealNameList.Contains(name));
            return name;
        }

        public async static Task<string> ChooseOpenFile(params object[] args)
        {
            return await YFDialogHelper.OpenDialog<Dialogs.Dialog_ChooseOpenFile>(args) as string;
        }

        public async static Task<string> ChooseSaveFile(params object[] args)
        {
            return await YFDialogHelper.OpenDialog<Dialogs.Dialog_ChooseSaveFile>(args) as string;
        }

        public async static Task<string> ChooseFolder(params object[] args)
        {
            return await YFDialogHelper.OpenDialog<Dialogs.Dialog_ChooseFolder>(args) as string;
        }
    }
}
