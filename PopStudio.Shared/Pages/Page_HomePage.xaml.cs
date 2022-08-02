using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using PopStudio.PlatformAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PopStudio.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page_HomePage : Page, IMenuChoosable
    {
        public string Title { get; set; } = YFString.GetString("HomePage_Title");

        public static string StaticTitle { get; set; } = YFString.GetString("HomePage_Title");

        public Action OnShow { get; set; }

        public Page_HomePage()
        {
            this.InitializeComponent();
            LoadFont();
        }

        void LoadFont()
        {
            label_begin.Text = YFString.GetString("HomePage_Begin");
            label_function.Text = YFString.GetString("HomePage_Function");
            label_permission.Text = YFString.GetString("HomePage_Permission");
            label_agreement.Text = YFString.GetString("HomePage_Agreement");
            label_ver.Text = string.Format(YFString.GetString("HomePage_Version"), GlobalConst.APPVERSION);
            label_author_string.Text = YFString.GetString("HomePage_Author_String");
            label_author.Text = YFString.GetString("HomePage_Author");
            label_thanks_string.Text = YFString.GetString("HomePage_Thanks_String");
            label_thanks.Text = YFString.GetString("HomePage_Thanks");
            label_qqgroup_string.Text = YFString.GetString("HomePage_QQGroup_String");
            label_qqgroup.Text = YFString.GetString("HomePage_QQGroup");
            label_course_string.Text = YFString.GetString("HomePage_Course_String");
            label_course.Text = YFString.GetString("HomePage_Course");
            label_appnewnotice_string.Text = YFString.GetString("HomePage_AppNewNotice_String");
            label_appnewnotice.Text = GlobalConst.APPNOTICE;
        }
    }
}
