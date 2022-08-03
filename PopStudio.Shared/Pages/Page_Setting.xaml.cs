using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
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
            list_dz_encodecompression.Header = new TwoItem("文件扩展名", "压缩算法");
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
                    string format = "修改压缩方式为{0}";
                    listView.Items.Add(string.Format(format, Package.Dz.DzCompressionFlags.STORE.ToString()));
                    listView.Items.Add(string.Format(format, Package.Dz.DzCompressionFlags.LZMA.ToString()));
                    listView.Items.Add(string.Format(format, Package.Dz.DzCompressionFlags.GZIP.ToString()));
                    listView.Items.Add(string.Format(format, Package.Dz.DzCompressionFlags.BZIP2.ToString()));
                    if (!item.Special)
                    {
                        listView.Items.Add("删除此项");
                    }
                    listView.SelectedIndex = 0;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "请选择你要进行的操作",
                        Content = listView,
                        PrimaryButtonText = "确定",
                        CloseButtonText = "取消"
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
                            DzCompression[DzCompression.IndexOf(item)] = item;
                            GlobalSetting.Singleton.Dz.SetFlags(item.Item1, Package.Dz.DzCompressionFlags.STORE, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 1)
                        {
                            item.Item2 = Package.Dz.DzCompressionFlags.LZMA.ToString();
                            DzCompression[DzCompression.IndexOf(item)] = item;
                            GlobalSetting.Singleton.Dz.SetFlags(item.Item1, Package.Dz.DzCompressionFlags.LZMA, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 2)
                        {
                            item.Item2 = Package.Dz.DzCompressionFlags.GZIP.ToString();
                            DzCompression[DzCompression.IndexOf(item)] = item;
                            GlobalSetting.Singleton.Dz.SetFlags(item.Item1, Package.Dz.DzCompressionFlags.GZIP, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 3)
                        {
                            item.Item2 = Package.Dz.DzCompressionFlags.BZIP2.ToString();
                            DzCompression[DzCompression.IndexOf(item)] = item;
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
                        Title = "请输入后缀名并选择压缩算法",
                        Content = panel,
                        PrimaryButtonText = "确定",
                        CloseButtonText = "取消"
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
                    string format = "修改压缩方式为{0}";
                    listView.Items.Add(string.Format(format, Package.Pak.PakCompressionFlags.STORE.ToString()));
                    listView.Items.Add(string.Format(format, Package.Pak.PakCompressionFlags.ZLIB.ToString()));
                    if (!item.Special)
                    {
                        listView.Items.Add("删除此项");
                    }
                    listView.SelectedIndex = 0;
                    ContentDialog noWifiDialog = new ContentDialog
                    {
                        Title = "请选择你要进行的操作",
                        Content = listView,
                        PrimaryButtonText = "确定",
                        CloseButtonText = "取消"
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
                            PakCompression[PakCompression.IndexOf(item)] = item;
                            GlobalSetting.Singleton.Pak.SetFlags(item.Item1, Package.Pak.PakCompressionFlags.STORE, item.Special);
                            GlobalSetting.Save();
                        }
                        else if (listView.SelectedIndex == 1)
                        {
                            item.Item2 = Package.Pak.PakCompressionFlags.ZLIB.ToString();
                            PakCompression[PakCompression.IndexOf(item)] = item;
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
                        Title = "请输入后缀名并选择压缩算法",
                        Content = panel,
                        PrimaryButtonText = "确定",
                        CloseButtonText = "取消"
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
        }

        private ObservableCollection<TwoItem> _dzcompression = new ObservableCollection<TwoItem>();

        public ObservableCollection<TwoItem> DzCompression => _dzcompression;

        private ObservableCollection<TwoItem> _pakcompression = new ObservableCollection<TwoItem>();

        public ObservableCollection<TwoItem> PakCompression => _pakcompression;

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
