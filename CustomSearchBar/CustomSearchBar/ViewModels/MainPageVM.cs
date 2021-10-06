using CustomSearchBar.Controllers;
using CustomSearchBar.Fonts;
using CustomSearchBar.Helper;
using CustomSearchBar.Interfaces;
using CustomSearchBar.Resources.Languages;
using CustomSearchBar.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CustomSearchBar.ViewModels
{
    public class MainPageVM : BaseViewModel
    {
        private string entrySearch = "";
        private bool visibleEmptySearch = false;
        private bool parentContentVisible = true;

        public Entry ReceivesEntry = new Entry();
        public SideMenu SideMenu { get; set; }
        public ObservableCollection<string> ListRecentSearch { get; set; }
        public List<string> ClassificationFood
        {
            get
            {
                return new List<string>
                {
                    LanguageResources.MainPage_Classification1,
                    LanguageResources.MainPage_Classification2,
                    LanguageResources.MainPage_Classification3,
                    LanguageResources.MainPage_Classification4,
                };
            }
        }

        public List<MenuFoodModel> FoodMenu
        {
            get
            {
                return new List<MenuFoodModel>
                {
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu1, URL="https://i.pinimg.com/236x/84/04/7e/84047e6ca5c176eafb95ffbe202eb438.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu2, URL="https://i.pinimg.com/236x/5c/28/39/5c28393be382ba635223696135b2a6dc.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu3, URL="https://i.pinimg.com/236x/7b/3c/a8/7b3ca884ace36473a090c52ef048c4bc.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu4, URL="https://i.pinimg.com/236x/57/2e/05/572e059ec90b4c87c1d4c7ab495cc228.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu5, URL="https://i.pinimg.com/236x/00/2b/0c/002b0c1f8667a39b35bbb9cfdd039fce.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu6, URL="https://i.pinimg.com/236x/00/2b/0c/002b0c1f8667a39b35bbb9cfdd039fce.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu1, URL="https://i.pinimg.com/236x/84/04/7e/84047e6ca5c176eafb95ffbe202eb438.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu2, URL="https://i.pinimg.com/236x/5c/28/39/5c28393be382ba635223696135b2a6dc.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu3, URL="https://i.pinimg.com/236x/7b/3c/a8/7b3ca884ace36473a090c52ef048c4bc.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu4, URL="https://i.pinimg.com/236x/57/2e/05/572e059ec90b4c87c1d4c7ab495cc228.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu5, URL="https://i.pinimg.com/236x/00/2b/0c/002b0c1f8667a39b35bbb9cfdd039fce.jpg"},
                    new MenuFoodModel{Name =   LanguageResources.MainPage_FoodMenu6, URL="https://i.pinimg.com/236x/00/2b/0c/002b0c1f8667a39b35bbb9cfdd039fce.jpg"},

                };
            }
        }
        public bool ParentContentVisible
        {
            get => parentContentVisible;
            set { SetProperty(ref parentContentVisible, value); }
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
        public bool VisibleEmptySearch
        {
            get => visibleEmptySearch;
            set { SetProperty(ref visibleEmptySearch, value); }
        }


        // commands
        public ICommand ChangeLaguageCommand => new Command<Label>(ChangeLaguage);
        public ICommand ExitSearchCommand => new Command(ExecuteExitSearchCommand);
        public ICommand EraseEntrySearshCommand => new Command(EraseEntrySearch);
        public ICommand CancelImage_RecentSearch_Command => new Command<string>(ExecuteCancelImage_RecentSearch_Command);
        public ICommand MenuIconCommand => new Command<Label>(EMenuIcon_Command);
        // constructor
        public MainPageVM()
        {
            ListRecentSearch = new ObservableCollection<string>();
            AddResultsInTheRecentSearchList(SearchDataStore.GetItems());
        }

        // Methods
        #region private Methonds
        private async void ChangeLaguage(Label label)
        {
            if (label.Text == "EN")
            {
                await LocalizationResourceManager.SetLanguageAsync("en");
            }
            else
            {
                await LocalizationResourceManager.SetLanguageAsync("ar");
            }
        }
        private void EMenuIcon_Command(Label sender)
        {
            var label = sender as Xamarin.Forms.Label;
            if (label.Text == IconFont.Close)
            {
                _ = AnimationMenuIcon(label, "menu");
                ParentContentVisible = true;
                ReceivesEntry.Unfocus();
                EraseEntrySearch();
                ColorChange("#cb1901", true);
            }
            else
            {
                // run SideMenuView
                SideMenu.IsPresented = true;
            }
        }

        private void EraseEntrySearch()
        {
            EntrySearch = "";
        }
        private void ExecuteExitSearchCommand()
        {
            ParentContentVisible = true;
            OnPropertyChanged("MainContentVisible");
        }
        private void getReseltFromSearch(string search)
        {
            var ListOfResult = SearchDataStore.GetItems().Where(s => s.ToLower().Contains(search.ToLower())).OrderBy((x) => x);
            var ListOfResult1 = SearchDataStore.GetItems().Where(s => s.ToLower().Contains(search.ToLower())).OrderBy((x) => x);
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
        #endregion

        #region public Methods
        public void ColorChange(string hexCode = null, bool state = false)
        {
            Color color = state & !String.IsNullOrEmpty(hexCode) ? Color.FromHex(hexCode) : Color.FromHex("#000000");
            var status = DependencyService.Get<IMyEnvironment>();
            status.SetStatusBarColor(color, true);
            status.SetNavigationBarColor(color);
        }
        public async Task<bool> AnimationMenuIcon(Label label, string state)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            var an = new Animation();
            bool isFinsh = false;
            int start1, end2;
            if (state == "menu")
            {
                start1 = 180;
                end2 = 0;
            }
            else
            {
                start1 = 0;
                end2 = 180;
            }
            an.Add(0, 0.50, new Animation(v => label.Rotation = v, start1, 90,
               finished: () =>
               {
                   label.Text = state == "menu" ? IconFont.ViewHeadline : IconFont.Close;
               }));
            an.Add(0.40, 0.50, new Animation(v => label.Opacity = v, 1, 0.5));
            an.Add(0.51, 0.60, new Animation(v => label.Opacity = v, 0.5, 1));
            an.Add(0.47, 1, new Animation(v => label.Rotation = v, 90, end2,
                finished: () =>
                {
                    isFinsh = true;
                }));

            an.Commit(owner: label, "MenuIcon", length: 250);
            taskCompletionSource.SetResult(isFinsh);
            return await taskCompletionSource.Task;
        }

        public void AddWordForRecentSearchList(string item)
        {
            SearchDataStore.SetItem(item);
            RefreshList();
        }
        #endregion



    }
    // new class
    public class MenuFoodModel
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
