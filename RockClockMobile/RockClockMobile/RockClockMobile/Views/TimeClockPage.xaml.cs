using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeClockPage : ContentPage
    {
        bool isTimedIn = false;
        
        public TimeClockPage()
        {
            InitializeComponent();

            btnTimeClock.Text = "Clock In";
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                myLabel.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(!isTimedIn)
            { 
                var cur_time = DateTime.Now.ToString("h:mm tt");
                DisplayAlert("Alert", "You have clocked in. " +cur_time, "OK");
                btnTimeClockBreak.IsVisible = true;
            }
            else
            {
                var cur_time = DateTime.Now.ToString("h:mm tt");
                DisplayAlert("Alert", "You have clocked out. " + cur_time, "OK");
                btnTimeClock.Text = "Clock Out";
            }
        }

        private void timer()
        {
            int secs = 0;
            // fire an event every 1000 ms
            Timer timer = new Timer(1000);
            // when event fires, update Label
            timer.Elapsed += (sender, e) => { secs++; myLabel.Text = $"{secs} seconds"; };
            // start the timer
            timer.Start();
        }

        private void ButtonBreak_Clicked(object sender, EventArgs e)
        {

        }
    }
}