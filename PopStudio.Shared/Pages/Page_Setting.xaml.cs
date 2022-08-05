using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PopStudio.PlatformAPI;
using PopStudio.Settings;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_Setting : Page, IMenuChoosable
    {
        public string Title { get; set; } = YFString.GetString("Setting_Title");

        public static string StaticTitle { get; set; } = YFString.GetString("Setting_Title");

        public Action OnShow { get; set; }

        public Page_Setting()
        {
            this.InitializeComponent();
            LoadFont();
            LoadData();
        }

        void LoadFont()
        {
            label_rsb.Text = YFString.GetString("Setting_RsbTitle");
            label_rsb_decodeimage.Text = YFString.GetString("Setting_RsbDecodeImage");
            label_rsb_deleteimage.Text = YFString.GetString("Setting_RsbDeleteImage");
            label_dz.Text = YFString.GetString("Setting_DzTitle");
            label_dz_decodeimage.Text = YFString.GetString("Setting_DzDecodeImage");
            label_dz_deleteimage.Text = YFString.GetString("Setting_DzDeleteImage");
            label_dz_encodecompression.Text = YFString.GetString("Setting_DzCompression");
            button_dz_encodecompression_add.Content = YFString.GetString("Setting_Add");
            button_dz_encodecompression_clear.Content = YFString.GetString("Setting_Clear");
            label_pak.Text = YFString.GetString("Setting_PakTitle");
            label_pak_decodeimage.Text = YFString.GetString("Setting_PakDecodeImage");
            label_pak_deleteimage.Text = YFString.GetString("Setting_PakDeleteImage");
            label_pak_encodecompression.Text = YFString.GetString("Setting_PakCompression");
            button_pak_encodecompression_add.Content = YFString.GetString("Setting_Add");
            button_pak_encodecompression_clear.Content = YFString.GetString("Setting_Clear");
            label_ptxrsb.Text = YFString.GetString("Setting_PtxRsbTitle");
            label_ptxrsb_littleendianformat.Text = YFString.GetString("Setting_PtxRsbLittleEndianFormat");
            button_ptxrsb_littleendianformat_add.Content = YFString.GetString("Setting_Add");
            button_ptxrsb_littleendianformat_clear.Content = YFString.GetString("Setting_Clear");
            label_ptxrsb_bigendianformat.Text = YFString.GetString("Setting_PtxRsbBigEndianFormat");
            button_ptxrsb_bigendianformat_add.Content = YFString.GetString("Setting_Add");
            button_ptxrsb_bigendianformat_clear.Content = YFString.GetString("Setting_Clear");
            label_textv.Text = YFString.GetString("Setting_TexTVTitle");
            label_textv_encodezlib.Text = YFString.GetString("Setting_TexTVUseZlib");
            label_textv_format.Text = YFString.GetString("Setting_TexTVFormat");
            button_textv_format_add.Content = YFString.GetString("Setting_Add");
            button_textv_format_clear.Content = YFString.GetString("Setting_Clear");
            label_cdat.Text = YFString.GetString("Setting_CdatTitle");
            label_cdat_cipher.Text = YFString.GetString("Setting_CdatCipher");
            button_cdat_cipher.Content = YFString.GetString("Setting_Submit");
            label_texios.Text = YFString.GetString("Setting_TexiOSTitle");
            label_texios_format.Text = YFString.GetString("Setting_TexiOSFormat");
            button_texios_format_add.Content = YFString.GetString("Setting_Add");
            button_texios_format_clear.Content = YFString.GetString("Setting_Clear");
            label_txz.Text = YFString.GetString("Setting_TxzTitle");
            label_txz_format.Text = YFString.GetString("Setting_TxzFormat");
            button_txz_format_add.Content = YFString.GetString("Setting_Add");
            button_txz_format_clear.Content = YFString.GetString("Setting_Clear");
            label_rton.Text = YFString.GetString("Setting_RtonTitle");
            label_rton_cipher.Text = YFString.GetString("Setting_RtonCipher");
            button_rton_cipher.Content = YFString.GetString("Setting_Submit");
            label_list_dz_encodecompression_1.Text = YFString.GetString("Setting_FileExtension");
            label_list_dz_encodecompression_2.Text = YFString.GetString("Setting_CompressionAlgorithm");
            label_list_pak_encodecompression_1.Text = YFString.GetString("Setting_FileExtension");
            label_list_pak_encodecompression_2.Text = YFString.GetString("Setting_CompressionAlgorithm");
            label_list_ptxrsb_littleendianformat_1.Text = YFString.GetString("Setting_Number");
            label_list_ptxrsb_littleendianformat_2.Text = YFString.GetString("Setting_TextureFormat");
            label_list_ptxrsb_bigendianformat_1.Text = YFString.GetString("Setting_Number");
            label_list_ptxrsb_bigendianformat_2.Text = YFString.GetString("Setting_TextureFormat");
            label_list_textv_format_1.Text = YFString.GetString("Setting_Number");
            label_list_textv_format_2.Text = YFString.GetString("Setting_TextureFormat");
            label_list_texios_format_1.Text = YFString.GetString("Setting_Number");
            label_list_texios_format_2.Text = YFString.GetString("Setting_TextureFormat");
            label_list_txz_format_1.Text = YFString.GetString("Setting_Number");
            label_list_txz_format_2.Text = YFString.GetString("Setting_TextureFormat");
            button_reset_setting.Content = YFString.GetString("Setting_ResetSetting");
        }

        void LoadData()
        {
            switch_rsb_decodeimage.IsOn = GlobalSetting.Singleton.Rsb.DecodeImage;
            switch_rsb_decodeimage.Toggled += (s, e) =>
            {
                GlobalSetting.Singleton.Rsb.DecodeImage = switch_rsb_decodeimage.IsOn;
                GlobalSetting.Save();
            };
            switch_rsb_deleteimage.IsOn = GlobalSetting.Singleton.Rsb.DeleteDecodedImage;
            switch_rsb_deleteimage.Toggled += (s, e) =>
            {
                GlobalSetting.Singleton.Rsb.DeleteDecodedImage = switch_rsb_deleteimage.IsOn;
                GlobalSetting.Save();
            };
            switch_dz_decodeimage.IsOn = GlobalSetting.Singleton.Dz.DecodeImage;
            switch_dz_decodeimage.Toggled += (s, e) =>
            {
                GlobalSetting.Singleton.Dz.DecodeImage = switch_dz_decodeimage.IsOn;
                GlobalSetting.Save();
            };
            switch_dz_deleteimage.IsOn = GlobalSetting.Singleton.Dz.DeleteDecodedImage;
            switch_dz_deleteimage.Toggled += (s, e) =>
            {
                GlobalSetting.Singleton.Dz.DeleteDecodedImage = switch_dz_deleteimage.IsOn;
                GlobalSetting.Save();
            };
            DzCompression.Clear();
            DzCompression.Add(new TwoItem(YFString.GetString("Setting_Default"), GlobalSetting.Singleton.Dz.DefaultFlags.ToString(), true));
            foreach (var pairs in GlobalSetting.Singleton.Dz.CompressDictionary)
            {
                DzCompression.Add(new TwoItem(pairs.Key, pairs.Value.ToString()));
            }
            list_dz_encodecompression.ItemsSource = DzCompression;
            list_dz_encodecompression.ItemClick += async (s, e) =>
            {
                try
                {
                    TwoItem item = e.ClickedItem as TwoItem;
                    ListView listView = new ListView();
                    listView.SelectionMode = ListViewSelectionMode.Single;
                    string format = YFString.GetString("Setting_ModifyCompression");
                    listView.Items.Add(string.Format(format, Package.Dz.DzCompressionFlags.STORE.ToString()));
                    listView.Items.Add(string.Format(format, Package.Dz.DzCompressionFlags.LZMA.ToString()));
                    listView.Items.Add(string.Format(format, Package.Dz.DzCompressionFlags.GZIP.ToString()));
                    listView.Items.Add(string.Format(format, Package.Dz.DzCompressionFlags.BZIP2.ToString()));
                    if (!item.Special)
                    {
                        listView.Items.Add(YFString.GetString("Setting_DeleteThisItem"));
                    }
                    listView.SelectedIndex = 0;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_SelectOperation"),
                        Content = listView,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        if (listView.SelectedIndex == 0)
                        {
                            item.Item2 = Package.Dz.DzCompressionFlags.STORE.ToString();
                            list_dz_encodecompression.ItemsSource = null;
                            list_dz_encodecompression.ItemsSource = DzCompression;
                            GlobalSetting.Singleton.Dz.SetFlags(item.Item1, Package.Dz.DzCompressionFlags.STORE, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 1)
                        {
                            item.Item2 = Package.Dz.DzCompressionFlags.LZMA.ToString();
                            list_dz_encodecompression.ItemsSource = null;
                            list_dz_encodecompression.ItemsSource = DzCompression;
                            GlobalSetting.Singleton.Dz.SetFlags(item.Item1, Package.Dz.DzCompressionFlags.LZMA, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 2)
                        {
                            item.Item2 = Package.Dz.DzCompressionFlags.GZIP.ToString();
                            list_dz_encodecompression.ItemsSource = null;
                            list_dz_encodecompression.ItemsSource = DzCompression;
                            GlobalSetting.Singleton.Dz.SetFlags(item.Item1, Package.Dz.DzCompressionFlags.GZIP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 3)
                        {
                            item.Item2 = Package.Dz.DzCompressionFlags.BZIP2.ToString();
                            list_dz_encodecompression.ItemsSource = null;
                            list_dz_encodecompression.ItemsSource = DzCompression;
                            GlobalSetting.Singleton.Dz.SetFlags(item.Item1, Package.Dz.DzCompressionFlags.BZIP2, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 4)
                        {
                            DzCompression.Remove(item);
                            GlobalSetting.Singleton.Dz.RemoveFormat(item.Item1);
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_dz_encodecompression_add.Click += async (s, e) =>
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    TextBox textbox = new TextBox();
                    panel.Children.Add(textbox);
                    ComboBox combobox = new ComboBox();
                    combobox.Items.Add(Package.Dz.DzCompressionFlags.STORE.ToString());
                    combobox.Items.Add(Package.Dz.DzCompressionFlags.LZMA.ToString());
                    combobox.Items.Add(Package.Dz.DzCompressionFlags.GZIP.ToString());
                    combobox.Items.Add(Package.Dz.DzCompressionFlags.BZIP2.ToString());
                    combobox.SelectedIndex = 0;
                    combobox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    combobox.VerticalAlignment = VerticalAlignment.Stretch;
                    panel.Children.Add(combobox);
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_EnterCompressionInfo"),
                        Content = panel,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        Package.Dz.DzCompressionFlags flags = combobox.SelectedIndex switch
                        {
                            0 => Package.Dz.DzCompressionFlags.STORE,
                            1 => Package.Dz.DzCompressionFlags.LZMA,
                            2 => Package.Dz.DzCompressionFlags.GZIP,
                            3 => Package.Dz.DzCompressionFlags.BZIP2,
                            _ => Package.Dz.DzCompressionFlags.ZERO,
                        };
                        if (GlobalSetting.Singleton.Dz.AddFlags(textbox.Text, flags))
                        {
                            DzCompression.Add(new TwoItem(textbox.Text, flags.ToString()));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_dz_encodecompression_clear.Click += (s, e) =>
            {
                try
                {
                    while (DzCompression.Count > 1)
                    {
                        TwoItem item = DzCompression[^1];
                        DzCompression.Remove(item);
                        GlobalSetting.Singleton.Dz.RemoveFormat(item.Item1);
                    }
                    GlobalSetting.Save();
                }
                catch (Exception)
                {

                }
            };
            switch_pak_decodeimage.IsOn = GlobalSetting.Singleton.Pak.DecodeImage;
            switch_pak_decodeimage.Toggled += (s, e) =>
            {
                GlobalSetting.Singleton.Pak.DecodeImage = switch_pak_decodeimage.IsOn;
                GlobalSetting.Save();
            };
            switch_pak_deleteimage.IsOn = GlobalSetting.Singleton.Pak.DeleteDecodedImage;
            switch_pak_deleteimage.Toggled += (s, e) =>
            {
                GlobalSetting.Singleton.Pak.DeleteDecodedImage = switch_pak_deleteimage.IsOn;
                GlobalSetting.Save();
            };
            PakCompression.Clear();
            PakCompression.Add(new TwoItem(YFString.GetString("Setting_Default"), GlobalSetting.Singleton.Pak.DefaultFlags.ToString(), true));
            foreach (var pairs in GlobalSetting.Singleton.Pak.CompressDictionary)
            {
                PakCompression.Add(new TwoItem(pairs.Key, pairs.Value.ToString()));
            }
            list_pak_encodecompression.ItemsSource = PakCompression;
            list_pak_encodecompression.ItemClick += async (s, e) =>
            {
                try
                {
                    TwoItem item = e.ClickedItem as TwoItem;
                    ListView listView = new ListView();
                    listView.SelectionMode = ListViewSelectionMode.Single;
                    string format = YFString.GetString("Setting_ModifyCompression");
                    listView.Items.Add(string.Format(format, Package.Pak.PakCompressionFlags.STORE.ToString()));
                    listView.Items.Add(string.Format(format, Package.Pak.PakCompressionFlags.ZLIB.ToString()));
                    if (!item.Special)
                    {
                        listView.Items.Add(YFString.GetString("Setting_DeleteThisItem"));
                    }
                    listView.SelectedIndex = 0;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_SelectOperation"),
                        Content = listView,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        if (listView.SelectedIndex == 0)
                        {
                            item.Item2 = Package.Pak.PakCompressionFlags.STORE.ToString();
                            list_pak_encodecompression.ItemsSource = null;
                            list_pak_encodecompression.ItemsSource = PakCompression;
                            GlobalSetting.Singleton.Pak.SetFlags(item.Item1, Package.Pak.PakCompressionFlags.STORE, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 1)
                        {
                            item.Item2 = Package.Pak.PakCompressionFlags.ZLIB.ToString();
                            list_pak_encodecompression.ItemsSource = null;
                            list_pak_encodecompression.ItemsSource = PakCompression;
                            GlobalSetting.Singleton.Pak.SetFlags(item.Item1, Package.Pak.PakCompressionFlags.ZLIB, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 2)
                        {
                            PakCompression.Remove(item);
                            GlobalSetting.Singleton.Pak.RemoveFormat(item.Item1);
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_pak_encodecompression_add.Click += async (s, e) =>
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    TextBox textbox = new TextBox();
                    panel.Children.Add(textbox);
                    ComboBox combobox = new ComboBox();
                    combobox.Items.Add(Package.Pak.PakCompressionFlags.STORE.ToString());
                    combobox.Items.Add(Package.Pak.PakCompressionFlags.ZLIB.ToString());
                    combobox.SelectedIndex = 0;
                    combobox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    combobox.VerticalAlignment = VerticalAlignment.Stretch;
                    panel.Children.Add(combobox);
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_EnterCompressionInfo"),
                        Content = panel,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        Package.Pak.PakCompressionFlags flags = combobox.SelectedIndex switch
                        {
                            0 => Package.Pak.PakCompressionFlags.STORE,
                            1 => Package.Pak.PakCompressionFlags.ZLIB,
                            _ => 0
                        };
                        if (GlobalSetting.Singleton.Pak.AddFlags(textbox.Text, flags))
                        {
                            PakCompression.Add(new TwoItem(textbox.Text, flags.ToString()));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_pak_encodecompression_clear.Click += (s, e) =>
            {
                try
                {
                    while (PakCompression.Count > 1)
                    {
                        TwoItem item = PakCompression[^1];
                        PakCompression.Remove(item);
                        GlobalSetting.Singleton.Pak.RemoveFormat(item.Item1);
                    }
                    GlobalSetting.Save();
                }
                catch (Exception)
                {

                }
            };
            PtxRsbLittleEndian.Clear();
            PtxRsbLittleEndian.Add(new TwoItem(YFString.GetString("Setting_Default"), GlobalSetting.Singleton.PtxRsb.DefaultFormatSmallEndian.ToString(), true));
            foreach (var pairs in GlobalSetting.Singleton.PtxRsb.FormatMapSmallEndian)
            {
                PtxRsbLittleEndian.Add(new TwoItem(pairs.Index.ToString(), pairs.Format.ToString()));
            }
            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
            list_ptxrsb_littleendianformat.ItemClick += async (s, e) =>
            {
                try
                {
                    TwoItem item = e.ClickedItem as TwoItem;
                    ListView listView = new ListView();
                    listView.SelectionMode = ListViewSelectionMode.Single;
                    string format = YFString.GetString("Setting_ModifyTexture");
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.NONE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A4_R4_G4_B4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A1_R5_G5_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT3.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString()));
                    if (!item.Special)
                    {
                        listView.Items.Add(YFString.GetString("Setting_DeleteThisItem"));
                    }
                    listView.SelectedIndex = 0;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_SelectOperation"),
                        Content = listView,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        if (listView.SelectedIndex == 0)
                        {
                            item.Item2 = Image.Texture.TextureFormat.NONE.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.NONE, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 1)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.B8_G8_R8_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 2)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 3)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.R8_G8_B8_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 4)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 5)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.A8_R8_G8_B8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 6)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 7)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.R4_G4_B4_A4, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 8)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 9)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A4_R4_G4_B4.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.A4_R4_G4_B4, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 10)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.R5_G5_B5_A1, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 11)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 12)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A1_R5_G5_B5.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.A1_R5_G5_B5, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 13)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.R5_G6_B5, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 14)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.R5_G6_B5_BLOCK, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 15)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.R8_G8_B8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 16)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_DXT1.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGB_DXT1, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 17)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT1.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_DXT1, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 18)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT3.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_DXT3, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 19)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_DXT5, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 20)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 21)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 22)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 23)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGB_ETC1, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 24)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGB_ETC1_ADD_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 25)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 26)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGB_PVRTCI_4BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 27)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 28)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 29)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGB_PVRTCI_2BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 30)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 31)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 32)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 33)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ATC.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGB_ATC, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 34)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 35)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString();
                            list_ptxrsb_littleendianformat.ItemsSource = null;
                            list_ptxrsb_littleendianformat.ItemsSource = PtxRsbLittleEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Small, Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 36)
                        {
                            PtxRsbLittleEndian.Remove(item);
                            GlobalSetting.Singleton.PtxRsb.RemoveFormat(int.Parse(item.Item1), Plugin.Endian.Small);
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_ptxrsb_littleendianformat_add.Click += async (s, e) =>
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    TextBox textbox = new TextBox();
                    panel.Children.Add(textbox);
                    ComboBox combobox = new ComboBox();
                    combobox.Items.Add(Image.Texture.TextureFormat.NONE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A4_R4_G4_B4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A1_R5_G5_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT3.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString());
                    combobox.SelectedIndex = 0;
                    combobox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    combobox.VerticalAlignment = VerticalAlignment.Stretch;
                    panel.Children.Add(combobox);
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_EnterTextureInfo"),
                        Content = panel,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        Image.Texture.TextureFormat flags = combobox.SelectedIndex switch
                        {
                            0 => Image.Texture.TextureFormat.NONE,
                            1 => Image.Texture.TextureFormat.B8_G8_R8_A8,
                            2 => Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8,
                            3 => Image.Texture.TextureFormat.R8_G8_B8_A8,
                            4 => Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8,
                            5 => Image.Texture.TextureFormat.A8_R8_G8_B8,
                            6 => Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING,
                            7 => Image.Texture.TextureFormat.R4_G4_B4_A4,
                            8 => Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK,
                            9 => Image.Texture.TextureFormat.A4_R4_G4_B4,
                            10 => Image.Texture.TextureFormat.R5_G5_B5_A1,
                            11 => Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK,
                            12 => Image.Texture.TextureFormat.A1_R5_G5_B5,
                            13 => Image.Texture.TextureFormat.R5_G6_B5,
                            14 => Image.Texture.TextureFormat.R5_G6_B5_BLOCK,
                            15 => Image.Texture.TextureFormat.R8_G8_B8,
                            16 => Image.Texture.TextureFormat.RGB_DXT1,
                            17 => Image.Texture.TextureFormat.RGBA_DXT1,
                            18 => Image.Texture.TextureFormat.RGBA_DXT3,
                            19 => Image.Texture.TextureFormat.RGBA_DXT5,
                            20 => Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON,
                            21 => Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON,
                            22 => Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING,
                            23 => Image.Texture.TextureFormat.RGB_ETC1,
                            24 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A8,
                            25 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE,
                            26 => Image.Texture.TextureFormat.RGB_PVRTCI_4BPP,
                            27 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP,
                            28 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8,
                            29 => Image.Texture.TextureFormat.RGB_PVRTCI_2BPP,
                            30 => Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP,
                            31 => Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP,
                            32 => Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP,
                            33 => Image.Texture.TextureFormat.RGB_ATC,
                            34 => Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC,
                            35 => Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC,
                            _ => Image.Texture.TextureFormat.NONE
                        };
                        if (GlobalSetting.Singleton.PtxRsb.AddFlags(int.Parse(textbox.Text), Plugin.Endian.Small, flags))
                        {
                            PtxRsbLittleEndian.Add(new TwoItem(textbox.Text, flags.ToString()));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_ptxrsb_littleendianformat_clear.Click += (s, e) =>
            {
                try
                {
                    while (PtxRsbLittleEndian.Count > 1)
                    {
                        TwoItem item = PtxRsbLittleEndian[^1];
                        PtxRsbLittleEndian.Remove(item);
                        GlobalSetting.Singleton.PtxRsb.RemoveFormat(int.Parse(item.Item1), Plugin.Endian.Small);
                    }
                    GlobalSetting.Save();
                }
                catch (Exception)
                {

                }
            };
            PtxRsbBigEndian.Clear();
            PtxRsbBigEndian.Add(new TwoItem(YFString.GetString("Setting_Default"), GlobalSetting.Singleton.PtxRsb.DefaultFormatBigEndian.ToString(), true));
            foreach (var pairs in GlobalSetting.Singleton.PtxRsb.FormatMapBigEndian)
            {
                PtxRsbBigEndian.Add(new TwoItem(pairs.Index.ToString(), pairs.Format.ToString()));
            }
            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
            list_ptxrsb_bigendianformat.ItemClick += async (s, e) =>
            {
                try
                {
                    TwoItem item = e.ClickedItem as TwoItem;
                    ListView listView = new ListView();
                    listView.SelectionMode = ListViewSelectionMode.Single;
                    string format = YFString.GetString("Setting_ModifyTexture");
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.NONE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A4_R4_G4_B4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A1_R5_G5_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT3.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString()));
                    if (!item.Special)
                    {
                        listView.Items.Add(YFString.GetString("Setting_DeleteThisItem"));
                    }
                    listView.SelectedIndex = 0;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_SelectOperation"),
                        Content = listView,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        if (listView.SelectedIndex == 0)
                        {
                            item.Item2 = Image.Texture.TextureFormat.NONE.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.NONE, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 1)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.B8_G8_R8_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 2)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 3)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.R8_G8_B8_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 4)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 5)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.A8_R8_G8_B8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 6)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 7)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.R4_G4_B4_A4, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 8)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 9)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A4_R4_G4_B4.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.A4_R4_G4_B4, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 10)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.R5_G5_B5_A1, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 11)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 12)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A1_R5_G5_B5.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.A1_R5_G5_B5, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 13)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.R5_G6_B5, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 14)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.R5_G6_B5_BLOCK, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 15)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.R8_G8_B8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 16)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_DXT1.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGB_DXT1, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 17)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT1.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_DXT1, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 18)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT3.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_DXT3, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 19)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_DXT5, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 20)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 21)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 22)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 23)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGB_ETC1, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 24)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGB_ETC1_ADD_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 25)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 26)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGB_PVRTCI_4BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 27)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 28)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 29)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGB_PVRTCI_2BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 30)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 31)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 32)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 33)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ATC.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGB_ATC, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 34)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 35)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString();
                            list_ptxrsb_bigendianformat.ItemsSource = null;
                            list_ptxrsb_bigendianformat.ItemsSource = PtxRsbBigEndian;
                            GlobalSetting.Singleton.PtxRsb.SetFlags(TryParseInt(item.Item1), Plugin.Endian.Big, Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 36)
                        {
                            PtxRsbBigEndian.Remove(item);
                            GlobalSetting.Singleton.PtxRsb.RemoveFormat(int.Parse(item.Item1), Plugin.Endian.Big);
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_ptxrsb_bigendianformat_add.Click += async (s, e) =>
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    TextBox textbox = new TextBox();
                    panel.Children.Add(textbox);
                    ComboBox combobox = new ComboBox();
                    combobox.Items.Add(Image.Texture.TextureFormat.NONE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A4_R4_G4_B4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A1_R5_G5_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT3.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString());
                    combobox.SelectedIndex = 0;
                    combobox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    combobox.VerticalAlignment = VerticalAlignment.Stretch;
                    panel.Children.Add(combobox);
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_EnterTextureInfo"),
                        Content = panel,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        Image.Texture.TextureFormat flags = combobox.SelectedIndex switch
                        {
                            0 => Image.Texture.TextureFormat.NONE,
                            1 => Image.Texture.TextureFormat.B8_G8_R8_A8,
                            2 => Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8,
                            3 => Image.Texture.TextureFormat.R8_G8_B8_A8,
                            4 => Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8,
                            5 => Image.Texture.TextureFormat.A8_R8_G8_B8,
                            6 => Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING,
                            7 => Image.Texture.TextureFormat.R4_G4_B4_A4,
                            8 => Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK,
                            9 => Image.Texture.TextureFormat.A4_R4_G4_B4,
                            10 => Image.Texture.TextureFormat.R5_G5_B5_A1,
                            11 => Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK,
                            12 => Image.Texture.TextureFormat.A1_R5_G5_B5,
                            13 => Image.Texture.TextureFormat.R5_G6_B5,
                            14 => Image.Texture.TextureFormat.R5_G6_B5_BLOCK,
                            15 => Image.Texture.TextureFormat.R8_G8_B8,
                            16 => Image.Texture.TextureFormat.RGB_DXT1,
                            17 => Image.Texture.TextureFormat.RGBA_DXT1,
                            18 => Image.Texture.TextureFormat.RGBA_DXT3,
                            19 => Image.Texture.TextureFormat.RGBA_DXT5,
                            20 => Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON,
                            21 => Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON,
                            22 => Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING,
                            23 => Image.Texture.TextureFormat.RGB_ETC1,
                            24 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A8,
                            25 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE,
                            26 => Image.Texture.TextureFormat.RGB_PVRTCI_4BPP,
                            27 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP,
                            28 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8,
                            29 => Image.Texture.TextureFormat.RGB_PVRTCI_2BPP,
                            30 => Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP,
                            31 => Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP,
                            32 => Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP,
                            33 => Image.Texture.TextureFormat.RGB_ATC,
                            34 => Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC,
                            35 => Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC,
                            _ => Image.Texture.TextureFormat.NONE
                        };
                        if (GlobalSetting.Singleton.PtxRsb.AddFlags(int.Parse(textbox.Text), Plugin.Endian.Big, flags))
                        {
                            PtxRsbBigEndian.Add(new TwoItem(textbox.Text, flags.ToString()));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_ptxrsb_bigendianformat_clear.Click += (s, e) =>
            {
                try
                {
                    while (PtxRsbBigEndian.Count > 1)
                    {
                        TwoItem item = PtxRsbBigEndian[^1];
                        PtxRsbBigEndian.Remove(item);
                        GlobalSetting.Singleton.PtxRsb.RemoveFormat(int.Parse(item.Item1), Plugin.Endian.Big);
                    }
                    GlobalSetting.Save();
                }
                catch (Exception)
                {

                }
            };
            switch_textv_encodezlib.IsOn = GlobalSetting.Singleton.TexTV.UseZlib;
            switch_textv_encodezlib.Toggled += (s, e) =>
            {
                GlobalSetting.Singleton.TexTV.UseZlib = switch_textv_encodezlib.IsOn;
                GlobalSetting.Save();
            };
            TexTVFormat.Clear();
            foreach (var pairs in GlobalSetting.Singleton.TexTV.FormatMap)
            {
                TexTVFormat.Add(new TwoItem(pairs.Index.ToString(), pairs.Format.ToString()));
            }
            list_textv_format.ItemsSource = TexTVFormat;
            list_textv_format.ItemClick += async (s, e) =>
            {
                try
                {
                    TwoItem item = e.ClickedItem as TwoItem;
                    ListView listView = new ListView();
                    listView.SelectionMode = ListViewSelectionMode.Single;
                    string format = YFString.GetString("Setting_ModifyTexture");
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.NONE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A4_R4_G4_B4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A1_R5_G5_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT3.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString()));
                    listView.Items.Add(YFString.GetString("Setting_DeleteThisItem"));
                    listView.SelectedIndex = 0;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_SelectOperation"),
                        Content = listView,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        if (listView.SelectedIndex == 0)
                        {
                            item.Item2 = Image.Texture.TextureFormat.NONE.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.NONE);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 1)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.B8_G8_R8_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 2)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 3)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R8_G8_B8_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 4)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 5)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A8_R8_G8_B8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 6)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 7)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R4_G4_B4_A4);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 8)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 9)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A4_R4_G4_B4.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A4_R4_G4_B4);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 10)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G5_B5_A1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 11)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 12)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A1_R5_G5_B5.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A1_R5_G5_B5);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 13)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G6_B5);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 14)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G6_B5_BLOCK);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 15)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R8_G8_B8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 16)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_DXT1.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_DXT1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 17)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT1.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 18)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT3.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT3);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 19)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 20)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 21)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 22)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 23)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ETC1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 24)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ETC1_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 25)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 26)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_PVRTCI_4BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 27)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 28)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 29)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_PVRTCI_2BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 30)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 31)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 32)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 33)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ATC.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ATC);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 34)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 35)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString();
                            list_textv_format.ItemsSource = null;
                            list_textv_format.ItemsSource = TexTVFormat;
                            GlobalSetting.Singleton.TexTV.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 36)
                        {
                            TexTVFormat.Remove(item);
                            GlobalSetting.Singleton.TexTV.RemoveFormat(int.Parse(item.Item1));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_textv_format_add.Click += async (s, e) =>
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    TextBox textbox = new TextBox();
                    panel.Children.Add(textbox);
                    ComboBox combobox = new ComboBox();
                    combobox.Items.Add(Image.Texture.TextureFormat.NONE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A4_R4_G4_B4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A1_R5_G5_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT3.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString());
                    combobox.SelectedIndex = 0;
                    combobox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    combobox.VerticalAlignment = VerticalAlignment.Stretch;
                    panel.Children.Add(combobox);
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_EnterTextureInfo"),
                        Content = panel,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        Image.Texture.TextureFormat flags = combobox.SelectedIndex switch
                        {
                            0 => Image.Texture.TextureFormat.NONE,
                            1 => Image.Texture.TextureFormat.B8_G8_R8_A8,
                            2 => Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8,
                            3 => Image.Texture.TextureFormat.R8_G8_B8_A8,
                            4 => Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8,
                            5 => Image.Texture.TextureFormat.A8_R8_G8_B8,
                            6 => Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING,
                            7 => Image.Texture.TextureFormat.R4_G4_B4_A4,
                            8 => Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK,
                            9 => Image.Texture.TextureFormat.A4_R4_G4_B4,
                            10 => Image.Texture.TextureFormat.R5_G5_B5_A1,
                            11 => Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK,
                            12 => Image.Texture.TextureFormat.A1_R5_G5_B5,
                            13 => Image.Texture.TextureFormat.R5_G6_B5,
                            14 => Image.Texture.TextureFormat.R5_G6_B5_BLOCK,
                            15 => Image.Texture.TextureFormat.R8_G8_B8,
                            16 => Image.Texture.TextureFormat.RGB_DXT1,
                            17 => Image.Texture.TextureFormat.RGBA_DXT1,
                            18 => Image.Texture.TextureFormat.RGBA_DXT3,
                            19 => Image.Texture.TextureFormat.RGBA_DXT5,
                            20 => Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON,
                            21 => Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON,
                            22 => Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING,
                            23 => Image.Texture.TextureFormat.RGB_ETC1,
                            24 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A8,
                            25 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE,
                            26 => Image.Texture.TextureFormat.RGB_PVRTCI_4BPP,
                            27 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP,
                            28 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8,
                            29 => Image.Texture.TextureFormat.RGB_PVRTCI_2BPP,
                            30 => Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP,
                            31 => Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP,
                            32 => Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP,
                            33 => Image.Texture.TextureFormat.RGB_ATC,
                            34 => Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC,
                            35 => Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC,
                            _ => Image.Texture.TextureFormat.NONE
                        };
                        if (GlobalSetting.Singleton.TexTV.AddFlags(int.Parse(textbox.Text), flags))
                        {
                            TexTVFormat.Add(new TwoItem(textbox.Text, flags.ToString()));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_textv_format_clear.Click += (s, e) =>
            {
                try
                {
                    while (TexTVFormat.Count > 0)
                    {
                        TwoItem item = TexTVFormat[^1];
                        TexTVFormat.Remove(item);
                        GlobalSetting.Singleton.TexTV.RemoveFormat(int.Parse(item.Item1));
                    }
                    GlobalSetting.Save();
                }
                catch (Exception)
                {

                }
            };
            textbox_cdat_cipher.Text = GlobalSetting.Singleton.Cdat.Cipher;
            button_cdat_cipher.Click += (s, e) =>
            {
                try
                {
                    GlobalSetting.Singleton.Cdat.Cipher = textbox_cdat_cipher.Text;
                    GlobalSetting.Save();
                }
                catch (Exception)
                {

                }
            };
            TexiOSFormat.Clear();
            foreach (var pairs in GlobalSetting.Singleton.TexIOS.FormatMap)
            {
                TexiOSFormat.Add(new TwoItem(pairs.Index.ToString(), pairs.Format.ToString()));
            }
            list_texios_format.ItemsSource = TexiOSFormat;
            list_texios_format.ItemClick += async (s, e) =>
            {
                try
                {
                    TwoItem item = e.ClickedItem as TwoItem;
                    ListView listView = new ListView();
                    listView.SelectionMode = ListViewSelectionMode.Single;
                    string format = YFString.GetString("Setting_ModifyTexture");
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.NONE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A4_R4_G4_B4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A1_R5_G5_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT3.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString()));
                    listView.Items.Add(YFString.GetString("Setting_DeleteThisItem"));
                    listView.SelectedIndex = 0;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_SelectOperation"),
                        Content = listView,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        if (listView.SelectedIndex == 0)
                        {
                            item.Item2 = Image.Texture.TextureFormat.NONE.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.NONE);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 1)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.B8_G8_R8_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 2)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 3)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R8_G8_B8_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 4)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 5)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A8_R8_G8_B8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 6)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 7)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R4_G4_B4_A4);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 8)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 9)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A4_R4_G4_B4.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A4_R4_G4_B4);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 10)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G5_B5_A1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 11)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 12)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A1_R5_G5_B5.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A1_R5_G5_B5);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 13)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G6_B5);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 14)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G6_B5_BLOCK);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 15)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R8_G8_B8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 16)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_DXT1.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_DXT1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 17)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT1.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 18)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT3.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT3);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 19)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 20)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 21)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 22)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 23)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ETC1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 24)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ETC1_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 25)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 26)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_PVRTCI_4BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 27)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 28)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 29)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_PVRTCI_2BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 30)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 31)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 32)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 33)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ATC.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ATC);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 34)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 35)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString();
                            list_texios_format.ItemsSource = null;
                            list_texios_format.ItemsSource = TexiOSFormat;
                            GlobalSetting.Singleton.TexIOS.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 36)
                        {
                            TexiOSFormat.Remove(item);
                            GlobalSetting.Singleton.TexIOS.RemoveFormat(int.Parse(item.Item1));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_texios_format_add.Click += async (s, e) =>
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    TextBox textbox = new TextBox();
                    panel.Children.Add(textbox);
                    ComboBox combobox = new ComboBox();
                    combobox.Items.Add(Image.Texture.TextureFormat.NONE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A4_R4_G4_B4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A1_R5_G5_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT3.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString());
                    combobox.SelectedIndex = 0;
                    combobox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    combobox.VerticalAlignment = VerticalAlignment.Stretch;
                    panel.Children.Add(combobox);
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_EnterTextureInfo"),
                        Content = panel,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        Image.Texture.TextureFormat flags = combobox.SelectedIndex switch
                        {
                            0 => Image.Texture.TextureFormat.NONE,
                            1 => Image.Texture.TextureFormat.B8_G8_R8_A8,
                            2 => Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8,
                            3 => Image.Texture.TextureFormat.R8_G8_B8_A8,
                            4 => Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8,
                            5 => Image.Texture.TextureFormat.A8_R8_G8_B8,
                            6 => Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING,
                            7 => Image.Texture.TextureFormat.R4_G4_B4_A4,
                            8 => Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK,
                            9 => Image.Texture.TextureFormat.A4_R4_G4_B4,
                            10 => Image.Texture.TextureFormat.R5_G5_B5_A1,
                            11 => Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK,
                            12 => Image.Texture.TextureFormat.A1_R5_G5_B5,
                            13 => Image.Texture.TextureFormat.R5_G6_B5,
                            14 => Image.Texture.TextureFormat.R5_G6_B5_BLOCK,
                            15 => Image.Texture.TextureFormat.R8_G8_B8,
                            16 => Image.Texture.TextureFormat.RGB_DXT1,
                            17 => Image.Texture.TextureFormat.RGBA_DXT1,
                            18 => Image.Texture.TextureFormat.RGBA_DXT3,
                            19 => Image.Texture.TextureFormat.RGBA_DXT5,
                            20 => Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON,
                            21 => Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON,
                            22 => Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING,
                            23 => Image.Texture.TextureFormat.RGB_ETC1,
                            24 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A8,
                            25 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE,
                            26 => Image.Texture.TextureFormat.RGB_PVRTCI_4BPP,
                            27 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP,
                            28 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8,
                            29 => Image.Texture.TextureFormat.RGB_PVRTCI_2BPP,
                            30 => Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP,
                            31 => Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP,
                            32 => Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP,
                            33 => Image.Texture.TextureFormat.RGB_ATC,
                            34 => Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC,
                            35 => Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC,
                            _ => Image.Texture.TextureFormat.NONE
                        };
                        if (GlobalSetting.Singleton.TexIOS.AddFlags(int.Parse(textbox.Text), flags))
                        {
                            TexiOSFormat.Add(new TwoItem(textbox.Text, flags.ToString()));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_texios_format_clear.Click += (s, e) =>
            {
                try
                {
                    while (TexiOSFormat.Count > 0)
                    {
                        TwoItem item = TexiOSFormat[^1];
                        TexiOSFormat.Remove(item);
                        GlobalSetting.Singleton.TexIOS.RemoveFormat(int.Parse(item.Item1));
                    }
                    GlobalSetting.Save();
                }
                catch (Exception)
                {

                }
            };
            TxzFormat.Clear();
            foreach (var pairs in GlobalSetting.Singleton.Txz.FormatMap)
            {
                TxzFormat.Add(new TwoItem(pairs.Index.ToString(), pairs.Format.ToString()));
            }
            list_txz_format.ItemsSource = TxzFormat;
            list_txz_format.ItemClick += async (s, e) =>
            {
                try
                {
                    TwoItem item = e.ClickedItem as TwoItem;
                    ListView listView = new ListView();
                    listView.SelectionMode = ListViewSelectionMode.Single;
                    string format = YFString.GetString("Setting_ModifyTexture");
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.NONE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A4_R4_G4_B4.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.A1_R5_G5_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.R8_G8_B8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT3.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString()));
                    listView.Items.Add(string.Format(format, Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString()));
                    listView.Items.Add(YFString.GetString("Setting_DeleteThisItem"));
                    listView.SelectedIndex = 0;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_SelectOperation"),
                        Content = listView,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        if (listView.SelectedIndex == 0)
                        {
                            item.Item2 = Image.Texture.TextureFormat.NONE.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.NONE);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 1)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.B8_G8_R8_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 2)
                        {
                            item.Item2 = Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 3)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R8_G8_B8_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 4)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 5)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A8_R8_G8_B8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 6)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 7)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R4_G4_B4_A4);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 8)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 9)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A4_R4_G4_B4.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A4_R4_G4_B4);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 10)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G5_B5_A1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 11)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 12)
                        {
                            item.Item2 = Image.Texture.TextureFormat.A1_R5_G5_B5.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.A1_R5_G5_B5);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 13)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G6_B5);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 14)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R5_G6_B5_BLOCK);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 15)
                        {
                            item.Item2 = Image.Texture.TextureFormat.R8_G8_B8.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.R8_G8_B8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 16)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_DXT1.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_DXT1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 17)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT1.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 18)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT3.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT3);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 19)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 20)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 21)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 22)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 23)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ETC1);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 24)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ETC1_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 25)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 26)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_PVRTCI_4BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 27)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 28)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 29)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_PVRTCI_2BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 30)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 31)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 32)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 33)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_ATC.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_ATC);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 34)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 35)
                        {
                            item.Item2 = Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString();
                            list_txz_format.ItemsSource = null;
                            list_txz_format.ItemsSource = TxzFormat;
                            GlobalSetting.Singleton.Txz.SetFlags(TryParseInt(item.Item1), Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 36)
                        {
                            TxzFormat.Remove(item);
                            GlobalSetting.Singleton.Txz.RemoveFormat(int.Parse(item.Item1));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_txz_format_add.Click += async (s, e) =>
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    TextBox textbox = new TextBox();
                    panel.Children.Add(textbox);
                    ComboBox combobox = new ComboBox();
                    combobox.Items.Add(Image.Texture.TextureFormat.NONE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A4_R4_G4_B4.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.A1_R5_G5_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R5_G6_B5_BLOCK.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.R8_G8_B8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT3.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC.ToString());
                    combobox.Items.Add(Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC.ToString());
                    combobox.SelectedIndex = 0;
                    combobox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    combobox.VerticalAlignment = VerticalAlignment.Stretch;
                    panel.Children.Add(combobox);
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_EnterTextureInfo"),
                        Content = panel,
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        Image.Texture.TextureFormat flags = combobox.SelectedIndex switch
                        {
                            0 => Image.Texture.TextureFormat.NONE,
                            1 => Image.Texture.TextureFormat.B8_G8_R8_A8,
                            2 => Image.Texture.TextureFormat.B8_G8_R8_A8_ADD_A8,
                            3 => Image.Texture.TextureFormat.R8_G8_B8_A8,
                            4 => Image.Texture.TextureFormat.R8_G8_B8_A8_ADD_A8,
                            5 => Image.Texture.TextureFormat.A8_R8_G8_B8,
                            6 => Image.Texture.TextureFormat.A8_R8_G8_B8_PADDING,
                            7 => Image.Texture.TextureFormat.R4_G4_B4_A4,
                            8 => Image.Texture.TextureFormat.R4_G4_B4_A4_BLOCK,
                            9 => Image.Texture.TextureFormat.A4_R4_G4_B4,
                            10 => Image.Texture.TextureFormat.R5_G5_B5_A1,
                            11 => Image.Texture.TextureFormat.R5_G5_B5_A1_BLOCK,
                            12 => Image.Texture.TextureFormat.A1_R5_G5_B5,
                            13 => Image.Texture.TextureFormat.R5_G6_B5,
                            14 => Image.Texture.TextureFormat.R5_G6_B5_BLOCK,
                            15 => Image.Texture.TextureFormat.R8_G8_B8,
                            16 => Image.Texture.TextureFormat.RGB_DXT1,
                            17 => Image.Texture.TextureFormat.RGBA_DXT1,
                            18 => Image.Texture.TextureFormat.RGBA_DXT3,
                            19 => Image.Texture.TextureFormat.RGBA_DXT5,
                            20 => Image.Texture.TextureFormat.RGBA_DXT5_BLOCK_MORTON,
                            21 => Image.Texture.TextureFormat.RGBA_DXT5_REFLECTEDMORTON,
                            22 => Image.Texture.TextureFormat.RGBA_DXT5_BIGENDIAN_PADDING,
                            23 => Image.Texture.TextureFormat.RGB_ETC1,
                            24 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A8,
                            25 => Image.Texture.TextureFormat.RGB_ETC1_ADD_A_PALETTE,
                            26 => Image.Texture.TextureFormat.RGB_PVRTCI_4BPP,
                            27 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP,
                            28 => Image.Texture.TextureFormat.RGBA_PVRTCI_4BPP_ADD_A8,
                            29 => Image.Texture.TextureFormat.RGB_PVRTCI_2BPP,
                            30 => Image.Texture.TextureFormat.RGBA_PVRTCI_2BPP,
                            31 => Image.Texture.TextureFormat.RGBA_PVRTCII_4BPP,
                            32 => Image.Texture.TextureFormat.RGBA_PVRTCII_2BPP,
                            33 => Image.Texture.TextureFormat.RGB_ATC,
                            34 => Image.Texture.TextureFormat.RGB_A_EXPLICIT_ATC,
                            35 => Image.Texture.TextureFormat.RGB_A_INTERPOLATED_ATC,
                            _ => Image.Texture.TextureFormat.NONE
                        };
                        if (GlobalSetting.Singleton.Txz.AddFlags(int.Parse(textbox.Text), flags))
                        {
                            TxzFormat.Add(new TwoItem(textbox.Text, flags.ToString()));
                            GlobalSetting.Save();
                        }
                    }
                }
                catch (Exception)
                {

                }
            };
            button_txz_format_clear.Click += (s, e) =>
            {
                try
                {
                    while (TxzFormat.Count > 0)
                    {
                        TwoItem item = TxzFormat[^1];
                        TxzFormat.Remove(item);
                        GlobalSetting.Singleton.Txz.RemoveFormat(int.Parse(item.Item1));
                    }
                    GlobalSetting.Save();
                }
                catch (Exception)
                {

                }
            };
            textbox_rton_cipher.Text = GlobalSetting.Singleton.Rton.Cipher;
            button_rton_cipher.Click += (s, e) =>
            {
                try
                {
                    GlobalSetting.Singleton.Rton.Cipher = textbox_rton_cipher.Text;
                    GlobalSetting.Save();
                }
                catch (Exception)
                {

                }
            };
            button_reset_setting.Click += async (s, e) =>
            {
                try
                {
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = YFString.GetString("Setting_ResetSetting"),
                        Content = YFString.GetString("Setting_ResetSetting_Ask"),
                        PrimaryButtonText = YFString.GetString("Setting_OK"),
                        CloseButtonText = YFString.GetString("Setting_Cancel")
                    };
#if WinUI
                    noWifiDialog.XamlRoot = this.Content.XamlRoot;
#endif
                    ContentDialogResult result = await noWifiDialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        GlobalSetting.Recover();
                        ContentDialog noWifiDialog2 = new ContentDialog
                        {
                            Title = YFString.GetString("Setting_ResetSetting"),
                            Content = YFString.GetString("Setting_ResetSetting_Finish"),
                            PrimaryButtonText = YFString.GetString("Setting_OK"),
                        };
#if WinUI
                        noWifiDialog2.XamlRoot = this.Content.XamlRoot;
#endif
                        await noWifiDialog2.ShowAsync();
#if WASM
                        Uno.Foundation.WebAssemblyRuntime.InvokeJS("location.reload();");
#else
                        Environment.Exit(0);
#endif
                    }
                }
                catch (Exception)
                {

                }
            };
        }

        private int TryParseInt(string str)
        {
            return int.TryParse(str, out int v) ? v : 0;
        }

        private ObservableCollection<TwoItem> _dzcompression = new ObservableCollection<TwoItem>();

        public ObservableCollection<TwoItem> DzCompression => _dzcompression;

        private ObservableCollection<TwoItem> _pakcompression = new ObservableCollection<TwoItem>();

        public ObservableCollection<TwoItem> PakCompression => _pakcompression;

        private ObservableCollection<TwoItem> _ptxrsblittleendian = new ObservableCollection<TwoItem>();

        public ObservableCollection<TwoItem> PtxRsbLittleEndian => _ptxrsblittleendian;

        private ObservableCollection<TwoItem> _ptxrsbbigendian = new ObservableCollection<TwoItem>();

        public ObservableCollection<TwoItem> PtxRsbBigEndian => _ptxrsbbigendian;

        private ObservableCollection<TwoItem> _textvformat = new ObservableCollection<TwoItem>();

        public ObservableCollection<TwoItem> TexTVFormat => _textvformat;

        private ObservableCollection<TwoItem> _texiosformat = new ObservableCollection<TwoItem>();

        public ObservableCollection<TwoItem> TexiOSFormat => _texiosformat;

        private ObservableCollection<TwoItem> _txzformat = new ObservableCollection<TwoItem>();

        public ObservableCollection<TwoItem> TxzFormat => _txzformat;

    }

    public class TwoItem
    {
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public bool Special { get; set; }

        public TwoItem()
        {

        }

        public TwoItem(string i1, string i2)
        {
            Item1 = i1;
            Item2 = i2;
        }

        public TwoItem(string i1, string i2, bool special)
        {
            Item1 = i1;
            Item2 = i2;
            Special = special;
        }
    }
}
