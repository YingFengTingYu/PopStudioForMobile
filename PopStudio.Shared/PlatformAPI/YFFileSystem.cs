using System;
using System.IO;
using System.Threading.Tasks;
using PopStudio.Plugin;
using System.Text.Json;
using PopStudio.SourceGen;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PopStudio.PlatformAPI
{
    public static class YFFileSystem
    {
        public class YFFile
        {
            public string Name { get => _name ??= "_"; set => _name = value.Replace("\\", "_").Replace("/", "_"); }
            public string RealFileName { get; set; }
            public DateTime Time { get; set; }
            [JsonIgnore]
            public YFDirectory Parent { get; set; }

            private string _name;

            public YFDirectory DeleteSelf()
            {
                Parent.DeleteYFFile(this);
                return Parent;
            }

            public string GetPath()
            {
                string str = "/" + this.Name;
                YFDirectory currentDirectory = this.Parent;
                while (!currentDirectory.IsRoot)
                {
                    str = "/" + currentDirectory.Name + str;
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

        public class YFDirectory
        {
            public string Name { get => _name ??= "_"; set => _name = value.Replace("\\", "_").Replace("/", "_"); }
            public List<YFDirectory> DirectoryMap { get => _directoryMap ??= new List<YFDirectory>(); set => _directoryMap = value; }
            public List<YFFile> FileMap { get => _fileMap ??= new List<YFFile>(); set => _fileMap = value; }
            public DateTime Time { get; set; }
            [JsonIgnore]
            public YFDirectory Parent { get; set; }
            public bool IsRoot => Parent == null;

            private string _name;
            private List<YFDirectory> _directoryMap;
            private List<YFFile> _fileMap;

            public bool Exist(string name) => FileExist(name) || DirectoryExist(name);

            public bool FileExist(string name) => GetYFFile(name) is not null;

            public bool DirectoryExist(string name) => GetYFDirectory(name) is not null;

            public YFFile GetYFFile(string name)
            {
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

            public void DeleteYFDirectory(string name)
            {
                DeleteYFDirectory(GetYFDirectory(name));
            }

            public void DeleteYFDirectory(YFDirectory yfDirectory)
            {
                if (yfDirectory?.Parent is not null)
                {
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

            public void DeleteYFFile(string name)
            {
                DeleteYFFile(GetYFFile(name));
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

            public YFFile CreateYFFile(string name)
            {
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

            public YFDirectory CreateYFDirectory(string name)
            {
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
                    "YFFileSystem.json");
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, Home, YFFileSystemSourceGenerationContext.Default.YFDirectory);
            }
        }

        public static void Init()
        {
            Home = null;
            string path = Path.Combine(
                    Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                    "YFFileSystem.json");
            if (File.Exists(path))
            {
                try
                {
                    Home ??= JsonSerializer.Deserialize(File.ReadAllText(path), YFFileSystemSourceGenerationContext.Default.YFDirectory);
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
            do
            {
                name = "PopStudioFileSystem(" + DateTime.Now.ToLongTimeString() + rnd.NextInt64() + ")";
            }
            while (RealNameList.Contains(name));
            return name;
        }

        public async static Task<string> ChooseOpenFile()
        {
            return await YFDialogHelper.OpenDialog<Dialogs.Dialog_ChooseOpenFile>() as string;
        }

        public async static Task<string> ChooseSaveFile()
        {
            return await YFDialogHelper.OpenDialog<Dialogs.Dialog_ChooseSaveFile>() as string;
        }

        public async static Task<string> ChooseFolder()
        {
            return await YFDialogHelper.OpenDialog<Dialogs.Dialog_ChooseFolder>() as string;
        }
    }
}
