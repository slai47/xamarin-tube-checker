using System;
using System.Collections.Generic;
using NotifyYou.Models;
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
            string url = "";
            if (!string.IsNullOrEmpty(item.LastVideoId))
            {
                url = "http://youtube.com/watch?v=" + item.LastVideoId;
            } else
            {
                url = "http://youtube.com/channel/" + item.ChannelId;
            }

            Device.OpenUri(new Uri(url));
        }

    }
}
