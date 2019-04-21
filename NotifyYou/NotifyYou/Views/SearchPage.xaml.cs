using System;
using System.Collections.Generic;
using System.Linq;
using NotifyYou.Models.Channel;
using NotifyYou.Services;
using NotifyYou.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace NotifyYou.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        SearchViewModel searchViewModel;

        public SearchPage()
        {
            InitializeComponent();

            BindingContext = this.searchViewModel = new SearchViewModel();

            SearchListView.ItemsSource = searchViewModel.SearchList;

            SearchText.Completed += (sender, e) =>
            {
                Search_Clicked(sender, e);
            };

            SearchProgress.SetBinding(IsVisibleProperty, nameof(searchViewModel.IsProgressVisible));
            SearchButton.SetBinding(IsVisibleProperty, nameof(searchViewModel.IsButtonVisible));
        }

        public void Search_Clicked(object sender, EventArgs e)
        {
            searchViewModel.Search(SearchText.Text);
        }


        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            YoutubeChannel item = (YoutubeChannel)e.Item;
            bool isActive = App.channelsDatastore.GetAllChannels().Any(channel => channel.Id == item.ChannelId);
            item.IsActive = !isActive;
            if (!isActive)
                App.channelsDatastore.AddUpdate(new Models.StoredChannel(item));
            else
                App.channelsDatastore.Delete(item.ChannelId);
        }
    }
}
