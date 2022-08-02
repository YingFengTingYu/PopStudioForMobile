using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PopStudio.Dialogs;
using PopStudio.Pages;
using PopStudio.PlatformAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PopStudio
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Singleton;

        public MainPage()
        {
            Singleton = this;
            this.InitializeComponent();
            page_menu.InitPage();
        }

        public void LoadPage(string title, Page page)
        {
            PageTitle.Text = title;
            PageControl.Content = page;
            ChangeShellState(true);
        }

        public void LoadPage(string title, Page page, bool change)
        {
            PageTitle.Text = title;
            PageControl.Content = page;
            if (change)
            {
                ChangeShellState(true);
            }
        }

        private List<bool> asyncLoad = new List<bool> { false };

        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await App.InitPlarform;
            ChangeShellState();
        }

        public void BeginDialog(IDialogClosable p)
        {
            if (p is Page page)
            {
                YFDialogControl.Content = page;
            }
            YFDialogGrid.Visibility = Visibility.Visible;
            YFDialongCancel.Content = YFString.GetString("MainPage_Close");
            YFDialongCancel.Visibility = Visibility.Visible;
        }

        public void BeginDialog(IDialogClosable p, bool v)
        {
            if (p is Page page)
            {
                YFDialogControl.Content = page;
            }
            YFDialogGrid.Visibility = Visibility.Visible;
            YFDialongCancel.Content = YFString.GetString("MainPage_Close");
            YFDialongCancel.Visibility = v ? Visibility.Visible : Visibility.Collapsed;
        }

        public void EndDialog()
        {
            YFDialogControl.Content = null;
            YFDialogGrid.Visibility = Visibility.Collapsed;
            YFDialongCancel.Visibility = Visibility.Collapsed;
        }

        public async void ChangeShellState(bool? state = null)
        {
            if (state ?? menu.Visibility == Visibility.Visible)
            {
                menu.Opacity = 1;
                for (int i = 7; i > 0; i--)
                {
                    await Task.Delay(10);
                    menu.Opacity = i / 7d;
                }
                menu.Visibility = Visibility.Collapsed;
            }
            else
            {
                menu.Visibility = Visibility.Visible;
                menu.Opacity = 0;
                for (int i = 1; i <= 7; i++)
                {
                    await Task.Delay(10);
                    menu.Opacity = i / 7d;
                }
            }
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ChangeShellState();
        }

        private async void YFDialongCancel_Click(object sender, RoutedEventArgs e)
        {
            if (YFDialogControl.Content is IDialogClosable dialog)
            {
                dialog.Result = null;
                await dialog.Close();
            }
        }

        private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
