using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using PopStudio.PlatformAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_Package : Page, IMenuChoosable
    {
        public string Title { get; set; } = YFString.GetString("Package_Title");

        public static string StaticTitle { get; set; } = YFString.GetString("Package_Title");

        public Action OnShow { get; set; }

        public Page_Package()
        {
            this.InitializeComponent();
            LoadFont();
        }

        void LoadFont()
        {
            label_introduction.Text = YFString.GetString("Package_Introduction");
            label_choosemode.Text = YFString.GetString("Package_ChooseMode");
            label_mode1.Text = YFString.GetString("Package_DecodeMode");
            label_mode2.Text = YFString.GetString("Package_EncodeMode");
            LoadFont_Checked(TB_Mode.IsOn);
            button1.Content = YFString.GetString("Package_Choose");
            button2.Content = YFString.GetString("Package_Choose");
            CB_CMode.Items.Clear();
            CB_CMode.Items.Add(YFString.GetString("Package_ModeRsb"));
            CB_CMode.Items.Add(YFString.GetString("Package_ModeDz"));
            CB_CMode.Items.Add(YFString.GetString("Package_ModePak"));
            CB_CMode.Items.Add(YFString.GetString("Package_ModeArcv"));
            CB_CMode.SelectedIndex = 0;
            button_run.Content = YFString.GetString("Package_Run");
            label_statue.Text = YFString.GetString("RunStatue_Title");
            text4.Text = YFString.GetString("RunStatue_Wait");
        }

        void LoadFont_Checked(bool v)
        {
            if (v)
            {
                text1.Text = YFString.GetString("Package_EncodeChooseInFile");
                text2.Text = YFString.GetString("Package_EncodeChooseOutFile");
                text3.Text = YFString.GetString("Package_EncodeChooseMode");
            }
            else
            {
                text1.Text = YFString.GetString("Package_DecodeChooseInFile");
                text2.Text = YFString.GetString("Package_DecodeChooseOutFile");
                text3.Text = YFString.GetString("Package_DecodeChooseMode");
            }
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            string path = TB_Mode.IsOn ? await YFFileSystem.ChooseFolder() : await YFFileSystem.ChooseOpenFile();
            if (!string.IsNullOrEmpty(path))
            {
                textbox1.Text = path;
            }
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            string path = TB_Mode.IsOn ? await YFFileSystem.ChooseSaveFile() : await YFFileSystem.ChooseFolder();
            if (!string.IsNullOrEmpty(path))
            {
                textbox2.Text = path;
            }
        }

        private async void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            button_run.IsEnabled = false;
            text4.Text = YFString.GetString("RunStatue_Run");
            bool mode = TB_Mode.IsOn;
            int cmode = CB_CMode.SelectedIndex;
            string inData = textbox1.Text;
            string outData = textbox2.Text;
            string err = null;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                if (mode)
                {
                    YFFileSystem.YFDirectory inFolder =
                        YFFileSystem.GetYFDirectoryFromPath(inData);
                    YFFileSystem.YFFile outFile =
                        YFFileSystem.CreateYFFileFromPath(outData)
                        ?? YFFileSystem.CreateYFDirectoryFromPath(outData)
                        .CreateYFFile(inFolder.Name + cmode switch
                        {
                            0 => ".rsb",
                            1 => ".dz",
                            2 => ".rsb",
                            3 => ".bin",
                            _ => null
                        });
                    await Task.Run(() => YFAPI.Pack(inFolder, outFile, cmode));
                }
                else
                {
                    YFFileSystem.YFFile inFile =
                        YFFileSystem.GetYFFileFromPath(inData);
                    YFFileSystem.YFDirectory outFolder =
                        YFFileSystem.CreateYFDirectoryFromPath(outData);
                    await Task.Run(() => YFAPI.Unpack(inFile, outFolder, cmode));
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            sw.Stop();
            decimal time = sw.ElapsedMilliseconds / 1000m;
            if (err is null)
            {
                text4.Text = string.Format(YFString.GetString("RunStatue_Succeed"), time);
            }
            else
            {
                text4.Text = string.Format(YFString.GetString("RunStatue_Fail"), err);
            }
            button_run.IsEnabled = true;
        }

        private void TB_Mode_Toggled(object sender, RoutedEventArgs e)
        {
            LoadFont_Checked(TB_Mode.IsOn);
            (textbox1.Text, textbox2.Text) = (textbox2.Text, textbox1.Text);
        }

        private void TB_Mode_batch_Toggled(object sender, RoutedEventArgs e)
        {
            LoadFont();
        }
    }
}
