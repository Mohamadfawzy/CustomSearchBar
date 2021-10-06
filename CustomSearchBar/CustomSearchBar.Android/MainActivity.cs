
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using CustomSearchBar.Droid.Renderers;
using Xamarin.Forms.Platform.Android;

namespace CustomSearchBar.Droid
{
    [Activity(Label = "CustomSearchBar", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Window.SetNavigationBarColor(Xamarin.Forms.Color.FromHex("#cb1901").ToAndroid());
            this.SetStatusBarColor(Xamarin.Forms.Color.FromHex("#cb1901").ToAndroid());

            // to open settings
            //Intent intentOpenSettings = new Intent();
            //intentOpenSettings.SetAction(Android.Provider.Settings.ActionAirplaneModeSettings);
            //Android.App.Application.Context.StartActivity(intentOpenSettings);

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        #region this code implement => How to keep soft keyboard always open in Xamarin Forms
        private bool _lieAboutCurrentFocus;
        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            var focused = CurrentFocus;
            bool customEntryRendererFocused = focused != null && focused.Parent is CustomEntryRenderer;

            _lieAboutCurrentFocus = customEntryRendererFocused;
            var result = base.DispatchTouchEvent(ev);
            _lieAboutCurrentFocus = false;
            return result;
        }
        public override Android.Views.View CurrentFocus
        {
            get
            {
                if (_lieAboutCurrentFocus)
                {
                    return null;
                }

                return base.CurrentFocus;
            }
        }
        #endregion
    } // end MainActivity class
}