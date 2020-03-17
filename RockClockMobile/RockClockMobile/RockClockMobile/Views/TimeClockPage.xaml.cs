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
        private Timer timer1;
        private int counter = 20;
        private static Timer _delayTimer;

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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(!isTimedIn)
            { 
                var cur_time = DateTime.Now.ToString("h:mm tt");
                lblClockedIn.Text = cur_time;
                await DisplayAlert("Alert", "You have clocked in " +cur_time, "OK");
                btnTimeClockBreak.IsVisible = true;
                btnTimeClock.Text = "Clock Out";
                isTimedIn = true;
                lblclockin.IsVisible = true;
                lblClockedIn.IsVisible = true;
                lblbreakSt.IsVisible = true;
                lblbreakEnd.IsVisible = true;
            }
            else
            {
                var cur_time = DateTime.Now.ToString("h:mm tt");
                await DisplayAlert("Alert", "You have clocked out " + cur_time, "OK");
                lblClockedOut.Text = cur_time;
                lblclockout.IsVisible = true;
                lblClockedOut.IsVisible = true;
            }

            //delay(3000);
            logOut();
            
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
                //lblTotal.IsVisible = true;
                //lblTotalBrkHrs.IsVisible = true;
                btnTimeClockBreak.Text = "Start Break";

            }
        }

        async void logOut()
        {
            await Navigation.PushAsync(new ItemsPage());
        }
        //private void timeToLogout()
        //{
        //    int counter = 60;
        //    timer1 = new Timer();
        //    timer1.Tick += new EventHandler(timer1_Tick);
        //    timer1.Interval = 1000; // 1 second
        //    timer1.Start();
        //    label1.Text = counter.ToString();
        //}

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    counter--;
        //    if (counter == 0)
        //        timer1.Stop();
        //    lblCountDown.Text = counter.ToString();
        //}

        async static void delay(int Time_delay)
        {
            int i = 0;
            //  ameTir = new System.Timers.Timer();
            _delayTimer = new System.Timers.Timer();
            _delayTimer.Interval = Time_delay;
            _delayTimer.AutoReset = false; //so that it only calls the method once
            _delayTimer.Elapsed += (s, args) => i = 1;
            _delayTimer.Start();
            while (i == 0) { };
        }
    }
}