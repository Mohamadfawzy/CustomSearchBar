using CustomSearchBar.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomSearchBar
{
    public partial class MainPage : ContentPage
    {
        MainPageVM vm;
        public MainPage()
        {
            InitializeComponent();
            vm = BindingContext as MainPageVM;
        }
        private void SearchBox_focused(object sender, FocusEventArgs e)
        {
            vm.MainContentVisible = false;
        }
        private void SearchBox_Unfocused(object sender, FocusEventArgs e)
        {
            vm.MainContentVisible = true;
        }
       
        private void Tap_Go_To_UserPage(object sender, EventArgs e)
        {

        }

        private void Tap_Go_To_Basket(object sender, EventArgs e)
        {

        }

        private void ListV_RecentSearches_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void ListViewRecentSearches_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}
