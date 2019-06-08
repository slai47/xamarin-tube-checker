using System;
using System.Collections.Generic;
using NotifyYou.Models;
using NotifyYou.Models.Events;
using NotifyYou.ViewModels;
using Xamarin.Forms;

namespace NotifyYou.Views
{
    public partial class ChannelsPage : ContentPage
    {
        ChannelsViewModel viewModel;

        public ChannelsPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            this.BindingContext = viewModel = new ChannelsViewModel();

            ChannelsListView.ItemsSource = viewModel.Channels;

            ChannelsListView.RefreshCommand = new Command(() =>
            {
                viewModel.CallForAllChannelActivities(true);
                ChannelsListView.IsRefreshing = false;
            });
            ChannelsListView.Footer = new BoxView
            {
                HeightRequest = 80
            };

        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            StoredChannel item = (StoredChannel)e.Item;
            NotificationSetting settings = App.ChannelsDatastore.GetSetting(item.ChannelId);
            NotificationSettings notificationSettingsView = new NotificationSettings(settings);
            Navigation.PushModalAsync(notificationSettingsView);
        }

    }
}
