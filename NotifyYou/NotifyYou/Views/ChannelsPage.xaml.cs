using System;
using System.Collections.Generic;
using NotifyYou.ViewModels;
using Xamarin.Forms;

namespace NotifyYou.Views
{
    public partial class ChannelsPage : ContentPage
    {
        ChannelsViewModel viewModel;

        public ChannelsPage()
        {
            InitializeComponent();

            this.BindingContext = viewModel = new ChannelsViewModel();
        }
    }
}
