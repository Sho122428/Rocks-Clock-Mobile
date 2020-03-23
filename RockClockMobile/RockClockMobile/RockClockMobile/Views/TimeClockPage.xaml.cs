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
        Employee empDtl = GlobalServices.employee;
        List<TimeLog> empUserLog = GlobalServices.EmployeeTime;
        //TimeLog LogedInUser = empUserLog.Where(a => a.rocksUserID == empDtl.EmpID).FirstOrDefault();

        private Timer timer1;
        private int counter = 20;
        private static Timer _delayTimer;
        private int empID;

        bool isTimedIn = false;
        bool isOnBreak = false;

        TimeLogViewModel viewModel;
        public TimeLog TimeLog { get; set; }
        

        public TimeClockPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new TimeLogViewModel();

            //btnTimeClock.Text = "Clock In";
            //empID = employee.EmpID;
            
            LoadClock();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {        
            if (!isTimedIn)
            { 
                var cur_time = DateTime.Now.ToString("h:mm tt");
                lblClockedIn.Text = cur_time;
                await DisplayAlert("Rocks Clock", "You have clocked in " +cur_time, "OK");
                //btnTimeClockBreak.IsVisible = true;
                btnTimeClock.Text = "Clock Out";
                isTimedIn = true;
                btnTimeClockBreak.IsEnabled = true;
                btnTimeClockBreak.Opacity = 1;
                //lblclockin.IsVisible = true;
                //lblClockedIn.IsVisible = true;
                //lblbreakSt.IsVisible = true;
                //lblBreakTimeStart.IsVisible = true;
                //lblbreakEnd.IsVisible = true;
                //lblBreakTimeEnd.IsVisible = true;I
                //lblclockout.IsVisible = true;
                //lblClockedOut.IsVisible = true;

                var userTimeLog = new TimeLog{
                    rocksUserID = empDtl.EmpID,
                    TimeIn = Convert.ToDateTime(cur_time),
                    TimeOut = Convert.ToDateTime(cur_time)
                };

                List<TimeLog> EmployeeTimeLog = new List<TimeLog>();

                if (empUserLog != null)
                {
                    EmployeeTimeLog = empUserLog;
                }

                EmployeeTimeLog.Add(userTimeLog);

                GlobalServices.EmployeeTime = EmployeeTimeLog;
            }
            else
            {
                TimeLog LogedInUser = empUserLog.Where(a => a.rocksUserID == empDtl.EmpID).FirstOrDefault();

                if (LogedInUser != null)
                {
                    LogedInUser.TimeOut = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt").ToString());
                    LogedInUser.IsClokedOut = true;
                }

                var cur_time = DateTime.Now.ToString("h:mm tt");
                await DisplayAlert("Alert", "You have clocked out " + cur_time, "OK");
                lblClockedOut.Text = cur_time;
                lblclockout.IsVisible = true;
                lblClockedOut.IsVisible = true;
            }

            TimeLog = new TimeLog
            {
                TimeId = 2,
                TimeIn = DateTime.Now,
                rocksUserID = empID

            };
            MessagingCenter.Send(this, "AddTimeLog", TimeLog);
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
            Application.Current.MainPage = new LoginPage();
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

        async void LoadClock()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });            

            if (empUserLog != null)
            {
                TimeLog LogedInUser = empUserLog.Where(a => a.rocksUserID == empDtl.EmpID).FirstOrDefault();

                if (LogedInUser != null && LogedInUser.IsClokedOut != true)
                {
                    isTimedIn = true;
                    lblClockedIn.Text = LogedInUser.TimeIn.ToString();
                    btnTimeClock.Text = "Clock Out";
                    btnTimeClock.BackgroundColor = Color.Red;
                }
            }            
        }

        private async void btnSignOut_Clicked(object sender, EventArgs e)
        {
             logOut();
        }
    }
}