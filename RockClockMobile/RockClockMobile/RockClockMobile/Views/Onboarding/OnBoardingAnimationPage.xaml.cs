using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System;

namespace RockClockMobile.Views.Onboarding
{
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OnBoardingAnimationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnBoardingAnimationPage" /> class.
        /// </summary>
        public OnBoardingAnimationPage()
        {
            InitializeComponent();
        }

        private void BreakButtonText_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Test","Testing only","OK");
        }
    }
}