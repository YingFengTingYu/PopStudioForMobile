using Microsoft.UI.Xaml.Controls;

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
            AddItem(new Page_FileExplorer());
            AddItem(new Page_Reanim());
            AddItem(new Page_Particle());
            AddItem(new Page_Trail());
            AddItem(new Page_Pam());
            AddItem(new Page_Rton());
            FlyoutItemList.SelectedIndex = 0;
            FlyoutItemList.SelectionChanged += Flyout_SelectionChanged;
        }

        private void AddItem(IMenuChoosable page)
        {
            FlyoutItemList.Items.Add(new FlyoutButtonItem
            {
                Page = page
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
        public IMenuChoosable Page { get; set; }
        public override string ToString()
        {
            return Page.Title;
        }
    }
}
