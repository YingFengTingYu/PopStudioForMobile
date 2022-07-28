using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Text;
using PopStudio.PlatformAPI;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.DataTransfer;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_FileExplorer : Page, IMenuChoosable
    {
        public Page_FileExplorer()
        {
            this.InitializeComponent();
            OnShow += Update;
        }

        bool CanClick = true;

        private async void ClickFileList(object sender, ItemClickEventArgs e)
        {
            if (!CanClick) return;
            if (e.ClickedItem is SingleFileItem info)
            {
                if (info.Kind == SingleFileItem.FileItemKind.File)
                {
                    CanClick = false;
                    ListView listView = new ListView();
                    listView.SelectionMode = ListViewSelectionMode.Single;
                    listView.Items.Add("导出");
                    listView.Items.Add("以文本文件形式编辑");
                    listView.Items.Add("复制名称");
                    listView.Items.Add("复制路径");
                    listView.Items.Add("查看信息");
                    listView.Items.Add("删除文件");
                    listView.Items.Add("重命名");
                    listView.Items.Add("剪切");
                    listView.Items.Add("复制");
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "请选择进行的操作",
                        Content = listView,
                        PrimaryButtonText = "确定",
                        CloseButtonText = "取消"
                    };
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        int mode = listView.SelectedIndex;
                        if (mode == 0)
                        {
                            // 导出
                            int index = info.Name.IndexOf('.');
                            string name;
                            string[] lst = new string[1];
                            if (index >= 0)
                            {
                                name = info.Name[..index];
                                lst[0] = info.Name[index..];
                            }
                            else
                            {
                                name = info.Name;
                                lst[0] = ".popstudio";
                            }
                            FileSavePicker fileSavePicker = new FileSavePicker();
                            fileSavePicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
                            fileSavePicker.SuggestedFileName = name;
                            fileSavePicker.FileTypeChoices.Add("File", lst);
                            fileSavePicker.FileTypeChoices.Add("PopCap Image Texture",
                                new string[]
                                {
                                    ".ptx"
                                });
                            fileSavePicker.FileTypeChoices.Add("Reflaction Object Notation",
                                new string[]
                                {
                                    ".rton"
                                });
                            fileSavePicker.FileTypeChoices.Add("Popcap Animate",
                                new string[]
                                {
                                    ".pam"
                                });
                            fileSavePicker.FileTypeChoices.Add("PNG Image",
                                new string[]
                                {
                                    ".png"
                                });
                            fileSavePicker.FileTypeChoices.Add("Text",
                                new string[]
                                {
                                    ".txt"
                                });
                            fileSavePicker.FileTypeChoices.Add("Javascript Object Notation",
                                new string[]
                                {
                                    ".json"
                                });
                            fileSavePicker.FileTypeChoices.Add("Zip File",
                                new string[]
                                {
                                    ".zip"
                                });
                            StorageFile saveFile = await fileSavePicker.PickSaveFileAsync();
                            if (saveFile != null)
                            {
                                await YFFileSystem.ExportFileAsync(CurrentDirectory.GetYFFile(info.Name), saveFile);
                            }
                        }
                        else if (mode == 1)
                        {
                            // 以文本文件形式编辑
                            YFFileSystem.YFFile m_file = CurrentDirectory.GetYFFile(info.Name);
                            if (m_file is not null)
                            {
                                await YFDialogHelper.OpenDialog<Dialogs.Dialog_ViewDocument>(m_file);
                            }
                        }
                        else if (mode == 2)
                        {
                            // 复制名称
                            DataPackage dataPackage = new DataPackage();
                            dataPackage.SetText(info.Name);
                            Clipboard.SetContent(dataPackage);
                        }
                        else if (mode == 3)
                        {
                            // 复制路径
                            DataPackage dataPackage = new DataPackage();
                            dataPackage.SetText(CurrentDirectory.GetPath() + "/" + info.Name);
                            Clipboard.SetContent(dataPackage);
                        }
                        else if (mode == 4)
                        {
                            TextBlock CreateCopyTextBlock(string format, string str)
                            {
                                TextBlock tblock = new TextBlock
                                {
                                    Text = string.Format(format, str),
                                    HorizontalAlignment = HorizontalAlignment.Stretch,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    TextWrapping = TextWrapping.Wrap
                                };
                                tblock.Tapped += CopyTextBlock_Tapped;
                                return tblock;
                            }
                            // 查看信息
                            YFFileSystem.YFFile m_file = CurrentDirectory.GetYFFile(info.Name);
                            // 获取文件名
                            string m4FileName = m_file.Name;
                            string m4FileExtension;
                            int index = m4FileName.IndexOf('.');
                            if (index >= 0)
                            {
                                m4FileExtension = m4FileName[index..];
                            }
                            else
                            {
                                m4FileExtension = string.Empty;
                            }
                            string m4FileSize = m_file.GetSizeAsString();
                            string m4FilePath = m_file.GetPath();
                            string m4NativePath = m_file.GetNativePath();
                            string m4CreateTime = m_file.Time.ToString();
                            StackPanel panel = new StackPanel();
                            panel.Children.Add(CreateCopyTextBlock("文件名：{0}", m4FileName));
                            panel.Children.Add(CreateCopyTextBlock("扩展名：{0}", m4FileExtension));
                            panel.Children.Add(CreateCopyTextBlock("文件大小：{0}", m4FileSize));
                            panel.Children.Add(CreateCopyTextBlock("文件路径：{0}", m4FilePath));
                            panel.Children.Add(CreateCopyTextBlock("本机路径：{0}", m4NativePath));
                            panel.Children.Add(CreateCopyTextBlock("创建时间：{0}", m4CreateTime));
                            ContentDialog createFileDialog = new ContentDialog
                            {
                                Title = "文件信息",
                                Content = panel,
                                CloseButtonText = "取消",
                                PrimaryButtonText = "确定"
                            };
                            await createFileDialog.ShowAsync();
                        }
                        else if (mode == 5)
                        {
                            // 删除文件
                            CurrentDirectory.DeleteYFFile(info.Name);
                            Update();
                        }
                        else if (mode == 6)
                        {
                            // 重命名
                            TextBlock textBlock = new TextBlock
                            {
                                Text = "请输入文件名"
                            };
                            TextBox textBox = new TextBox
                            {
                                Text = info.Name,
                                Width = 400
                            };
                            StackPanel panel = new StackPanel();
                            panel.Children.Add(textBlock);
                            panel.Children.Add(textBox);
                            ContentDialog createFileDialog = new ContentDialog
                            {
                                Title = "新建文件",
                                Content = panel,
                                CloseButtonText = "取消",
                                PrimaryButtonText = "确定"
                            };
                            ContentDialogResult result2 = await createFileDialog.ShowAsync();
                            if (result == ContentDialogResult.Primary)
                            {
                                CurrentDirectory.RenameYFFile(info.Name, textBox.Text);
                                Update();
                            }
                        }
                        else if (mode == 7)
                        {
                            // 剪贴
                            string newPath = await YFFileSystem.ChooseSaveFile(info.Name);
                            if (!string.IsNullOrEmpty(newPath))
                            {
                                YFFileSystem.MoveYFFileFromPath(CurrentDirectory.GetPath() + "/" + info.Name, newPath);
                                Update();
                            }
                        }
                        else if (mode == 8)
                        {
                            // 复制
                            string newPath = await YFFileSystem.ChooseSaveFile(info.Name);
                            if (!string.IsNullOrEmpty(newPath))
                            {
                                YFFileSystem.CopyYFFileFromPath(CurrentDirectory.GetPath() + "/" + info.Name, newPath);
                                Update();
                            }
                        }
                    }
                    CanClick = true;
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

        private void CopyTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is TextBlock t)
            {
                DataPackage dataPackage = new DataPackage();
                dataPackage.SetText(t.Text);
                Clipboard.SetContent(dataPackage);
            }
        }

        private YFFileSystem.YFDirectory CurrentDirectory = YFFileSystem.Home;

        private async void MenuCreateFile_Click(object sender, RoutedEventArgs e)
        {
            string defaultName = "新文件";
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
                Text = "请输入文件名"
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
                Title = "新建文件",
                Content = panel,
                CloseButtonText = "取消",
                PrimaryButtonText = "确定"
            };
            ContentDialogResult result = await createFileDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                string name = textBox.Text;
                if (CurrentDirectory.Exist(name))
                {
                    ContentDialog fileExistDialog = new ContentDialog
                    {
                        Title = "文件已存在",
                        Content = "创建文件失败，文件已存在",
                        CloseButtonText = "取消"
                    };
                    await fileExistDialog.ShowAsync();
                }
                else
                {
                    CurrentDirectory.CreateYFFile(name);
                    Update();
                }
            }
        }

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
                    await fileExistDialog.ShowAsync();
                }
                else
                {
                    CurrentDirectory.CreateYFDirectory(name);
                    Update();
                }
            }
        }

        private void MenuCompress_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog fileExistDialog = new ContentDialog
            {
                Title = "删除文件夹",
                Content = "被删除的文件夹和其中的内容无法恢复，确定要继续吗？",
                CloseButtonText = "取消",
                PrimaryButtonText = "确定"
            };
            ContentDialogResult result = await fileExistDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                CurrentDirectory = CurrentDirectory.DeleteSelf();
                Update();
            }
        }

        private async void MenuLoadFile_Click(object sender, RoutedEventArgs e)
        {
            var fileOpenPicker = new FileOpenPicker();
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

        private void MenuRefresh_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        public string Title { get; set; } = "文件浏览器";

        public Action OnShow { get; set; }

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
    }

    public class SingleFileItem
    {
        public string Name { get; private init; }
        public string ShowString { get; private init; }
        public FileItemKind Kind { get; private init; }
        public override string ToString() => ShowString;

        public enum FileItemKind
        {
            File,
            Folder,
            Back
        }

        public SingleFileItem(string name, bool isFolder)
        {
            Name = name;
            ShowString = (isFolder ? "\uD83D\uDCC1" : "\uD83D\uDCC4") + " " + Name;
            Kind = isFolder ? FileItemKind.Folder : FileItemKind.File;
        }

        public SingleFileItem()
        {
            Name = string.Empty;
            ShowString = "　返回上一级";
            Kind = FileItemKind.Back;
        }
    }
}
