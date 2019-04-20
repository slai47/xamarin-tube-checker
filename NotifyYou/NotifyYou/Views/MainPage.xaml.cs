using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotifyYou.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            // Children.Insert(0, FeedPage.cs) Use this to add a child if we need it. 
        }
    }
}