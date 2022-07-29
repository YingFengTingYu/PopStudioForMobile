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
            label_mode_batch1.Text = YFString.GetString("BatchMode_Single");
            label_mode_batch2.Text = YFString.GetString("BatchMode_Batch");
            string batch = TB_Mode_batch.IsOn ? "_Batch" : string.Empty;
            label_introduction.Text = YFString.GetString("Pam_Introduction" + batch);
            label_choosemode.Text = YFString.GetString("Pam_ChooseMode");
            label_mode1.Text = YFString.GetString("Pam_DecodeMode" + batch);
            label_mode2.Text = YFString.GetString("Pam_EncodeMode" + batch);
            LoadFont_Checked(TB_Mode.IsOn);
            button1.Content = YFString.GetString("Pam_Choose");
            button2.Content = YFString.GetString("Pam_Choose");
            button_run.Content = YFString.GetString("Pam_Run");
            label_statue.Text = YFString.GetString("RunStatue_Title");
            text4.Text = YFString.GetString("RunStatue_Wait");
        }

        void LoadFont_Checked(bool v)
        {
            string batch = TB_Mode_batch.IsOn ? "_Batch" : string.Empty;
            if (v)
            {
                text1.Text = YFString.GetString("Pam_EncodeChooseInFile" + batch);
                text2.Text = YFString.GetString("Pam_EncodeChooseOutFile" + batch);
            }
            else
            {
                text1.Text = YFString.GetString("Pam_DecodeChooseInFile" + batch);
                text2.Text = YFString.GetString("Pam_DecodeChooseOutFile" + batch);
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
            bool mode = TB_Mode.IsOn;
            string inData = textbox1.Text;
            string outData = textbox2.Text;
            string inFormat = mode ? ".pam.json" : ".pam";
            string outFormat = mode ? ".pam" : ".pam.json";
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
                    void TranscodePamInFolder(YFFileSystem.YFDirectory m_dir_in, YFFileSystem.YFDirectory m_dir_out)
                    {
                        foreach (YFFileSystem.YFFile f in m_dir_in.GetAllFiles())
                        {
                            if (f.Name.ToLower().EndsWith(inFormat) && !m_dir_out.DirectoryExist(f.Name))
                            {
                                YFFileSystem.YFFile o = m_dir_out.CreateYFFile(f.Name[..^inFormat.Length] + outFormat);
                                if (mode)
                                {
                                    taskList.Add(Task.Run(() =>
                                    {
                                        try
                                        {
                                            YFAPI.EncodePam(f, o);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }));
                                }
                                else
                                {
                                    taskList.Add(Task.Run(() =>
                                    {
                                        try
                                        {
                                            YFAPI.DecodePam(f, o);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }));
                                }
                            }
                        }
                        foreach (YFFileSystem.YFDirectory f in m_dir_in.GetAllDirectories())
                        {
                            if (!m_dir_out.FileExist(f.Name))
                            {
                                TranscodePamInFolder(f, m_dir_out.CreateYFDirectory(f.Name));
                            }
                        }
                    }
                    await Task.Run(() => TranscodePamInFolder(inFolder, outFolder));
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
                        taskList.Add(Task.Run(() => YFAPI.EncodePam(inFile, outFile)));
                    }
                    else
                    {
                        taskList.Add(Task.Run(() => YFAPI.DecodePam(inFile, outFile)));
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
