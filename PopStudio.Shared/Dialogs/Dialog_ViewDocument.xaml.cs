using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using PopStudio.PlatformAPI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dialog_ViewDocument : Page, IDialogClosable
    {
        public bool CanClose { get; set; }

        public object Result { get; set; }

        public Func<Task<bool>> OnClose { get; set; }

        public Action OnCloseOver { get; set; }

        public async void InitDialog(params object[] args)
        {
            if (args.Length >= 1 && args[0] is YFFileSystem.YFFile yfFile)
            {
                _currentFile = yfFile;
                CurrentDirectoryTitle.Text = yfFile.Name;
                using (Stream stream = yfFile.OpenAsStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        _lastSavedString = await sr.ReadToEndAsync();
                    }
                }
            }
            else
            {
                _currentFile = null;
                _lastSavedString = string.Empty;
            }
            text.TextWrapping = TextWrapping.Wrap;
            text.AcceptsReturn = true;
            text.Text = _lastSavedString;
            OnClose += async () =>
            {
                if (_lastSavedString != text.Text)
                {
                    ContentDialog fileExistDialog = new ContentDialog
                    {
                        Title = "是否保存",
                        Content = "当前文件未保存，若直接关闭则将丢失更改，是否需要保存？",
                        PrimaryButtonText = "保存",
                        SecondaryButtonText = "丢弃",
                        CloseButtonText = "取消"
                    };
                    ContentDialogResult result = await fileExistDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        // 保存
                        if (_currentFile != null)
                        {
                            _lastSavedString = text.Text;
                            using (Stream stream = _currentFile.CreateAsStream())
                            {
                                using (StreamWriter sr = new StreamWriter(stream))
                                {
                                    await sr.WriteAsync(_lastSavedString);
                                }
                            }
                        }
                    }
                    else if (result == ContentDialogResult.Secondary)
                    {
                        // 丢弃
                        return CanClose = true;
                    }
                    else if (result == ContentDialogResult.None)
                    {
                        return CanClose = false;
                    }
                }
                return CanClose = true;
            };
        }

        private YFFileSystem.YFFile _currentFile;
        private string _lastSavedString;

        public Dialog_ViewDocument()
        {
            this.InitializeComponent();
        }

        private void Menu_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private async void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            if (_currentFile != null)
            {
                _lastSavedString = text.Text;
                using (Stream stream = _currentFile.CreateAsStream())
                {
                    using (StreamWriter sr = new StreamWriter(stream))
                    {
                        await sr.WriteAsync(_lastSavedString);
                    }
                }
            }
        }

        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            (this as IDialogClosable)?.Close();
        }
    }
}
