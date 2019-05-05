using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotifyYou.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : Xamarin.Forms.TabbedPage
    {
        public MainTabbedPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            On<Android>().SetBarSelectedItemColor(Color.White); 
            On<Android>().SetBarItemColor(Color.Gray); 
        }
    }
}
