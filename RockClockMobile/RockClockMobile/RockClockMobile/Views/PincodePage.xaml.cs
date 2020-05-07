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
        NamesListViewModel namesListViewModel = new NamesListViewModel();

        RocksUser employee = GlobalServices.employee;
        int userId = 0;
        public PincodePage(int signedInUserId)
        {
            InitializeComponent();

            BindingContext = new PincodeViewModel();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });

            NavigationPage.SetHasNavigationBar(this,false);

            EntryPin.Keyboard = Keyboard.Numeric;

            EntryPin.Text = "1234";
            ImagePin.Source = ImageSource.FromFile("passwordicon.png");
            userId = signedInUserId;
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
               
                if (pin == 0)
                {
                    ToastPopup.ToastMessage("Pin is required.", false);
                    await Task.Delay(2000);
                }
                else
                {
                    var userLoginParam = new UserLoginParam
                    {
                        RocksUserId = userId,
                        Password = pin.ToString(),
                        Remember = true
                    };
                    var userLoggedIn = await namesListViewModel.UserLogin(userLoginParam);

                    if (userLoggedIn != null)
                    {
                        GlobalServices.employee = userLoggedIn.rocksUser;
                        var userSignedDetails = await namesListViewModel.GetUserList(userId);

                        if (userSignedDetails.isTempPassword && userSignedDetails != null)
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                await namesListViewModel.OnLoadPage();
                                App.Current.MainPage = new Views.ResetPassword.ResetPasswordPage(userId);
                            });
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                PincodeViewModel pincodeVM = (PincodeViewModel)this.BindingContext;

                                await pincodeVM.OnLoadPage();
                                pincodeVM.IsLoggedIn = false;
                                await Navigation.PushModalAsync(new NavigationPage(new Onboarding.OnBoardingAnimationPage()));
                            });
                        }
                    }
                    else {
                        ToastPopup.ToastMessage("Pin is incorrect.", false);
                        await Task.Delay(2000);
                    }
                }                
            }
        }
    }
}