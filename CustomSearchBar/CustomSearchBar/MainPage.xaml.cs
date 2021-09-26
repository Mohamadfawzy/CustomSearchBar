using CustomSearchBar.Fonts;
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
            MenuIcon.Text = IconFont.Close;
            vm.ReceivesEntry = (Entry)sender;
        }
        private void SearchBox_Unfocused(object sender, FocusEventArgs e)
        {
            //vm.MainContentVisible = true;
        }

        private void ListViewRecentSearches_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
        int x = 0;
        private void ListViewRecentSearches_Scrolled(object sender, ScrolledEventArgs e)
        {
            //if (e.ScrollY > (x + 30) || e.ScrollY < (x - 30))
            //{
            //    EntrySearch.Unfocus();
            //    x = (int)e.ScrollY;
            //}
        }

        private void EntrySearch_Completed(object sender, EventArgs e)
        {
            vm.AddWordForRecentSearchList(((Entry)sender).Text );
        }
        
    }
}
