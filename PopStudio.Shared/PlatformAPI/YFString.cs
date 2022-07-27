using Windows.ApplicationModel.Resources;

namespace PopStudio.PlatformAPI
{
    public static class YFString
    {
        private static ResourceLoader _resourceLoader = ResourceLoader.GetForViewIndependentUse();

        public static string GetString(string index) => _resourceLoader.GetString(index);
    }
}
