using CustomSearchBar.Helper;
using CustomSearchBar.Resources.Languages;
using System;
using System.Globalization;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomSearchBar
{
    public partial class App : Application
    {
        CultureInfo language;
        public App()
        {
            InitializeComponent();
            //string x = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            //language = new CultureInfo(Xamarin.Essentials.Preferences.Get("CurrentLanguage", "ar"));
            //Thread.CurrentThread.CurrentUICulture = language;
            //LanguageResources.Culture = language;
           
            //MainPage = new MainPage();

            //if (Xamarin.Essentials.Preferences.Get("CurrentLanguage", x) == "ar")
            //{
            //    MainPage.FlowDirection = FlowDirection.RightToLeft;
            //    //Services.AllServices.lang = "ar";
            //}
            //else
            //{
            //    MainPage.FlowDirection = FlowDirection.LeftToRight;

            //}


            var lang = LocalizationResourceManager.SetLanguage(isMainPage:true);
            MainPage = new MainPage();
            LocalizationResourceManager.ChangeDirection(lang);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
