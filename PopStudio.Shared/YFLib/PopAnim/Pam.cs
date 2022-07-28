using PopStudio.Plugin;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using static PopStudio.PlatformAPI.YFFileSystem;

namespace PopStudio.PopAnim
{
    /// <summary>
    /// It's all from Disassembling PVZ2 and Zuma's Revenge!
    /// </summary>
    internal class Pam
    {
        public static void Encode(YFFile inFile, YFFile outFile)
        {
            PopAnimInfo pam;
            using (Stream stream = inFile.OpenAsStream())
            {
                pam = JsonSerializer.Deserialize(
                    stream,
                    typeof(PopAnimInfo),
                    new PopAnimJsonContext(new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true,
                    })
                    ) as PopAnimInfo;
            }
            using (BinaryStream bs = outFile.CreateAsBinaryStream())
            {
                pam.Write(bs);
            }
        }

        public static void Decode(YFFile inFile, YFFile outFile)
        {
            PopAnimInfo pam = new PopAnimInfo();
            using (BinaryStream bs = inFile.OpenAsBinaryStream())
            {
                pam.Read(bs);
            }
            using (Stream stream = outFile.CreateAsStream())
            {
                JsonSerializer.Serialize(
                    stream,
                    pam,
                    typeof(PopAnimInfo),
                    new PopAnimJsonContext(new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true,
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                    })
                    );
            }
        }
    }
}
