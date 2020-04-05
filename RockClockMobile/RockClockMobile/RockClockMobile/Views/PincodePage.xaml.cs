using RockClockMobile.Models;
using RockClockMobile.Services;
using RockClockMobile.ViewModels;
using RockClockMobile.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PincodePage : ContentPage
    {
        //Employee employeeSignedIn = GlobalServices.employee;
        NamesListViewModel userViewModel = new NamesListViewModel();
        PincodeViewModel pincodeViewModel = new PincodeViewModel();
        User userSign = new User();
        public PincodePage(string userPassword)
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });

            NavigationPage.SetHasNavigationBar(this,false);
            userPassword = null;
            if (userPassword == null)
            {
                BtnSignIn.Text = "Create PIN";
            }
        }

        private async void BtnSignInEvent(object sender, EventArgs e)
        {
            int pin = EntryPin.Text == ""? 0 : Convert.ToInt32(EntryPin.Text);

            if (BtnSignIn.Text == "Create PIN")
            {
               // await Task.Run(async () => {
                    await DisplayAlert("Confirmation", "PIN created successfully.", "Ok");
                    EntryPin.Text = string.Empty;
                    BtnSignIn.Text = "Sign In";
               // });
                
                
                //userSign = await userViewModel.GetUser();
            }
            else {
                if (pin == 0)
                {
                    await DisplayAlert("Error", "Pincode is required.", "OK");
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