using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotifyYou.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartTabbedPage : TabbedPage
    {
        public StartTabbedPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}