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
        RocksUser empDtl = GlobalServices.employee;
        List<TimeLog> empUserLog = GlobalServices.EmployeeTime;
        List<BreakLog> empUserBreakLog = GlobalServices.EmployeeBreak;
        //TimeLog LogedInUser = empUserLog.Where(a => a.rocksUserID == empDtl.EmpID).FirstOrDefault();

        

        bool isTimedIn = false;
        bool isOnBreak = false;

        TimeLogViewModel viewModel;
        public TimeLog TimeLog { get; set; }
        

        public TimeClockPage()
        {
            InitializeComponent();

            //BindingContext = viewModel = new TimeLogViewModel();

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
                var countTimeID = 0;

                if (empUserLog != null)
                    countTimeID = empUserLog.Count;
                
                var userTimeLog = new TimeLog{
                    id = countTimeID + 1,
                    RocksUserId = empDtl.id,
                    Start = Convert.ToDateTime(cur_time),
                    //IsClockedOut = false
                    
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
                TimeLog LoggedInUser = empUserLog.Where(a => a.RocksUserId == empDtl.id).FirstOrDefault();

                if (LoggedInUser != null)
                {
                    LoggedInUser.End = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt").ToString());
                    //LoggedInUser.IsClockedOut = true;
                }

                var cur_time = DateTime.Now.ToString("h:mm tt");
                await DisplayAlert("Rocks Clock", "You have clocked out " + cur_time, "OK");
                lblClockedOut.Text = cur_time;
                lblclockout.IsVisible = true;
                lblClockedOut.IsVisible = true;
            }

            logOut();
            
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

                TimeLog LoggedInUser = empUserLog.Where(a => a.RocksUserId == empDtl.id).FirstOrDefault();
                var ndx = 0; 
                    
                if(empUserBreakLog != null)
                {
                    ndx = empUserBreakLog.Count;
                }

                var userBreakLog = new BreakLog
                {
                    timeLogId = LoggedInUser.id,
                    id = ndx + 1,
                    breakIn = Convert.ToDateTime(cur_time),
                    IsTakingABreak = true

                };

                List<BreakLog> EmployeeBreakLog = new List<BreakLog>();

                if (empUserBreakLog != null)
                {
                    EmployeeBreakLog = empUserBreakLog;
                }

                EmployeeBreakLog.Add(userBreakLog);

                GlobalServices.EmployeeBreak = EmployeeBreakLog;
            }
            else
            {
                TimeLog LoggedInUser = empUserLog.Where(a => a.RocksUserId == empDtl.id).FirstOrDefault();
                BreakLog takeBreak = empUserBreakLog.Where(a => a.timeLogId == LoggedInUser.id).FirstOrDefault();

                if (takeBreak != null)
                {
                    takeBreak.breakOut = Convert.ToDateTime(DateTime.Now.ToString("h:mm tt").ToString());
                    takeBreak.IsTakingABreak = false;
                }
                isOnBreak = false;
                await DisplayAlert("Alert", "You have ended your break " + cur_time, "OK");
                
                lblBreakTimeEnd.Text = cur_time;
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
                TimeLog LoggedInUser = empUserLog.Where(a => a.RocksUserId == empDtl.id).FirstOrDefault();

                if (LoggedInUser != null)
                {
                    isTimedIn = true;
                    lblClockedIn.Text = LoggedInUser.Start.Value.ToString("h:mm tt");
                    btnTimeClock.Text = "Clock Out";
                    btnTimeClock.BackgroundColor = Color.Red;
                    btnTimeClockBreak.IsEnabled = true;
                    btnTimeClockBreak.Opacity = 1;
                }
                else if(LoggedInUser != null) //Display data only
                {
                    lblClockedIn.Text = LoggedInUser.Start.Value.ToString("h:mm tt");
                    lblClockedOut.Text = LoggedInUser.End.Value.ToString("h:mm tt");
                    btnTimeClock.IsEnabled = false;
                    btnTimeClock.Opacity = .5;
                    btnTimeClockBreak.IsEnabled = false;
                    btnTimeClockBreak.Opacity = .5;
                }

                if (empUserBreakLog != null)
                {
                    BreakLog takeBreak = empUserBreakLog.Where(a => a.timeLogId == LoggedInUser.id).FirstOrDefault();

                    if (takeBreak != null && takeBreak.IsTakingABreak != false)
                    {
                        isOnBreak = true;
                        lblBreakTimeStart.Text = takeBreak.breakIn.ToString("h:mm tt");
                        
                        lblBreakTimeStart.IsVisible = true;
                        btnTimeClockBreak.Text = "End Break";
                    }
                    else if(takeBreak != null && takeBreak.IsTakingABreak == false)
                    {
                        lblBreakTimeStart.Text = takeBreak.breakIn.ToString("h:mm tt");
                        lblBreakTimeEnd.Text = takeBreak.breakOut.ToString("h:mm tt");
                    }
                }
            }

            
        }

        private async void btnSignOut_Clicked(object sender, EventArgs e)
        {
             logOut();
        }

        

        


    }
}