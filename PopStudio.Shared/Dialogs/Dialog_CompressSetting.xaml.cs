using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Windows.Storage.Pickers;
using Windows.Storage;
using PopStudio.PlatformAPI;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PopStudio.Pages;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dialog_CompressSetting : Page, IDialogClosable
    {
        public bool CanClose { get; set; }

        public object Result { get; set; }

        public Func<Task<bool>> OnClose { get; set; }

        public Action OnCloseOver { get; set; }

        public void InitDialog(params object[] args)
        {
            CB_CompressFormat.Items.Add(".zip");
            CB_CompressFormat.Items.Add(".tar");
            CB_CompressFormat.Items.Add(".tar.gz");
            CB_CompressFormat.Items.Add(".tar.bz2");
            CB_CompressFormat.Items.Add(".tar.lz");
            CB_CompressFormat.SelectedIndex = 0;
            CB_CompressFormat.SelectionChanged += (s, e) => UpdateFormat();
            OnClose += () => Task.FromResult(CanClose = true);
            if ((args?.Length ?? 0) >= 1)
            {
                NewFileName.Text = args[0] as string;
            }
            Update();
            UpdateFormat();
        }

        private void UpdateFormat()
        {
            string name = NewFileName.Text;
            int i = name.IndexOf('.');
            if (i > 0)
            {
                name = name[..i];
            }
            NewFileName.Text = name + CB_CompressFormat.SelectedIndex switch
            {
                0 => ".zip",
                1 => ".tar",
                2 => ".tgz",
                3 => ".tbz",
                _ => null
            };
        }

        public Dialog_CompressSetting()
        {
            this.InitializeComponent();
        }

        private void ClickFileList(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is SingleFileItem info)
            {
                if (info.Kind == SingleFileItem.FileItemKind.File)
                {
                    NewFileName.Text = info.Name;
                }
                else if (info.Kind == SingleFileItem.FileItemKind.Folder)
                {
                    CurrentDirectory = CurrentDirectory.GetYFDirectory(info.Name) ?? CurrentDirectory;
                    Update();
                }
                else if (info.Kind == SingleFileItem.FileItemKind.Back)
                {
                    CurrentDirectory = CurrentDirectory.Parent ?? CurrentDirectory;
                    Update();
                }
            }
        }

        private YFFileSystem.YFDirectory CurrentDirectory = YFFileSystem.Home;

        private async void MenuCreateDirectory_Click(object sender, RoutedEventArgs e)
        {
            string defaultName = "新建文件夹";
            if (CurrentDirectory.Exist(defaultName))
            {
                string n;
                int i = 2;
                do
                {
                    n = defaultName + " (" + (i++) + ")";
                }
                while (CurrentDirectory.Exist(n));
                defaultName = n;
            }
            TextBlock textBlock = new TextBlock
            {
                Text = "请输入文件夹名"
            };
            TextBox textBox = new TextBox
            {
                Text = defaultName
            };
            StackPanel panel = new StackPanel();
            panel.Children.Add(textBlock);
            panel.Children.Add(textBox);
            ContentDialog createFileDialog = new ContentDialog
            {
                Title = "新建文件夹",
                Content = panel,
                CloseButtonText = "取消",
                PrimaryButtonText = "确定"
            };
#if WinUI
            createFileDialog.XamlRoot = this.Content.XamlRoot;
#endif
            ContentDialogResult result = await createFileDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                string name = textBox.Text;
                if (CurrentDirectory.Exist(name))
                {
                    ContentDialog fileExistDialog = new ContentDialog
                    {
                        Title = "文件夹已存在",
                        Content = "创建文件夹失败，文件夹已存在",
                        CloseButtonText = "取消"
                    };
#if WinUI
                    fileExistDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    await fileExistDialog.ShowAsync();
                }
                else
                {
                    CurrentDirectory.CreateYFDirectory(name);
                    Update();
                }
            }
        }

        private async void MenuLoadFile_Click(object sender, RoutedEventArgs e)
        {
            var fileOpenPicker = new FileOpenPicker();
#if WinUI
            WinRT.Interop.InitializeWithWindow.Initialize(fileOpenPicker, WinUI.MainWindow.Handle);
#endif
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            fileOpenPicker.FileTypeFilter.Add("*");
            StorageFile pickedFile = await fileOpenPicker.PickSingleFileAsync();
            if (pickedFile != null)
            {
                string name = pickedFile.Name;
                int mode = 0;
                if (CurrentDirectory.FileExist(name)) mode |= 1;
                if (CurrentDirectory.DirectoryExist(name)) mode |= 2;
                bool create = true;
                if (mode != 0)
                {
                    ContentDialog fileExistDialog = new ContentDialog
                    {
                        Title = "文件已存在",
                        Content = "文件" + name + "已存在，请选择进行的操作",
                        CloseButtonText = "取消",
                        PrimaryButtonText = "重命名"
                    };
#if WinUI
                    fileExistDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    if ((mode & 2) == 0)
                    {
                        fileExistDialog.SecondaryButtonText = "覆盖";
                    }
                    ContentDialogResult result = await fileExistDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        string name_filename, name_fileextension;
                        int index = name.IndexOf('.');
                        if (index >= 0)
                        {
                            name_filename = name[..index];
                            name_fileextension = name[index..];
                        }
                        else
                        {
                            name_filename = name;
                            name_fileextension = string.Empty;
                        }
                        int i = 2;
                        do
                        {
                            name = name_filename + " (" + (i++) + ")" + name_fileextension;
                        }
                        while (CurrentDirectory.Exist(name));
                    }
                    else if (result == ContentDialogResult.Secondary)
                    {

                    }
                    else
                    {
                        create = false;
                    }
                }
                if (create)
                {
                    await YFFileSystem.ImportFileAsync(pickedFile, CurrentDirectory, name);
                    Update();
                }
            }
        }

        private void Menu_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        public void Update()
        {
            FileList.Clear();
            if (!CurrentDirectory.IsRoot)
            {
                FileList.Add(new SingleFileItem());
            }
            List<YFFileSystem.YFDirectory> directoryList = CurrentDirectory.DirectoryMap.ToList();
            directoryList.Sort((a, b) => String.Compare(a.Name, b.Name));
            foreach (YFFileSystem.YFDirectory directory in directoryList)
            {
                FileList.Add(new SingleFileItem(directory.Name, true));
            }
            List<YFFileSystem.YFFile> fileList = CurrentDirectory.FileMap.ToList();
            fileList.Sort((a, b) => String.Compare(a.Name, b.Name));
            foreach (YFFileSystem.YFFile m_file in fileList)
            {
                FileList.Add(new SingleFileItem(m_file.Name, false));
            }
            CurrentDirectoryTitle.Text = CurrentDirectory.GetPath();
        }

        private ObservableCollection<SingleFileItem> _fileList;

        public ObservableCollection<SingleFileItem> FileList => _fileList ??= new ObservableCollection<SingleFileItem>();

        private async void CurrentDirectoryTitle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = "请输入路径"
            };
            TextBox textBox = new TextBox
            {
                Text = CurrentDirectory.GetPath()
            };
            StackPanel panel = new StackPanel();
            panel.Children.Add(textBlock);
            panel.Children.Add(textBox);
            ContentDialog createFileDialog = new ContentDialog
            {
                Title = "更改路径",
                Content = panel,
                CloseButtonText = "取消",
                PrimaryButtonText = "确定"
            };
#if WinUI
            createFileDialog.XamlRoot = this.Content.XamlRoot;
#endif
            ContentDialogResult result = await createFileDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                YFFileSystem.YFDirectory directory = YFFileSystem.GetYFDirectoryFromPath(textBox.Text);
                if (directory is not null)
                {
                    CurrentDirectory = directory;
                    Update();
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string enterredName = NewFileName.Text;
            if (enterredName.Length < 1)
            {
                Result = null;
            }
            else
            {
                enterredName = YFFileSystem.NormalizeName(enterredName);
                if (CurrentDirectory.DirectoryExist(enterredName))
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "文件夹已存在",
                        Content = "有同名文件夹存在，无法创建文件！",
                        CloseButtonText = "确定"
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    await noWifiDialog.ShowAsync();
                }
                else if (CurrentDirectory.FileExist(enterredName))
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "文件已存在",
                        Content = "文件已存在，是否覆盖？",
                        PrimaryButtonText = "确定",
                        CloseButtonText = "取消"
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        Result = new CompressSetting(CurrentDirectory.GetPath() + "/" + enterredName, (CompressionFormat)CB_CompressFormat.SelectedIndex);
                    }
                    else
                    {
                        Result = null;
                    }
                }
                else
                {
                    Result = new CompressSetting(CurrentDirectory.GetPath() + "/" + enterredName, (CompressionFormat)CB_CompressFormat.SelectedIndex);
                }
            }
            (this as IDialogClosable)?.Close();
        }

        public enum CompressionFormat
        {
            Zip,
            Tar,
            Tgz,
            Tbz
        }

        public class CompressSetting
        {
            public string Path { get; set; }
            public CompressionFormat Format { get; set; }

            public CompressSetting(string name, CompressionFormat format)
            {
                Path = name;
                Format = format;
            }
        }
    }
}
