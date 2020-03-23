using RockClockMobile.Models;
using RockClockMobile.Services;
using RockClockMobile.ViewModels;
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
        private static Timer _delayTimer;
        private int empID;
        
        bool isTimedIn = false;
        bool isOnBreak = false;

        TimeLogViewModel viewModel;
        public TimeLog TimeLog { get; set; }
        

        public TimeClockPage()
        {
            InitializeComponent();

            //isClocked_In = GlobalServices.IsClockedIn;
            //if(isClocked_In)
            BindingContext = viewModel = new TimeLogViewModel();

            //btnTimeClock.Text = "Clock In";
            
            
            LoadClock();
            TimeLog timeLog = new TimeLog();
            timeLog.rocksUserID = GlobalServices.employee.EmpID;
            viewModel.LoadEmployeeTimeLog.Execute(timeLog);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(!isTimedIn)
            { 
                var cur_time = DateTime.Now.ToString("h:mm tt");
                lblClockedIn.Text = cur_time;
                await DisplayAlert("Rocks Clock", "You have clocked in " +cur_time, "OK");
                
                btnTimeClock.Text = "Clock Out";
                isTimedIn = true;
                btnTimeClockBreak.IsEnabled = true;
                btnTimeClockBreak.Opacity = 1;
               
            }
            else
            {
                var cur_time = DateTime.Now.ToString("h:mm tt");
                await DisplayAlert("Alert", "You have clocked out " + cur_time, "OK");
                lblClockedOut.Text = cur_time;
                lblclockout.IsVisible = true;
                lblClockedOut.IsVisible = true;
            }
            int cntId = viewModel.TimeLogs.Count;
            TimeLog = new TimeLog
            {
                TimeId = cntId + 1,
                TimeIn = DateTime.Now,
                rocksUserID = int.Parse(Application.Current.Properties["user_id "].ToString())

            };
            
            MessagingCenter.Send(this, "AddTimeLog", TimeLog);
            GlobalServices.IsClockedIn = true;
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

        private async void ButtonBreak_Clicked(object sender, EventArgs e)
        {
            var cur_time = DateTime.Now.ToString("h:mm tt");

           // grdBreak.IsVisible = true;
            
            if(!isOnBreak)
            {
                isOnBreak = true;
                await DisplayAlert("Alert", "You have started your break " + cur_time, "OK");
                
                lblBreakTimeStart.Text = cur_time;
                lblBreakTimeStart.IsVisible = true;
                btnTimeClockBreak.Text = "End Break";
            }
            else
            {
                isOnBreak = false;
                await DisplayAlert("Alert", "You have ended your break " + cur_time, "OK");
                
                lblBreakTimeEnd.Text = cur_time;
                //lblTotal.IsVisible = true;
                //lblTotalBrkHrs.IsVisible = true;
                lblBreakTimeEnd.IsVisible = true;
                btnTimeClockBreak.Text = "Start Break";

            }
            logOut();
        }

        async void logOut()
        {
            //await Navigation.PushAsync(new LoginPage());
            await Task.Delay(1000);
            Application.Current.MainPage = new LoginPage();
        }
        

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

        async void LoadClock()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });
        }

        private async void btnSignOut_Clicked(object sender, EventArgs e)
        {
             logOut();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.TimeLogs.Count == 0)
                viewModel.LoadTimeLogsCommand.Execute(null);

        }
    }
}