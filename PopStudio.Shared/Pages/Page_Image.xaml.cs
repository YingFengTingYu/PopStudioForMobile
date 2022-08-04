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
    public sealed partial class Page_Image : Page, IMenuChoosable
    {
        public string Title { get; set; } = YFString.GetString("Image_Title");

        public static string StaticTitle { get; set; } = YFString.GetString("Image_Title");

        public Action OnShow { get; set; }

        public Page_Image()
        {
            this.InitializeComponent();
            OnShow += LoadFont_InternalFormat;
            LoadFont();
        }

        void LoadFont()
        {
            label_mode_batch1.Text = YFString.GetString("BatchMode_Single");
            label_mode_batch2.Text = YFString.GetString("BatchMode_Batch");
            string batch = TB_Mode_batch.IsOn ? "_Batch" : string.Empty;
            label_introduction.Text = YFString.GetString("Image_Introduction" + batch);
            label_choosemode.Text = YFString.GetString("Image_ChooseMode");
            label_mode1.Text = YFString.GetString("Image_DecodeMode" + batch);
            label_mode2.Text = YFString.GetString("Image_EncodeMode" + batch);
            LoadFont_Checked(TB_Mode.IsOn);
            text_internalformat.Text = YFString.GetString("Image_InternalFormat");
            button1.Content = YFString.GetString("Image_Choose");
            button2.Content = YFString.GetString("Image_Choose");
            CB_CMode.Items.Clear();
            CB_CMode.Items.Add(YFString.GetString("Image_ModePtxRsb"));
            CB_CMode.Items.Add(YFString.GetString("Image_ModeTexTV"));
            CB_CMode.Items.Add(YFString.GetString("Image_ModeCdat"));
            CB_CMode.Items.Add(YFString.GetString("Image_ModeTexIOS"));
            CB_CMode.Items.Add(YFString.GetString("Image_ModeTxz"));
            CB_CMode.Items.Add(YFString.GetString("Image_ModePtxPS3"));
            CB_CMode.Items.Add(YFString.GetString("Image_ModePtxXBox360"));
            CB_CMode.Items.Add(YFString.GetString("Image_ModePtxPSV"));
            CB_CMode.SelectedIndex = 0;
            button_run.Content = YFString.GetString("Image_Run");
            label_statue.Text = YFString.GetString("RunStatue_Title");
            text4.Text = YFString.GetString("RunStatue_Wait");
        }

        private void CB_CMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadFont_InternalFormat();
        }

        private List<(string, int, Plugin.Endian)> _internalFormatInfo;

        void LoadFont_InternalFormat()
        {
            CB_InternalFormat.Items.Clear();
            switch (CB_CMode.SelectedIndex)
            {
                case 0:
                    _internalFormatInfo = Settings.GlobalSetting.Singleton.PtxRsb.GetStringList();
                    foreach ((string, int, Plugin.Endian) item in _internalFormatInfo)
                    {
                        if (item.Item3 == Plugin.Endian.Big)
                        {
                            CB_InternalFormat.Items.Add(
                                string.Format(YFString.GetString("Image_BigEndianComboBoxItem"),
                                item.Item2,
                                item.Item1
                                ));
                        }
                        else
                        {
                            CB_InternalFormat.Items.Add(
                                string.Format(YFString.GetString("Image_CommonComboBoxItem"),
                                item.Item2,
                                item.Item1
                                ));
                        }
                    }
                    break;
                case 1:
                    _internalFormatInfo = Settings.GlobalSetting.Singleton.TexTV.GetStringList();
                    foreach ((string, int, Plugin.Endian) item in _internalFormatInfo)
                    {
                        CB_InternalFormat.Items.Add(
                            string.Format(YFString.GetString("Image_CommonComboBoxItem"),
                            item.Item2,
                            item.Item1
                            ));
                    }
                    break;
                case 2:
                    CB_InternalFormat.Items.Add(
                        string.Format(YFString.GetString("Image_FormatOnlyComboBoxItem"),
                        "Encrypted"
                        ));
                    break;
                case 3:
                    _internalFormatInfo = Settings.GlobalSetting.Singleton.TexIOS.GetStringList();
                    foreach ((string, int, Plugin.Endian) item in _internalFormatInfo)
                    {
                        CB_InternalFormat.Items.Add(
                            string.Format(YFString.GetString("Image_CommonComboBoxItem"),
                            item.Item2,
                            item.Item1
                            ));
                    }
                    break;
                case 4:
                    _internalFormatInfo = Settings.GlobalSetting.Singleton.Txz.GetStringList();
                    foreach ((string, int, Plugin.Endian) item in _internalFormatInfo)
                    {
                        CB_InternalFormat.Items.Add(
                            string.Format(YFString.GetString("Image_CommonComboBoxItem"),
                            item.Item2,
                            item.Item1
                            ));
                    }
                    break;
                case 5:
                    CB_InternalFormat.Items.Add(
                        string.Format(YFString.GetString("Image_FormatOnlyComboBoxItem"),
                        "RGBA_DXT5"
                        ));
                    break;
                case 6:
                    CB_InternalFormat.Items.Add(
                        string.Format(YFString.GetString("Image_FormatOnlyComboBoxItem"),
                        "RGBA_DXT5_BIGENDIAN_PADDING"
                        ));
                    break;
                case 7:
                    CB_InternalFormat.Items.Add(
                        string.Format(YFString.GetString("Image_FormatOnlyComboBoxItem"),
                        "RGBA_DXT5_REFLECTEDMORTON"
                        ));
                    break;
            }
            CB_InternalFormat.SelectedIndex = 0;
        }

        void LoadFont_Checked(bool v)
        {
            string batch = TB_Mode_batch.IsOn ? "_Batch" : string.Empty;
            if (v)
            {
                text1.Text = YFString.GetString("Image_EncodeChooseInFile" + batch);
                text2.Text = YFString.GetString("Image_EncodeChooseOutFile" + batch);
                text3.Text = YFString.GetString("Image_EncodeChooseMode");
            }
            else
            {
                text1.Text = YFString.GetString("Image_DecodeChooseInFile" + batch);
                text2.Text = YFString.GetString("Image_DecodeChooseOutFile" + batch);
                text3.Text = YFString.GetString("Image_DecodeChooseMode");
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

        private string GetExtension(int mode) => mode switch
        {
            0 or (>= 5 and <= 7) => ".ptx",
            1 or 3 => ".tex",
            2 => ".cdat",
            4 => ".txz",
            _ => null
        };

        private async void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            button_run.IsEnabled = false;
            text4.Text = YFString.GetString("RunStatue_Run");
            bool batch = TB_Mode_batch.IsOn;
            bool mode = TB_Mode.IsOn;
            int cmode = CB_CMode.SelectedIndex;
            (string _, int internalFormat, Plugin.Endian endian) = _internalFormatInfo[CB_InternalFormat.SelectedIndex];
            string inData = textbox1.Text;
            string outData = textbox2.Text;
            string inFormat = mode ? ".png" : GetExtension(cmode);
            string outFormat = mode ? GetExtension(cmode) : ".png";
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
                    void TranscodePtxInFolder(YFFileSystem.YFDirectory m_dir_in, YFFileSystem.YFDirectory m_dir_out)
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
                                            YFAPI.EncodeImage(f, o, cmode, internalFormat, endian);
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
                                            YFAPI.DecodeImage(f, o, cmode);
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
                                TranscodePtxInFolder(f, m_dir_out.CreateYFDirectory(f.Name));
                            }
                        }
                    }
                    await Task.Run(() => TranscodePtxInFolder(inFolder, outFolder));
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
                        taskList.Add(Task.Run(() => YFAPI.EncodeImage(inFile, outFile, cmode, internalFormat, endian)));
                    }
                    else
                    {
                        taskList.Add(Task.Run(() => YFAPI.DecodeImage(inFile, outFile, cmode)));
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
            Stack_InternalFormat.Visibility = TB_Mode.IsOn ? Visibility.Visible : Visibility.Collapsed;
            (textbox1.Text, textbox2.Text) = (textbox2.Text, textbox1.Text);
        }

        private void TB_Mode_batch_Toggled(object sender, RoutedEventArgs e)
        {
            LoadFont();
        }
    }
}
