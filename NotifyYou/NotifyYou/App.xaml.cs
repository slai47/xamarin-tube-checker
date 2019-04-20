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
        public static IChannelsDataStore channelsDatastore;

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            channelsDatastore = new YoutubeChannelsDataStore(false);
            //channelsDatastore.Init();
            //channelsDatastore.InitDb();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            channelsDatastore.Save();
            //channelsDatastore.SaveDb(); // Future work
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
