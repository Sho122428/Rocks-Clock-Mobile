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
        bool isOnBreak = false;
        
        public TimeClockPage()
        {
            InitializeComponent();

            btnTimeClock.Text = "Clock In";
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(!isTimedIn)
            { 
                var cur_time = DateTime.Now.ToString("h:mm tt");
                lblClockedIn.Text = cur_time;
                DisplayAlert("Alert", "You have clocked in " +cur_time, "OK");
                btnTimeClockBreak.IsVisible = true;
                btnTimeClock.Text = "Clock Out";
                isTimedIn = true;
                lblclockin.IsVisible = true;
                lblClockedIn.IsVisible = true;
                lblbreak.IsVisible = true;
            }
            else
            {
                var cur_time = DateTime.Now.ToString("h:mm tt");
                DisplayAlert("Alert", "You have clocked out " + cur_time, "OK");
                lblClockedOut.Text = cur_time;
                lblclockout.IsVisible = true;
                lblClockedOut.IsVisible = true;
            }
        }

        private void timer()
        {
            int secs = 0;
            // fire an event every 1000 ms
            Timer timer = new Timer(1000);
            // when event fires, update Label
            timer.Elapsed += (sender, e) => { secs++; lblClockedIn.Text = $"{secs} seconds"; };
            // start the timer
            timer.Start();
        }

        private void ButtonBreak_Clicked(object sender, EventArgs e)
        {
            var cur_time = DateTime.Now.ToString("h:mm tt");

           // grdBreak.IsVisible = true;
            
            if(!isOnBreak)
            {
                isOnBreak = true;
                DisplayAlert("Alert", "You have started your break " + cur_time, "OK");
                
                lblBreakTimeStart.Text = cur_time;
                
                btnTimeClockBreak.Text = "End Break";
            }
            else
            {
                isOnBreak = false;
                DisplayAlert("Alert", "You have ended your break " + cur_time, "OK");
                
                lblBreakTimeEnd.Text = cur_time;
                lblTotal.IsVisible = true;
                lblTotalBrkHrs.IsVisible = true;
                btnTimeClockBreak.Text = "Start Break";

            }
        }

        
    }
}