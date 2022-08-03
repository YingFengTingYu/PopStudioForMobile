using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using System;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PopStudio.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public static MainWindow Singleton;
        private AppWindow m_AppWindow;
        public static nint Handle => WindowNative.GetWindowHandle(Singleton);

        public MainWindow()
        {
            this.InitializeComponent();
            Singleton = this;

            m_AppWindow = GetAppWindowForCurrentWindow();
            m_AppWindow.Title = "PopStudio";

            SetTitleBarColors();
        }

        private bool SetTitleBarColors()
        {
            // Check to see if customization is supported.
            // Currently only supported on Windows 11.
            if (AppWindowTitleBar.IsCustomizationSupported())
            {
                if (m_AppWindow is null)
                {
                    m_AppWindow = GetAppWindowForCurrentWindow();
                }
                var titleBar = m_AppWindow.TitleBar;

                // Set active window colors
                titleBar.ForegroundColor = Colors.White;
                titleBar.BackgroundColor = Colors.CornflowerBlue;
                titleBar.ButtonForegroundColor = Colors.White;
                titleBar.ButtonBackgroundColor = Colors.CornflowerBlue;
                titleBar.ButtonHoverForegroundColor = Colors.LightGray;
                titleBar.ButtonHoverBackgroundColor = Colors.CornflowerBlue;
                titleBar.ButtonPressedForegroundColor = Colors.LightGray;
                titleBar.ButtonPressedBackgroundColor = Colors.CornflowerBlue;

                // Set inactive window colors
                titleBar.InactiveForegroundColor = Colors.White;
                titleBar.InactiveBackgroundColor = Colors.CornflowerBlue;
                titleBar.ButtonInactiveForegroundColor = Colors.LightGray;
                titleBar.ButtonInactiveBackgroundColor = Colors.CornflowerBlue;

                titleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;

                return true;
            }
            return false;
        }

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(wndId);
        }
    }
}
