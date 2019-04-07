using System;
using System.Collections.Generic;
using NotifyYou.API;
using NotifyYou.Models;
using NotifyYou.Models.Channel;
using Xamarin.Forms;

namespace NotifyYou.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        List<YoutubeChannel> SearchList;

        public SearchViewModel()
        {
            Title = "Searching";
            SearchList = new List<YoutubeChannel>();

        }

        public void Search(string search)
        {
            // call api to get channels with a text.
            IYoutube api = new YoutubeApi();
            var call = api.GetChannels(search);
            SearchList = call.Result.items;
            Console.Out.Write("SearchList = " + SearchList.Count);
        }
    }
}
