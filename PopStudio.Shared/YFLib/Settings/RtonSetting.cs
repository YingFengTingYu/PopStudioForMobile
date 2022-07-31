namespace PopStudio.Settings
{
    public class RtonSetting
    {
        public string Cipher { get; set; }

        public void Init()
        {
            Cipher ??= string.Empty;
        }
    }
}
