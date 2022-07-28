﻿using System;
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
    public sealed partial class Page_Trail : Page, IMenuChoosable
    {
        public string Title { get; set; } = YFString.GetString("Trail_Title");

        public Action OnShow { get; set; }

        public Page_Trail()
        {
            this.InitializeComponent();
            LoadFont();
        }

        void LoadFont()
        {
            label_introduction.Text = YFString.GetString("Trail_Introduction");
            text1.Text = YFString.GetString("Trail_ChooseInFile");
            text2.Text = YFString.GetString("Trail_ChooseOutFile");
            button1.Content = YFString.GetString("Trail_Choose");
            button2.Content = YFString.GetString("Trail_Choose");
            text_in.Text = YFString.GetString("Trail_ChooseInMode");
            text_out.Text = YFString.GetString("Trail_ChooseOutMode");
            CB_InMode.Items.Clear();
            CB_InMode.Items.Add(YFString.GetString("Trail_ModePCCompiled"));
            CB_InMode.Items.Add(YFString.GetString("Trail_ModeTVCompiled"));
            CB_InMode.Items.Add(YFString.GetString("Trail_ModePhone32Compiled"));
            CB_InMode.Items.Add(YFString.GetString("Trail_ModePhone64Compiled"));
            CB_InMode.Items.Add(YFString.GetString("Trail_ModeGameConsoleCompiled"));
            CB_InMode.Items.Add(YFString.GetString("Trail_ModeWPXnb"));
            CB_InMode.Items.Add(YFString.GetString("Trail_ModeStudioJson"));
            CB_InMode.Items.Add(YFString.GetString("Trail_ModeRawXml"));
            CB_InMode.SelectedIndex = 0;
            CB_OutMode.Items.Clear();
            CB_OutMode.Items.Add(YFString.GetString("Trail_ModePCCompiled"));
            CB_OutMode.Items.Add(YFString.GetString("Trail_ModeTVCompiled"));
            CB_OutMode.Items.Add(YFString.GetString("Trail_ModePhone32Compiled"));
            CB_OutMode.Items.Add(YFString.GetString("Trail_ModePhone64Compiled"));
            CB_OutMode.Items.Add(YFString.GetString("Trail_ModeGameConsoleCompiled"));
            CB_OutMode.Items.Add(YFString.GetString("Trail_ModeWPXnb"));
            CB_OutMode.Items.Add(YFString.GetString("Trail_ModeStudioJson"));
            CB_OutMode.Items.Add(YFString.GetString("Trail_ModeRawXml"));
            CB_OutMode.SelectedIndex = 7;
            button_run.Content = YFString.GetString("Trail_Run");
            label_statue.Text = YFString.GetString("RunStatue_Title");
            text4.Text = YFString.GetString("RunStatue_Wait");
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
            YFFileSystem.YFFile inFile = YFFileSystem.GetYFFileFromPath(textbox1.Text);
            YFFileSystem.YFFile outFile = YFFileSystem.CreateYFFileFromPath(textbox2.Text);
            int inmode = CB_InMode.SelectedIndex;
            int outmode = CB_OutMode.SelectedIndex;
            YFThread.Invoke(() =>
            {
                string err = null;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                try
                {
                    YFAPI.TranscodeTrail(inFile, outFile, inmode, outmode, null);
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
    }
}
