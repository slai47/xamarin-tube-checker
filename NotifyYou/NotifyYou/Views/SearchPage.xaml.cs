using System;
using System.Collections.Generic;
using NotifyYou.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotifyYou.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        SearchViewModel searchViewModel;

        public SearchPage(SearchViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.searchViewModel = viewModel;
        }

        public void Search_Clicked(object sender, EventArgs e)
        {

        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {

        }
    }
}
