using RockClockMobile.Custom;
using RockClockMobile.Models;
using RockClockMobile.Services;
using RockClockMobile.ViewModels;
using RockClockMobile.ViewModels.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PincodePage : ContentPage
    {
        //Employee employeeSignedIn = GlobalServices.employee;
        NamesListViewModel namesListViewModel = new NamesListViewModel();
        //PincodeViewModel pincodeViewModel = new PincodeViewModel();
        User userSign = new User();
        Employee employee = GlobalServices.employee;
        int lastUserId = 0;
        public PincodePage(string userPassword,int userId)
        {
            InitializeComponent();

            BindingContext = new PincodeViewModel();

            lastUserId = userId;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });

            NavigationPage.SetHasNavigationBar(this,false);

            if (userPassword == "0")
            {
                BtnSignIn.Text = "Create PIN";
            }
            EntryPin.Keyboard = Keyboard.Numeric;
        }

        private async void BtnSignInEvent(object sender, EventArgs e)
        {    
            if (string.IsNullOrWhiteSpace(EntryPin.Text) || EntryPin.Text.Length > 4)
            {
                ToastPopup.ToastMessage("Pin contains whitespaces or incorrect.", false);
                await Task.Delay(2000);
            }
            else {
                int pin = EntryPin.Text == "" ? 0 : Convert.ToInt32(EntryPin.Text);

                if (BtnSignIn.Text == "Create PIN")
                {
                    await namesListViewModel.AddUser(pin.ToString(), lastUserId);

                    // await Task.Run(async () => {
                    ToastPopup.ToastMessage("Pin created successfully.", false);
                    await Task.Delay(2000);
                    EntryPin.Text = string.Empty;
                    BtnSignIn.Text = "Sign In";


                    // });


                    //userSign = await userViewModel.GetUser();
                }
                else
                {
                    if (pin == 0)
                    {
                        ToastPopup.ToastMessage("Pin is required.", false);
                        await Task.Delay(2000);
                    }
                    else
                    {
                        //if (userSign.password != pin)
                        //{
                        //    await DisplayAlert("Error", "Pincode is not registered.", "OK");
                        //}
                        //else
                        //{
                        await Navigation.PushModalAsync(new NavigationPage(new Onboarding.OnBoardingAnimationPage()));
                        //}
                    }
                }
            }
        }
    }
}