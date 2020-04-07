using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

using Xamarin.Forms;
using RockClockMobile.ViewModels.LoginForm;

namespace RockClockMobile.Views.LoginForm
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        /// 
       
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnLoginEvent(object sender, System.EventArgs e)
        {          
            var details = (LoginPageViewModel)this.BindingContext;

            if (details.Username.ToLower() == "sho@gmail.com")
            {
                App.Current.MainPage = new Views.Navigation.NamesListPage();
            }
            else {
                await Navigation.PushModalAsync(new NavigationPage(new Onboarding.OnBoardingAnimationPage()));
            }
        }
    }
}