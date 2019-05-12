using System;
using System.Collections.Generic;
using NotifyYou.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NotifyYou.Views
{
    public partial class SettingsPage : ContentPage
    {
        private SettingViewModel viewModel;

        public SettingsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            BindingContext = viewModel = new SettingViewModel();

            BindText();
        }

        private void BindText()
        {
            verisonInfo.ValueText = AppInfo.VersionString + " (" + AppInfo.BuildString + ")";
        }
    }
}
