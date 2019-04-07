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

        public SearchPage()
        {
            InitializeComponent();

            BindingContext = this.searchViewModel = new SearchViewModel();

            SearchListView.ItemsSource = searchViewModel.SearchList;
        }

        public void Search_Clicked(object sender, EventArgs e)
        {
            searchViewModel.Search(SearchText.Text);
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            
        }

        public void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {

        }
    }
}
