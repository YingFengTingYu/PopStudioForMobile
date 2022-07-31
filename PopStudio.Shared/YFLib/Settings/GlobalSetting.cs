using System;
using System.IO;
using System.Text.Json;

namespace PopStudio.Settings
{
    public class GlobalSetting
    {
        public static GlobalSetting Singleton;

        public static object thisLock = new object();

        public static void Save()
        {
            lock (thisLock)
            {
                string path = Path.Combine(
                    Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                    "PopStudioSetting(Type(GlobalSetting)_Name(Singleton))");
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    JsonSerializer.Serialize(fs, Singleton, SettingContext.Default.GlobalSetting);
                }
            }
        }

        public static void Init()
        {
            Singleton = null;
            string path = Path.Combine(
                    Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                    "PopStudioSetting(Type(GlobalSetting)_Name(Singleton))");
            if (File.Exists(path))
            {
                try
                {
                    Singleton ??= JsonSerializer.Deserialize(File.ReadAllText(path), SettingContext.Default.GlobalSetting);
                }
                catch (Exception)
                {

                }
            }
            Singleton ??= new GlobalSetting();
            Singleton.PtxRsbFormat ??= new PtxRsbSetting();
            Singleton.PtxRsbFormat.Init();
            Singleton.TexTVFormat ??= new TexTVSetting();
            Singleton.TexTVFormat.Init();
            Singleton.CdatFormat ??= new CdatSetting();
            Singleton.CdatFormat.Init();
            Singleton.TexIOSFormat ??= new TexIOSSetting();
            Singleton.TexIOSFormat.Init();
            Singleton.TxzFormat ??= new TxzSetting();
            Singleton.TxzFormat.Init();
            Singleton.ImageLabelConvertor ??= new ImageLabelSetting();
            Singleton.ImageLabelConvertor.Init();
            Singleton.RtonCipher ??= new RtonSetting();
            Singleton.RtonCipher.Init();
            Save();
        }

        public PtxRsbSetting PtxRsbFormat { get; set; }

        public TexTVSetting TexTVFormat { get; set; }

        public CdatSetting CdatFormat { get; set; }

        public TexIOSSetting TexIOSFormat { get; set; }

        public TxzSetting TxzFormat { get; set; }

        public ImageLabelSetting ImageLabelConvertor { get; set; }

        public RtonSetting RtonCipher { get; set; }
    }
}
