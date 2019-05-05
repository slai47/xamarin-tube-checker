using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace NotifyYou.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartTabbedPage : Xamarin.Forms.TabbedPage
    {
        public StartTabbedPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            On<Android>().SetBarSelectedItemColor(Color.White);
            On<Android>().SetBarItemColor(Color.Gray);
        }
    }
}