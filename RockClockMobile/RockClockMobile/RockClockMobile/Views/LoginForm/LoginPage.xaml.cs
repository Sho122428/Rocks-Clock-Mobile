using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

using Xamarin.Forms;
using RockClockMobile.ViewModels.LoginForm;
using RockClockMobile.Custom;
using System.Threading.Tasks;

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
            ImageLogo.Source = ImageSource.FromFile("fslogo.png");
            ImagePassword.Source = ImageSource.FromFile("passwordicon.png");
        }

        private async void BtnLoginEvent(object sender, System.EventArgs e)
        {          
            var details = (LoginPageViewModel)this.BindingContext;

            //if(EmlEntry.GetValue().ToString() == "")

            if (details.CanLogin)
            {
                //if (details.UserEmail.ToLower() == "denolantest@email.com")
                //{
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        LoginPageViewModel loginPageVM = (LoginPageViewModel)this.BindingContext;
                        await loginPageVM.OnLoadPage();                       

                        App.Current.MainPage = new Views.Navigation.NamesListPage();

                        ToastPopup.ToastMessage("Successfully logged in.", false);
                        await Task.Delay(2000);
                    });
                //}
                //else
                //{

                //    await DisplayAlert("Info", "use Denolantest@email.com", "OK");
                //    //Device.BeginInvokeOnMainThread(async () =>
                //    //{
                //    //    LoginPageViewModel loginPageVM = (LoginPageViewModel)this.BindingContext;
                //    //    await loginPageVM.OnLoadPage();

                //    //    await Navigation.PushModalAsync(new NavigationPage(new Onboarding.OnBoardingAnimationPage()));
                //    //});                
                //}
            }
            else {
                ToastPopup.ToastMessage("Login error, please check the credentials.", false);
                await Task.Delay(2000);
            }
        }
    }
}