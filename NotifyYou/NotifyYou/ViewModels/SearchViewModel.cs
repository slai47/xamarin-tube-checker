using System;
using System.Collections.Generic;
using NotifyYou.Models;
using Xamarin.Forms;

namespace NotifyYou.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        List<Channel> SearchList;



        public SearchViewModel()
        {
            Title = "Searching";
            SearchList = new List<Channel>();

        }

        public void Search_Clicked(object sender, EventArgs e)
        {
            // call api to get channels with a text.

        }
    }
}
