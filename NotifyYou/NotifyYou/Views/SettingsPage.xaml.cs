using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NotifyYou.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
