using CustomSearchBar.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomSearchBar.Controllers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SideMenuItems : StackLayout
    {
        public ICommand ItemTappedCommand => new Command<string>(ItemTapped);
        public SideMenuItems()
        {
            InitializeComponent();
        }
        #region BindableProperty
        public static readonly BindableProperty ItemIdProperty =
            BindableProperty.Create(
                nameof(ItemId),
                typeof(string),
                typeof(SideMenuItems),
                "null",
                BindingMode.TwoWay,
                null);
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(
                nameof(Icon),
                typeof(string),
                typeof(SideMenuItems),
                string.Empty,
                BindingMode.TwoWay,
                null);
        public static readonly BindableProperty TitleProperty =
           BindableProperty.Create(
               nameof(Title),
               typeof(string),
               typeof(SideMenuItems),
               string.Empty,
               BindingMode.TwoWay,
               null);
        public static readonly BindableProperty IsAnimationProperty =
           BindableProperty.Create(
               nameof(IsAnimation),
               typeof(bool),
               typeof(SideMenuItems),
               false,
               BindingMode.TwoWay,
               null);
        #endregion

        #region Property
        // Id
        public string ItemId
        {
            get => (string)GetValue(ItemIdProperty);
            set { SetValue(ItemIdProperty, value); }
        }
        //Icon
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set { SetValue(IconProperty, value); }
        }
        // Title
       
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set { SetValue(TitleProperty, value); }
        }
        // Is Animation
        public bool IsAnimation
        {
            get => (bool)GetValue(IsAnimationProperty);
            set
            {
                SetValue(IsAnimationProperty, value);
                if (!value) //false
                {
                    FinishAnimation();
                }
                else // true
                {
                    StartAnimation();
                }
            }
        }
        #endregion

        #region Methods
        private void StartAnimation()
        {
            this.Opacity = 1;
            var an = new Animation();
            an.Add(0, 1, new Animation((x) => testAnimation.TranslationY = x, -10, 0));
            an.Add(0, 1, new Animation((x) => testAnimation.Rotation = x, -10, 0));
            an.Commit(testAnimation, "an1", 16, 250, Easing.SinIn);
        }

        private void FinishAnimation()
        {
            CancelAnimation(this);
            this.Opacity = 0;
            testAnimation.TranslationY = -10;
            testAnimation.Rotation = -10;
        }
        private void ItemTapped(string id)
        {
            Console.WriteLine($"ItemTappedCommand id= {id}");
        }
        public void CancelAnimation(VisualElement self)
        {
            self.AbortAnimation("ColorTo");
        }
        #endregion
    }
}