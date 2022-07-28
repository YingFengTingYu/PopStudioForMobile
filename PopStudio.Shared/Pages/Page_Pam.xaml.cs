using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using PopStudio.PlatformAPI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_Pam : Page, IMenuChoosable
    {
        public string Title { get; set; } = YFString.GetString("Pam_Title");

        public Action OnShow { get; set; }

        public Page_Pam()
        {
            this.InitializeComponent();
            LoadFont();
        }

        void LoadFont()
        {
            label_introduction.Text = YFString.GetString("Pam_Introduction");
            label_choosemode.Text = YFString.GetString("Pam_ChooseMode");
            label_mode1.Text = YFString.GetString("Pam_DecodeMode");
            label_mode2.Text = YFString.GetString("Pam_EncodeMode");
            LoadFont_Checked(TB_Mode.IsOn);
            button1.Content = YFString.GetString("Pam_Choose");
            button2.Content = YFString.GetString("Pam_Choose");
            button_run.Content = YFString.GetString("Pam_Run");
            label_statue.Text = YFString.GetString("RunStatue_Title");
            text4.Text = YFString.GetString("RunStatue_Wait");
        }

        void LoadFont_Checked(bool v)
        {
            if (v)
            {
                text1.Text = YFString.GetString("Pam_EncodeChooseInFile");
                text2.Text = YFString.GetString("Pam_EncodeChooseOutFile");
            }
            else
            {
                text1.Text = YFString.GetString("Pam_DecodeChooseInFile");
                text2.Text = YFString.GetString("Pam_DecodeChooseOutFile");
            }
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            string path = await YFFileSystem.ChooseOpenFile();
            if (!string.IsNullOrEmpty(path))
            {
                textbox1.Text = path;
            }
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            string path = await YFFileSystem.ChooseSaveFile();
            if (!string.IsNullOrEmpty(path))
            {
                textbox2.Text = path;
            }
        }

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            button_run.IsEnabled = false;
            text4.Text = YFString.GetString("RunStatue_Run");
            bool mode = TB_Mode.IsOn == true;
            YFFileSystem.YFFile inFile = YFFileSystem.GetYFFileFromPath(textbox1.Text);
            YFFileSystem.YFFile outFile = YFFileSystem.CreateYFFileFromPath(textbox2.Text);
            YFThread.Invoke(() =>
            {
                string err = null;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                try
                {
                    if (mode)
                    {
                        YFAPI.EncodePam(inFile, outFile);
                    }
                    else
                    {
                        YFAPI.DecodePam(inFile, outFile);
                    }
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
                sw.Stop();
                decimal time = sw.ElapsedMilliseconds / 1000m;
                YFThread.InvokeOnMainThread(() =>
                {
                    if (err is null)
                    {
                        text4.Text = string.Format(YFString.GetString("RunStatue_Succeed"), time);
                    }
                    else
                    {
                        text4.Text = string.Format(YFString.GetString("RunStatue_Fail"), err);
                    }
                    button_run.IsEnabled = true;
                });
            });
        }

        private void TB_Mode_Toggled(object sender, RoutedEventArgs e)
        {
            LoadFont_Checked(TB_Mode.IsOn);
            (textbox1.Text, textbox2.Text) = (textbox2.Text, textbox1.Text);
        }
    }
}
