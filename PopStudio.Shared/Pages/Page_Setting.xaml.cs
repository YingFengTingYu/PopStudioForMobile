using System;
using Microsoft.UI.Xaml.Controls;
using PopStudio.PlatformAPI;

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
        }

        void LoadFont()
        {
            
        }
    }
}
