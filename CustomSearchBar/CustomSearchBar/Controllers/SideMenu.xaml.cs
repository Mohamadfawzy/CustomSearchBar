using CustomSearchBar.Fonts;
using CustomSearchBar.Helper;
using CustomSearchBar.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace CustomSearchBar.Controllers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideMenu : ContentView
    {
        private const int ItemsCount = 12;
        private double xOffset, sign, SideMenuWidth = 0.0;
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

        public string[] ItemsIconsArray
        {
            get
            {
                if (this.FlowDirection == FlowDirection.RightToLeft)
                {
                    return ItemsIconsArrayLeft;
                }
                else
                {
                    return ItemsIconsArrayRight;
                }
            }
        }
        public string[] ItemsIconsArrayRight
        {
            get => new string[ItemsCount]
            {
                IconFont.AccountEditOutline  ,
                IconFont.Cart,
                IconFont.Heart,
                IconFont.CreditCardOutline,
                IconFont.Bell ,
                IconFont.ViewModule,
                IconFont.CameraTimer,
                IconFont.CogOutline,
                IconFont.MessageText,
                IconFont.HelpCircleOutline ,
                IconFont.InformationOutline ,
                IconFont.LogoutVariant ,
            };
        }
        public string[] ItemsIconsArrayLeft
        {
            get => new string[ItemsCount]
            {
                IconFont.AccountEditOutline,
                IconFont.Cart,
                IconFont.Heart,
                IconFont.CreditCardOutline,
                IconFont.Bell ,
                IconFont.ViewModule,
                IconFont.CameraTimer,
                IconFont.CogOutline,
                IconFont.MessageText,
                IconFont.HelpCircleOutline ,
                IconFont.InformationOutline ,
                IconFont.LogoutVariant ,
            };
        }

        public string[] ItemsTitlesArray
        {
            get => new string[ItemsCount]
            {
                LanguageResources.SideMenu_ItemTitle_01,
                LanguageResources.SideMenu_ItemTitle_02,
                LanguageResources.SideMenu_ItemTitle_03,
                LanguageResources.SideMenu_ItemTitle_04,
                LanguageResources.SideMenu_ItemTitle_05,
                LanguageResources.SideMenu_ItemTitle_06,
                LanguageResources.SideMenu_ItemTitle_07,
                LanguageResources.SideMenu_ItemTitle_08,
                LanguageResources.SideMenu_ItemTitle_09,
                LanguageResources.SideMenu_ItemTitle_10,
                LanguageResources.SideMenu_ItemTitle_11,
                LanguageResources.SideMenu_ItemTitle_12,
            };
        }
        #region BindableProperty
        public static readonly BindableProperty IsPresentedProperty =
            BindableProperty.Create(
                nameof(IsPresented),
                typeof(bool),
                typeof(SideMenu),
                false,
                BindingMode.TwoWay,
                null);
        #endregion
        public bool IsPresented
        {
            get => (bool)GetValue(IsPresentedProperty);
            set
            {
                bool old = IsPresented;
                SetValue(IsPresentedProperty, value);
                if (!value) //false
                {
                    Close();
                }
                else // true
                {
                    Open();
                }
            }
        }
        public ICommand ItemTapd => new Command(() => App.Current.MainPage.DisplayAlert("T", "message", "ok", "can"));
        public SideMenu()
        {
            InitializeComponent();
            InitializeDirection(LocalizationResourceManager.CurrentLanguage);
        }
        private void InitializeDirection(string lang)
        {
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;
            var xamarinWidth = width / mainDisplayInfo.Density;
            SideMenuWidth = xamarinWidth * 75 / 100;
            if (lang == "ar")
            {
                parentSideMenu.TranslationX = SideMenuWidth;
                frameSM.CornerRadius = new CornerRadius(30, 0, 0, 0);
                sign = 1;
            }
            else
            {
                sign = -1;
                SideMenuWidth *= sign;
                parentSideMenu.TranslationX = SideMenuWidth;
                backgroundBhind.Opacity = 0;
                frameSM.CornerRadius = new CornerRadius(0, 30, 0, 0);
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IsPresented = false;
        }
        async void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (!watch.IsRunning)
            {
                watch = new System.Diagnostics.Stopwatch();
                watch.Start();
            }
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    break;
                case GestureStatus.Running:
                    OnPanRunning(e.TotalX);
                    break;
                case GestureStatus.Completed:
                    await OnPanCompleted(e.TotalX);
                    break;
            }
        }

        void OnPanRunning(double TotalX)
        {
            // This condition keeps the list from getting out of place.
            if (TotalX + xOffset > 0 && sign < 0)
            {
                frameSM.TranslationX = sign * 0.1; // LTR-EN
            }
            else if (TotalX + xOffset < 0 && sign > 0)
            {
                frameSM.TranslationX = sign * 0.1; // RTL-AR
            }
            else
            {
                frameSM.TranslationX = xOffset + TotalX;
            }
            backgroundBhind.Opacity = (100 - (Math.Abs(TotalX) / 304 * 100)) / 100;
        }
        async Task OnPanCompleted(double TotalX)
        {
            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            if (elapsedTime < 90)
            {
                Close();
            }
            if (Math.Abs(frameSM.TranslationX) > frameSM.Width / 2)
            {
                Close();
            }
            else
            {
                await frameSM.TranslateTo(-0.1, 0);
            }
            xOffset = frameSM.TranslationX;
            //Console.WriteLine($"Complete. x: {(int)e.TotalX} id: {e.GestureId} elapedMS: {elapsedTime} offset: {xOffset}");

        }

        // methods
        private async void Open()
        {
            this.IsVisible = true;
            frameSM.TranslationX = sign * 0.1;
            var start = SideMenuWidth;
            parentSideMenu.Animate("am2", new Animation((x) => parentSideMenu.TranslationX = x, start, 0));
            parentSideMenu.Animate("backgroundBhind", new Animation((x) => backgroundBhind.Opacity = x, 0, 1));
            for (var i = 0; i < contenairItems.Children.Count; i++)
            {
                var item = contenairItems.Children[i] as SideMenuItems;
                item.IsAnimation = true;
                await Task.Delay(50);
            }
            //await testAnimation.ScaleXTo(1, 600,Easing.SinIn);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Console.WriteLine($"X: (int){e.ScrollX}");

        }

        private async void Close()
        {
            SideMenuWidth = sign * parentSideMenu.Width;
            await parentSideMenu.TranslateTo(SideMenuWidth, 0);
            frameSM.TranslationX = 0;
            backgroundBhind.Opacity = 0;
            xOffset = 0;
            //await Task.Delay(250);
            this.IsVisible = false;
            for (var i = 0; i < contenairItems.Children.Count; i++)
            {
                var item = contenairItems.Children[i] as SideMenuItems;
                item.IsAnimation = false;
            }
        }

        private void AnimteItems()
        {

        }

    }
}