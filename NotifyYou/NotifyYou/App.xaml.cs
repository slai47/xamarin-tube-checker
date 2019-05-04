using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NotifyYou.Views;
using NotifyYou.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NotifyYou
{
    public partial class App : Application
    {
        public static IChannelsDataStore ChannelsDatastore;

        public App()
        {
            InitializeComponent();
            ChannelsDatastore = new YoutubeChannelsDataStore(false);
            ChannelsDatastore.Init();
            if(ChannelsDatastore.GetAllChannels().Count > 0)
            {
                MainPage = new MainTabbedPage();
            } else
            {
                MainPage = new StartTabbedPage();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
