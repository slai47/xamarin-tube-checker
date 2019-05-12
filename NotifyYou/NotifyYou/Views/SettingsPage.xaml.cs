using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NotifyYou.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindText();
        }

        private void BindText()
        {
            verisonInfo.ValueText = AppInfo.VersionString + " (" + AppInfo.BuildString + ")";
        }
    }
}
