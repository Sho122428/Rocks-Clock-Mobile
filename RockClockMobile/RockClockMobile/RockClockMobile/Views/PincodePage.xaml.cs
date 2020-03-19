using RockClockMobile.Models;
using RockClockMobile.Services;
using RockClockMobile.ViewModels;
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
        PincodeViewModel pincodeViewModel = new PincodeViewModel();
        //public Employee EmpDetail;
        public PincodePage()
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
        }

        private async void BtnSignInEvent(object sender, EventArgs e)
        {
            int pin = EntryPin.Text == ""? 0 : Convert.ToInt32(EntryPin.Text);
            var empDtl = GlobalServices.employee;

            if (pin == 0)
            {
                await DisplayAlert("Error", "Pincode is required.", "OK");
            }
            else {
                if (!pincodeViewModel.ValidatePin(pin))
                {
                    await DisplayAlert("Error", "Pincode is not registered.", "OK");
                }
                else
                {
                    await Navigation.PushModalAsync(new NavigationPage(new TimeClockPage()));
                }
            }                      
        }
    }
}