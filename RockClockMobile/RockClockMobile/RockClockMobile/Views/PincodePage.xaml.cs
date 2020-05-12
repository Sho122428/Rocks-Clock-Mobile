using RockClockMobile.Custom;
using RockClockMobile.Models;
using RockClockMobile.Services;
using RockClockMobile.ViewModels;
using RockClockMobile.ViewModels.Navigation;
using RockClockMobile.ViewModels.ResetPassword;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PincodePage : ContentPage
    {
        
        public PincodePage()
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

            
        }

        private async void BtnSignInEvent(object sender, EventArgs e)
        {
            //PincodeViewModel pincodeVM = (PincodeViewModel)this.BindingContext;
            //pincodeVM.IsLoggedIn = false;

            //if (string.IsNullOrWhiteSpace(EntryPin.Text) || EntryPin.Text.Length > 4)
            //{
            //    ToastPopup.ToastMessage("Pin contains whitespaces or incorrect.", false);
            //    await Task.Delay(2000);
            //}
            //else {
            //    int pin = EntryPin.Text == "" ? 0 : Convert.ToInt32(EntryPin.Text);
               
            //    if (pin == 0)
            //    {
            //        ToastPopup.ToastMessage("Pin is required.", false);
            //        await Task.Delay(2000);
            //    }
            //    else
            //    {
            //        var userLoginParam = new UserLoginParam
            //        {
            //            RocksUserId = userId,
            //            Password = pin.ToString(),
            //            Remember = true
            //        };
            //        var userLoggedIn = await pinVM.UserLogin(userLoginParam);

            //        if (userLoggedIn != null)
            //        {
            //            GlobalServices.employee = userLoggedIn.rocksUser;
            //            var userSignedDetails = await pinVM.GetUserList(userId);

            //            if (userSignedDetails.isTempPassword && userSignedDetails != null)
            //            {
            //                Device.BeginInvokeOnMainThread(async () =>
            //                {
            //                    await pincodeVM.OnLoadPage();
            //                    App.Current.MainPage = new Views.ResetPassword.ResetPasswordPage(userId);
            //                });
            //            }
            //            else
            //            {
            //                Device.BeginInvokeOnMainThread(async () =>
            //                {     
            //                    await pincodeVM.OnLoadPage();
            //                    pincodeVM.IsLoggedIn = false;
            //                    await Navigation.PushModalAsync(new NavigationPage(new Onboarding.OnBoardingAnimationPage()));
            //                });
            //            }
            //        }
            //        else {
            //            ToastPopup.ToastMessage("Pin is incorrect.", false);
            //            await Task.Delay(2000);
            //        }
            //    }
            //    //Sets the flag of automating to redirect to home to false;
            //    pinVM.IsLoggedIn = false;
            //}
        }
    }
}