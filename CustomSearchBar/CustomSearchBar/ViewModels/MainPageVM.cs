using CustomSearchBar.Fonts;
using CustomSearchBar.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomSearchBar.ViewModels
{
    public class MainPageVM : BaseViewModel
    {
        private string entrySearch = "";
        private bool mainContentVisible = true;
        public bool MainContentVisible
        {
            get => mainContentVisible;
            set { SetProperty(ref mainContentVisible, value); }
        }
        public string EntrySearch
        {
            get => entrySearch;
            set
            {
                if (entrySearch == value) return;
                entrySearch = value;
                getReseltFromSearch(EntrySearch);
                OnPropertyChanged(nameof(EntrySearch));
            }
        }
        private bool visibleEmptySearch = false;
        public bool VisibleEmptySearch
        {
            get => visibleEmptySearch;
            set { SetProperty(ref visibleEmptySearch, value); }
        }
        public Entry ReceivesEntry = new Entry();
        public ObservableCollection<string> ListRecentSearch { get; set; }

        public ICommand ExitSearchCommand => new Command(ExecuteExitSearchCommand);
        public ICommand EraseEntrySearshCommand => new Command(EraseEntrySearch);
        public ICommand CancelImage_RecentSearch_Command => new Command<string>(ExecuteCancelImage_RecentSearch_Command);
        public ICommand MenuIcon_Command => new Command(EMenuIcon_Command);

        private async void EMenuIcon_Command(object sender)
        {
            var label = sender as Xamarin.Forms.Label;
            await label.RotateTo(180);
            if (label.Text == IconFont.Close)
            {
                MainContentVisible = true;
                label.Text = IconFont.ViewHeadline;
                ReceivesEntry.Unfocus();
                EraseEntrySearch();
            }
            else
            {
                // run SideMenuView
            }
            label.Rotation = 0;
        }

        public MainPageVM()
        {
            ListRecentSearch = new ObservableCollection<string>();
            AddResultsInTheRecentSearchList(SearchDataStore.GetItems());
        }
        public void AddWordForRecentSearchList(string item)
        {
            SearchDataStore.SetItem(item);
            RefreshList();
        }

        private void EraseEntrySearch()
        {
            EntrySearch = "";
        }
        private void ExecuteExitSearchCommand()
        {
            MainContentVisible = true;
            OnPropertyChanged("MainContentVisible");
        }
        private void getReseltFromSearch(string search)
        {
            var ListOfResult = SearchDataStore.GetItems().Where(s => s.Contains(search)).OrderBy((x) => x);
            if (ListOfResult.Count() < 1)
                VisibleEmptySearch = true;
            else
                VisibleEmptySearch = false;
            AddResultsInTheRecentSearchList(ListOfResult);
        }
        private void AddResultsInTheRecentSearchList(IEnumerable<string> list)
        {
            ListRecentSearch.Clear();
            foreach (var item in list)
            {
                ListRecentSearch.Add(item);
            }
        }

        private void ExecuteCancelImage_RecentSearch_Command(string item)
        {
            ListRecentSearch.Remove(item);
        }

        private void RefreshList()
        {
            ListRecentSearch.Clear();
            // get items froms Repository
            var ListOfResult = SearchDataStore.GetItems().OrderBy((x) => x);
            //add item for List
            foreach (var item in ListOfResult)
            {
                ListRecentSearch.Add(item);
            }
        }

    }
}
