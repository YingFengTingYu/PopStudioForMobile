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
    public sealed partial class Page_Rton : Page, IMenuChoosable
    {
        public string Title { get; set; } = YFString.GetString("Rton_Title");

        public Action OnShow { get; set; }

        public Page_Rton()
        {
            this.InitializeComponent();
            LoadFont();
        }

        void LoadFont()
        {
            label_mode_batch1.Text = YFString.GetString("BatchMode_Single");
            label_mode_batch2.Text = YFString.GetString("BatchMode_Batch");
            string batch = TB_Mode_batch.IsOn ? "_Batch" : string.Empty;
            label_introduction.Text = YFString.GetString("Rton_Introduction" + batch);
            label_choosemode.Text = YFString.GetString("Rton_ChooseMode");
            label_mode1.Text = YFString.GetString("Rton_DecodeMode" + batch);
            label_mode2.Text = YFString.GetString("Rton_EncodeMode" + batch);
            LoadFont_Checked(TB_Mode.IsOn);
            button1.Content = YFString.GetString("Rton_Choose");
            button2.Content = YFString.GetString("Rton_Choose");
            CB_CMode.Items.Clear();
            CB_CMode.Items.Add(YFString.GetString("Rton_ModeSimpleRton"));
            CB_CMode.Items.Add(YFString.GetString("Rton_ModeEncryptedRton"));
            CB_CMode.SelectedIndex = 0;
            button_run.Content = YFString.GetString("Rton_Run");
            label_statue.Text = YFString.GetString("RunStatue_Title");
            text4.Text = YFString.GetString("RunStatue_Wait");
        }

        void LoadFont_Checked(bool v)
        {
            string batch = TB_Mode_batch.IsOn ? "_Batch" : string.Empty;
            if (v)
            {
                text1.Text = YFString.GetString("Rton_EncodeChooseInFile" + batch);
                text2.Text = YFString.GetString("Rton_EncodeChooseOutFile" + batch);
                text3.Text = YFString.GetString("Rton_EncodeChooseMode");
            }
            else
            {
                text1.Text = YFString.GetString("Rton_DecodeChooseInFile" + batch);
                text2.Text = YFString.GetString("Rton_DecodeChooseOutFile" + batch);
                text3.Text = YFString.GetString("Rton_DecodeChooseMode");
            }
        }

        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            string path = TB_Mode_batch.IsOn ? await YFFileSystem.ChooseFolder() : await YFFileSystem.ChooseOpenFile();
            if (!string.IsNullOrEmpty(path))
            {
                textbox1.Text = path;
            }
        }

        private async void Button2_Click(object sender, RoutedEventArgs e)
        {
            string path = TB_Mode_batch.IsOn ? await YFFileSystem.ChooseFolder() : await YFFileSystem.ChooseSaveFile();
            if (!string.IsNullOrEmpty(path))
            {
                textbox2.Text = path;
            }
        }

        private async void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            button_run.IsEnabled = false;
            text4.Text = YFString.GetString("RunStatue_Run");
            bool batch = TB_Mode_batch.IsOn;
            bool mode = TB_Mode.IsOn == true;
            int cmode = CB_CMode.SelectedIndex;
            string inData = textbox1.Text;
            string outData = textbox2.Text;
            string inFormat = mode ? ".json" : ".rton";
            string outFormat = mode ? ".rton" : ".json";
            string err = null;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                List<Task> taskList = new List<Task>();
                if (batch)
                {
                    YFFileSystem.YFDirectory inFolder = YFFileSystem.GetYFDirectoryFromPath(inData);
                    YFFileSystem.YFDirectory outFolder = YFFileSystem.CreateYFDirectoryFromPath(outData);
                    void TranscodeRtonInFolder(YFFileSystem.YFDirectory m_dir_in, YFFileSystem.YFDirectory m_dir_out)
                    {
                        foreach (YFFileSystem.YFFile f in m_dir_in.FileMap)
                        {
                            if (f.Name.ToLower().EndsWith(inFormat))
                            {
                                YFFileSystem.YFFile o = m_dir_out.CreateYFFile(f.Name[..^inFormat.Length] + outFormat);
                                taskList.Add(Task.Run(() =>
                                {
                                    YFAPI.DecodeRton(f, o, cmode, null);
                                }));
                            }
                        }
                        foreach (YFFileSystem.YFDirectory f in m_dir_in.DirectoryMap)
                        {
                            TranscodeRtonInFolder(f, m_dir_out.CreateYFDirectory(f.Name));
                        }
                    }
                    await Task.Run(() => TranscodeRtonInFolder(inFolder, outFolder));
                }
                else
                {
                    YFFileSystem.YFFile inFile = YFFileSystem.GetYFFileFromPath(inData);
                    YFFileSystem.YFFile outFile =
                        YFFileSystem.CreateYFFileFromPath(outData)
                        ?? YFFileSystem.CreateYFDirectoryFromPath(outData)
                        .CreateYFFile(inFile.Name + outFormat);
                    if (mode)
                    {
                        taskList.Add(Task.Run(() => YFAPI.EncodeRton(inFile, outFile, cmode, null)));
                    }
                    else
                    {
                        taskList.Add(Task.Run(() => YFAPI.DecodeRton(inFile, outFile, cmode, null)));
                    }
                }
                await Task.WhenAll(taskList);
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
