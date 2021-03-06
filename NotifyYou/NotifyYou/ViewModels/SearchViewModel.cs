﻿using System;
using System.Collections.ObjectModel;
using NotifyYou.API;
using NotifyYou.Models.Channel;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyYou.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public ObservableCollection<YoutubeChannel> SearchList;
        public bool _isProgressVisible;
        public bool IsProgressVisible { get {
                return _isProgressVisible;
            }
            set
            {
                _isProgressVisible = value;
                OnPropertyChanged(nameof(IsProgressVisible));
            } 
        }
        public bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get
            {
                return _isButtonVisible;
            }
            set
            {
                _isButtonVisible = value;
                OnPropertyChanged(nameof(IsButtonVisible));
            }
        }
        

        public SearchViewModel()
        {
            Title = "Searching";
            SearchList = new ObservableCollection<YoutubeChannel>();
            Toggle(false);
        }

        public void Search(string search)
        {
            if (search.Any())
            {
                Toggle(true);
                if (SearchList.Count > 0)
                {
                    SearchList.Clear();
                }
                Task.Run( async () =>
                {
                    // call api to get channels with a text.
                    IYoutube api = new YoutubeApi();
                    var call = await api.GetChannels(search);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        foreach (var item in call.items)
                        {
                            item.IsActive = App.ChannelsDatastore.Exists(item.ChannelId);
                            SearchList.Add(item);
                        }
                        Console.Out.WriteLine("SearchList " + SearchList.Count);
                        Toggle(false);
                    });

                });
            }
        }

        public void Toggle(bool showProgress)
        {
            IsProgressVisible = showProgress;
            IsButtonVisible = !showProgress;
        }
    }
}
