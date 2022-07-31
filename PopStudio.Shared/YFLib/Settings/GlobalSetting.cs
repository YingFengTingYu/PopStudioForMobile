using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    JsonSerializer.Serialize(
                        stream,
                        Singleton,
                        typeof(GlobalSetting),
                        new SettingContext(new JsonSerializerOptions
                        {
                            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                            WriteIndented = true,
                            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                            Converters = { new JsonStringEnumConverter() }
                        })
                        );
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
                    using (Stream stream = new FileStream(path, FileMode.Open))
                    {
                        Singleton ??= JsonSerializer.Deserialize(
                            stream,
                            typeof(GlobalSetting),
                            new SettingContext(new JsonSerializerOptions
                            {
                                AllowTrailingCommas = true,
                                Converters = { new JsonStringEnumConverter() }
                            })
                            ) as GlobalSetting;
                    }
                    
                }
                catch (Exception)
                {
                }
            }
            Singleton ??= new GlobalSetting();
            Singleton.Rsb ??= new RsbSetting();
            Singleton.Rsb.Init();
            Singleton.Dz ??= new DzSetting();
            Singleton.Dz.Init();
            Singleton.Pak ??= new PakSetting();
            Singleton.Pak.Init();
            Singleton.PtxRsb ??= new PtxRsbSetting();
            Singleton.PtxRsb.Init();
            Singleton.TexTV ??= new TexTVSetting();
            Singleton.TexTV.Init();
            Singleton.Cdat ??= new CdatSetting();
            Singleton.Cdat.Init();
            Singleton.TexIOS ??= new TexIOSSetting();
            Singleton.TexIOS.Init();
            Singleton.Txz ??= new TxzSetting();
            Singleton.Txz.Init();
            Singleton.ImageLabel ??= new ImageLabelSetting();
            Singleton.ImageLabel.Init();
            Singleton.Rton ??= new RtonSetting();
            Singleton.Rton.Init();
            Save();
        }

        [JsonPropertyName("rsb")]
        public RsbSetting Rsb { get; set; }

        [JsonPropertyName("dz")]
        public DzSetting Dz { get; set; }

        [JsonPropertyName("pak")]
        public PakSetting Pak { get; set; }

        [JsonPropertyName("ptx_rsb")]
        public PtxRsbSetting PtxRsb { get; set; }

        [JsonPropertyName("tex_tv")]
        public TexTVSetting TexTV { get; set; }

        [JsonPropertyName("cdat")]
        public CdatSetting Cdat { get; set; }

        [JsonPropertyName("tex_ios")]
        public TexIOSSetting TexIOS { get; set; }

        [JsonPropertyName("txz")]
        public TxzSetting Txz { get; set; }

        [JsonPropertyName("image_label")]
        public ImageLabelSetting ImageLabel { get; set; }

        [JsonPropertyName("rton")]
        public RtonSetting Rton { get; set; }
    }
}
