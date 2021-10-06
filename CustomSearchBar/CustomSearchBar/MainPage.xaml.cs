using CustomSearchBar.Fonts;
using CustomSearchBar.Interfaces;
using CustomSearchBar.Services;
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.SideMenu = sideMenu;
        }
        private void SearchBox_focused(object sender, FocusEventArgs e)
        {
            Task.Run(() =>
            {
                _= vm.AnimationMenuIcon(labelMenuIcon, "close");

            });
            vm.ParentContentVisible = false;
            vm.ReceivesEntry = (Entry)sender;
            vm.ColorChange("", true);
            framLanguage.IsVisible = false;

        }

        private void EntrySearch_Completed(object sender, EventArgs e)
        {
            vm.AddWordForRecentSearchList(((Entry)sender).Text);
        }
        // is not usees .
        private void TapOpenSideMenu(object sender, EventArgs e)
        {
            sideMenu.IsPresented  = true;
        }

        private void EntrySearch_Unfocused(object sender, FocusEventArgs e)
        {
            framLanguage.IsVisible = true;
        }
    }
}
