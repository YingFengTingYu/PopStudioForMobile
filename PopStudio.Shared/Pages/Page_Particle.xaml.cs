using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using PopStudio.PlatformAPI;
using System.Threading.Tasks;
using System.Collections.Generic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_Particle : Page, IMenuChoosable
    {
        public string Title { get; set; } = YFString.GetString("Particle_Title");

        public Action OnShow { get; set; }

        public Page_Particle()
        {
            this.InitializeComponent();
            LoadFont();
        }

        void LoadFont()
        {
            label_mode_batch1.Text = YFString.GetString("BatchMode_Single");
            label_mode_batch2.Text = YFString.GetString("BatchMode_Batch");
            string batch = TB_Mode_batch.IsOn ? "_Batch" : string.Empty;
            label_introduction.Text = YFString.GetString("Particle_Introduction");
            text1.Text = YFString.GetString("Particle_ChooseInFile" + batch);
            text2.Text = YFString.GetString("Particle_ChooseOutFile" + batch);
            button1.Content = YFString.GetString("Particle_Choose");
            button2.Content = YFString.GetString("Particle_Choose");
            text_in.Text = YFString.GetString("Particle_ChooseInMode");
            text_out.Text = YFString.GetString("Particle_ChooseOutMode");
            CB_InMode.Items.Clear();
            CB_InMode.Items.Add(YFString.GetString("Particle_ModePCCompiled"));
            CB_InMode.Items.Add(YFString.GetString("Particle_ModeTVCompiled"));
            CB_InMode.Items.Add(YFString.GetString("Particle_ModePhone32Compiled"));
            CB_InMode.Items.Add(YFString.GetString("Particle_ModePhone64Compiled"));
            CB_InMode.Items.Add(YFString.GetString("Particle_ModeGameConsoleCompiled"));
            CB_InMode.Items.Add(YFString.GetString("Particle_ModeWPXnb"));
            CB_InMode.Items.Add(YFString.GetString("Particle_ModeStudioJson"));
            CB_InMode.Items.Add(YFString.GetString("Particle_ModeRawXml"));
            CB_InMode.SelectedIndex = 0;
            CB_OutMode.Items.Clear();
            CB_OutMode.Items.Add(YFString.GetString("Particle_ModePCCompiled"));
            CB_OutMode.Items.Add(YFString.GetString("Particle_ModeTVCompiled"));
            CB_OutMode.Items.Add(YFString.GetString("Particle_ModePhone32Compiled"));
            CB_OutMode.Items.Add(YFString.GetString("Particle_ModePhone64Compiled"));
            CB_OutMode.Items.Add(YFString.GetString("Particle_ModeGameConsoleCompiled"));
            CB_OutMode.Items.Add(YFString.GetString("Particle_ModeWPXnb"));
            CB_OutMode.Items.Add(YFString.GetString("Particle_ModeStudioJson"));
            CB_OutMode.Items.Add(YFString.GetString("Particle_ModeRawXml"));
            CB_OutMode.SelectedIndex = 7;
            button_run.Content = YFString.GetString("Particle_Run");
            label_statue.Text = YFString.GetString("RunStatue_Title");
            text4.Text = YFString.GetString("RunStatue_Wait");
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

        private string GetExtension(int mode) => mode switch
        {
            >= 0 and <= 4 => ".xml.compiled",
            5 => ".xnb",
            6 => ".xml.json",
            7 => ".xml",
            _ => null
        };

        private async void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            button_run.IsEnabled = false;
            text4.Text = YFString.GetString("RunStatue_Run");
            bool batch = TB_Mode_batch.IsOn;
            int inmode = CB_InMode.SelectedIndex;
            int outmode = CB_OutMode.SelectedIndex;
            string inData = textbox1.Text;
            string outData = textbox2.Text;
            string inFormat = GetExtension(inmode);
            string outFormat = GetExtension(outmode);
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
                    void TranscodeParticleInFolder(YFFileSystem.YFDirectory m_dir_in, YFFileSystem.YFDirectory m_dir_out)
                    {
                        foreach (YFFileSystem.YFFile f in m_dir_in.GetAllFiles())
                        {
                            if (f.Name.ToLower().EndsWith(inFormat) && !m_dir_out.DirectoryExist(f.Name))
                            {
                                YFFileSystem.YFFile o = m_dir_out.CreateYFFile(f.Name[..^inFormat.Length] + outFormat);
                                taskList.Add(Task.Run(() =>
                                {
                                    try
                                    {
                                        YFAPI.TranscodeParticle(f, o, inmode, outmode, null);
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }));
                            }
                        }
                        foreach (YFFileSystem.YFDirectory f in m_dir_in.GetAllDirectories())
                        {
                            if (!m_dir_out.FileExist(f.Name))
                            {
                                TranscodeParticleInFolder(f, m_dir_out.CreateYFDirectory(f.Name));
                            }
                        }
                    }
                    await Task.Run(() => TranscodeParticleInFolder(inFolder, outFolder));
                }
                else
                {
                    YFFileSystem.YFFile inFile = YFFileSystem.GetYFFileFromPath(inData);
                    YFFileSystem.YFFile outFile =
                        YFFileSystem.CreateYFFileFromPath(outData)
                        ?? YFFileSystem.CreateYFDirectoryFromPath(outData)
                        .CreateYFFile(inFile.Name + outFormat);
                    taskList.Add(Task.Run(() => YFAPI.TranscodeParticle(inFile, outFile, inmode, outmode, null)));
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

        private void TB_Mode_batch_Toggled(object sender, RoutedEventArgs e)
        {
            LoadFont();
        }
    }
}
