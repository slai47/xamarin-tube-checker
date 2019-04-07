using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NotifyYou.API;
using NotifyYou.Models;
using NotifyYou.Models.Channel;
using Xamarin.Forms;

namespace NotifyYou.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public ObservableCollection<YoutubeChannel> SearchList;

        public SearchViewModel()
        {
            Title = "Searching";
            SearchList = new ObservableCollection<YoutubeChannel>();

        }

        public void Search(string search)
        {
            // call api to get channels with a text.
            IYoutube api = new YoutubeApi();
            var call = api.GetChannels(search);
            foreach (var item in call.Result.items)
            {
                SearchList.Add(item);
            }
            Console.Out.WriteLine("SearchList " + SearchList.Count);
        }
    }
}
