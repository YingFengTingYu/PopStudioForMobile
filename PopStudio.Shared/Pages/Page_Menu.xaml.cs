using Microsoft.UI.Xaml.Controls;
using System;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_Menu : Page
    {
        public Page_Menu()
        {
            this.InitializeComponent();
            AddItem(new Page_HomePage());
            AddItem(() => new Page_FileExplorer(), Page_FileExplorer.StaticTitle);
            AddItem(new Page_Package());
            AddItem(() => new Page_Image(), Page_Image.StaticTitle);
            AddItem(new Page_Reanim());
            AddItem(new Page_Particle());
            AddItem(new Page_Trail());
            AddItem(new Page_Pam());
            AddItem(new Page_Rton());
            AddItem(() => new Page_Setting(), Page_Setting.StaticTitle);
            FlyoutItemList.SelectedIndex = 0;
            FlyoutItemList.SelectionChanged += Flyout_SelectionChanged;
        }

        private void AddItem(Func<IMenuChoosable> pagector, string title)
        {
            FlyoutItemList.Items.Add(new FlyoutButtonItem
            {
                GetPage = pagector,
                Title = title
            });
        }

        private void AddItem(IMenuChoosable page)
        {
            FlyoutItemList.Items.Add(new FlyoutButtonItem
            {
                Page = page,
                Title = page.Title
            });
        }

        public void InitPage()
        {
            ShowPage((FlyoutItemList.SelectedItem as FlyoutButtonItem)?.Page, false);
        }

        public void ShowPage(IMenuChoosable page)
        {
            if (page is Page windowPage)
            {
                MainPage.Singleton.LoadPage(page.Title, windowPage);
                page.OnShow?.Invoke();
            }
        }

        public void ShowPage(IMenuChoosable page, bool v)
        {
            if (page is Page windowPage)
            {
                MainPage.Singleton.LoadPage(page.Title, windowPage, v);
                page.OnShow?.Invoke();
            }
        }

        private void Flyout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowPage(((sender as ListView)?.SelectedItem as FlyoutButtonItem)?.Page);
        }
    }

    public class FlyoutButtonItem
    {
        private IMenuChoosable _page;

        public IMenuChoosable Page
        {
            get => _page ??= GetPage?.Invoke();
            set => _page = value;
        }

        public string Title { get; set; }

        public Func<IMenuChoosable> GetPage { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
