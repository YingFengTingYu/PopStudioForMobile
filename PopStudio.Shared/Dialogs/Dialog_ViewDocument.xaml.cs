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
                    if (stream.Length < 1048576)
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            _lastSavedString = await sr.ReadToEndAsync();
                        }
                    }
                    else
                    {
                        OnClose += () => Task.FromResult(CanClose = true);
                        await Task.Delay(1000);
                        ContentDialog fileExistDialog = new ContentDialog
                        {
                            Title = YFString.GetString("Dialog_FileTooLarge"),
                            Content = string.Format(YFString.GetString("Dialog_FileTooLargeInfo"), 1),
                            CloseButtonText = YFString.GetString("Dialog_Close")
                        };
#if WinUI
                        fileExistDialog.XamlRoot = this.Content.XamlRoot;
#endif
                        await fileExistDialog.ShowAsync();
                        (this as IDialogClosable)?.Close();
                        return;
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
                        Title = YFString.GetString("Dialog_AskSaveTitle"),
                        Content = YFString.GetString("Dialog_AskSaveInfo"),
                        PrimaryButtonText = YFString.GetString("Dialog_Save"),
                        SecondaryButtonText = YFString.GetString("Dialog_Lose"),
                        CloseButtonText = YFString.GetString("Dialog_Cancel")
                    };
#if WinUI
                    fileExistDialog.XamlRoot = this.Content.XamlRoot;
#endif
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
            LoadFont();
        }

        void LoadFont()
        {
            flyout_save.Text = YFString.GetString("Dialog_Save");
            flyout_close.Text = YFString.GetString("Dialog_Close");
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
