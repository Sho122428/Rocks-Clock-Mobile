using RockClockMobile.Models;
using RockClockMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views.Onboarding
{
    /// <summary>
    /// Page to display on-boarding gradient with animation
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalkthroughItemPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WalkthroughItemPage" /> class.
        /// </summary>
        Employee empDtl = GlobalServices.employee;
        List<TimeLog> empUserLog = GlobalServices.EmployeeTime;
        List<BreakLog> empUserBreakLog = GlobalServices.EmployeeBreak;

        public WalkthroughItemPage()
        {
            InitializeComponent();

            LoadClock();
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
                TimeLog LoggedInUser = empUserLog.Where(a => a.rocksUserID == empDtl.EmpID).FirstOrDefault();

                if (LoggedInUser != null && LoggedInUser.IsClockedOut != true)
                {
                    //isTimedIn = true;
                    //lblClockedIn.Text = LoggedInUser.TimeIn.ToString("h:mm tt");
                    //btnTimeClock.Text = "Clock Out";
                    //btnTimeClock.BackgroundColor = Color.Red;
                    //btnTimeClockBreak.IsEnabled = true;
                    //btnTimeClockBreak.Opacity = 1;
                    lblClockedIn.Text = LoggedInUser.TimeIn.ToString("h:mm tt");
                }
                //else if (LoggedInUser != null && LoggedInUser.IsClockedOut == true) //Display data only
                //{
                //    lblClockedIn.Text = LoggedInUser.TimeIn.ToString("h:mm tt");
                //    lblClockedOut.Text = LoggedInUser.TimeOut.ToString("h:mm tt");
                //    btnTimeClock.IsEnabled = false;
                //    btnTimeClock.Opacity = .5;
                //    btnTimeClockBreak.IsEnabled = false;
                //    btnTimeClockBreak.Opacity = .5;
                //}

                //if (empUserBreakLog != null)
                //{
                //    BreakLog takeBreak = empUserBreakLog.Where(a => a.TimeId == LoggedInUser.TimeId).FirstOrDefault();

                //    if (takeBreak != null && takeBreak.IsTakingABreak != false)
                //    {
                //        isOnBreak = true;
                //        lblBreakTimeStart.Text = takeBreak.BreakIn.ToString("h:mm tt");

                //        lblBreakTimeStart.IsVisible = true;
                //        btnTimeClockBreak.Text = "End Break";
                //    }
                //    else if (takeBreak != null && takeBreak.IsTakingABreak == false)
                //    {
                //        lblBreakTimeStart.Text = takeBreak.BreakIn.ToString("h:mm tt");
                //        lblBreakTimeEnd.Text = takeBreak.BreakOut.ToString("h:mm tt");
                //    }
                //}
            }
        }
    }
}