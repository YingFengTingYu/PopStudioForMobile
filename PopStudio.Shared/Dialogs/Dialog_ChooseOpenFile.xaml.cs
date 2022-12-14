using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Windows.Storage;
using PopStudio.PlatformAPI;
using PopStudio.Pages;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dialog_ChooseOpenFile : Page, IDialogClosable
    {
        public bool CanClose { get; set; }

        public object Result { get; set; }

        public Func<Task<bool>> OnClose { get; set; }

        public Action OnCloseOver { get; set; }

        public void InitDialog(params object[] args)
        {
            OnClose += () => Task.FromResult(CanClose = true);
            Update();
            LoadFont();
        }

        void LoadFont()
        {
            flyout_createfolder.Text = YFString.GetString("FileExplorer_CreateFolder");
            flyout_importfile.Text = YFString.GetString("FileExplorer_ImportFile");
        }

        public Dialog_ChooseOpenFile()
        {
            this.InitializeComponent();
        }

        private void ClickFileList(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is SingleFileItem info)
            {
                if (info.Kind == SingleFileItem.FileItemKind.File)
                {
                    Result = CurrentDirectory.GetPath() + "/" + info.Name;
                    (this as IDialogClosable)?.Close();
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
            try
            {
                string defaultName = YFString.GetString("FileExplorer_NewFolder");
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
                    Text = YFString.GetString("FileExplorer_EnterFolderName")
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
                    Title = YFString.GetString("FileExplorer_NewFolder"),
                    Content = panel,
                    CloseButtonText = YFString.GetString("FileExplorer_Cancel"),
                    PrimaryButtonText = YFString.GetString("FileExplorer_OK")
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
                            Title = YFString.GetString("FileExplorer_FolderExist"),
                            Content = YFString.GetString("FileExplorer_FolderExistInfo"),
                            CloseButtonText = YFString.GetString("FileExplorer_Cancel")
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
            catch (Exception)
            {

            }
        }

        private async void MenuLoadFile_Click(object sender, RoutedEventArgs e)
        {
            StorageFile pickedFile = await YFNativeFilePicker.PickOpenFileAsync();
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
                        Title = YFString.GetString("FileExplorer_CannotCreate"),
                        Content = string.Format(YFString.GetString("FileExplorer_CannotCreateInfo"), name),
                        CloseButtonText = YFString.GetString("FileExplorer_Cancel"),
                        PrimaryButtonText = YFString.GetString("FileExplorer_Rename")
                    };
                    if ((mode & 2) == 0)
                    {
                        fileExistDialog.SecondaryButtonText = YFString.GetString("FileExplorer_Overwrite");
                    }
#if WinUI
                    fileExistDialog.XamlRoot = this.Content.XamlRoot;
#endif
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
                Text = YFString.GetString("FileExplorer_EnterPath")
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
                Title = YFString.GetString("FileExplorer_ChangePath"),
                Content = panel,
                CloseButtonText = YFString.GetString("FileExplorer_Cancel"),
                PrimaryButtonText = YFString.GetString("FileExplorer_OK")
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
    }
}
