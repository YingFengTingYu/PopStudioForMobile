using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using PopStudio.PlatformAPI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dialog_ViewImage : Page, IDialogClosable
    {
        public bool CanClose { get; set; }

        public object Result { get; set; }

        public Func<Task<bool>> OnClose { get; set; }

        public Action OnCloseOver { get; set; }

        public async void InitDialog(params object[] args)
        {
            if (args.Length >= 1 && args[0] is YFFileSystem.YFFile yfFile)
            {
                CurrentDirectoryTitle.Text = yfFile.Name;
                using (Stream stream = yfFile.OpenAsStream())
                {
                    using (Image.YFBitmap bitmap = Image.YFBitmap.Create(stream))
                    {
                        int totalSize = bitmap.Width * bitmap.Height * 4;
                        if (totalSize <= 67108864)
                        {
                            WriteableBitmap m = new WriteableBitmap(bitmap.Width, bitmap.Height);
                            const int bufferSize = 81920;
                            byte[] m_buffer = new byte[bufferSize];
                            int fullTime = totalSize / bufferSize;
                            int addsLength = totalSize % bufferSize;
                            nint pointer = bitmap.Pixels;
                            uint destPointer = 0;
                            for (uint i = 0; i < fullTime; i++)
                            {
                                Marshal.Copy(pointer, m_buffer, 0, bufferSize);
                                m_buffer.CopyTo(0, m.PixelBuffer, destPointer, bufferSize);
                                pointer += bufferSize;
                                destPointer += bufferSize;
                            }
                            if (addsLength != 0)
                            {
                                Marshal.Copy(pointer, m_buffer, 0, addsLength);
                                m_buffer.CopyTo(0, m.PixelBuffer, destPointer, addsLength);
                            }
                            viewimage.Source = m;
                        }
                        else
                        {
                            OnClose += () => Task.FromResult(CanClose = true);
                            await Task.Delay(1000);
                            ContentDialog fileExistDialog = new ContentDialog
                            {
                                Title = YFString.GetString("Dialog_FileTooLarge"),
                                Content = string.Format(YFString.GetString("Dialog_FileTooLargeInfo"), 64),
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
            }
            OnClose += () => Task.FromResult(CanClose = true);
        }

        public Dialog_ViewImage()
        {
            this.InitializeComponent();
            LoadFont();
        }

        void LoadFont()
        {
            flyout_close.Text = YFString.GetString("Dialog_Close");
        }

        private void Menu_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            (this as IDialogClosable)?.Close();
        }
    }
}
