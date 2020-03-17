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
        }

        private async void BtnSignInEvent(object sender, EventArgs e)
        {
            int pin = Convert.ToInt32(EntryPin.Text);

            if (pin != 123)
            {
                await DisplayAlert("Error","Pincode is not registered.","OK");
            }
            else {
                await Navigation.PushModalAsync(new NavigationPage(new TimeClockPage()));
            }            
        }
    }
}