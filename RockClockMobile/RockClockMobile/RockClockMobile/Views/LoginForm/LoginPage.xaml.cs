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
            BindingContext = new LoginPageViewModel();

            PasswordEntry.Text = "Fullsc@l3";
        }

        private async void BtnLoginEvent(object sender, System.EventArgs e)
        {          
            var details = (LoginPageViewModel)this.BindingContext;
            //var email = details.

            if (details.UserEmail.ToLower() == "denolantest@email.com")
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    LoginPageViewModel loginPageVM = (LoginPageViewModel)this.BindingContext;
                    await loginPageVM.OnLoadPage();

                    App.Current.MainPage = new Views.Navigation.NamesListPage();
                });                
            }
            else {

                await DisplayAlert("Info","use Denolantest@email.com","OK");
                //Device.BeginInvokeOnMainThread(async () =>
                //{
                //    LoginPageViewModel loginPageVM = (LoginPageViewModel)this.BindingContext;
                //    await loginPageVM.OnLoadPage();

                //    await Navigation.PushModalAsync(new NavigationPage(new Onboarding.OnBoardingAnimationPage()));
                //});                
            }
        }
    }
}