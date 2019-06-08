using System;
using System.Collections.Generic;
using NotifyYou.Models;
using NotifyYou.ViewModels;
using Xamarin.Forms;

namespace NotifyYou.Views
{
    public partial class NotificationSettings : ContentPage
    {
        NotificationSettingsViewModel viewModel;

        public NotificationSettings(NotificationSetting setting)
        {
            InitializeComponent();
            BackgroundColor = Color.Transparent;
            this.BindingContext = viewModel = new NotificationSettingsViewModel(setting);

            SetupClicks();
        }

        private void SetupClicks()
        {
            soundSetting.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() => {
                        viewModel.SoundOn = !viewModel.SoundOn;
                     })
                });
            activeSetting.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() => {
                        viewModel.IsActive = !viewModel.IsActive;
                    })
                });
            vibrateSetting.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() => {
                        viewModel.VibrateOn = !viewModel.VibrateOn;
                    })
                });
        }

        void HandleBack(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
