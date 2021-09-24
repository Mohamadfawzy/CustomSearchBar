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
        public ObservableCollection<string> ListRecentSearch { get; set; }

        public ICommand ExitSearchCommand => new Command(ExecuteExitSearchCommand);
        public ICommand EraseEntrySearshCommand => new Command(ExecuteEraseEntrySearsh);
        public ICommand CancelImage_RecentSearch_Command => new Command<string>(ExecuteCancelImage_RecentSearch_Command);

        public MainPageVM()
        {
            ListRecentSearch = new ObservableCollection<string>();
            AddItemListRecentSearch(SearchDataStore.GetItemsAsync());
        }


        private void ExecuteEraseEntrySearsh()
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
            var filteredResult = SearchDataStore.GetItemsAsync().Where(s => s.Contains(search));
            AddItemListRecentSearch(filteredResult);
        }
        private void AddItemListRecentSearch(IEnumerable<string> list)
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


    }
}
