using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RockClockMobile.Models;
using RockClockMobile.Models.Onboarding;
using RockClockMobile.Services;
using RockClockMobile.Views.Onboarding;
using Syncfusion.SfRotator.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RockClockMobile.ViewModels.Onboarding
{
    /// <summary>
    /// ViewModel for on-boarding gradient page with animation.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class OnBoardingAnimationViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<Boarding> boardings;

        private string nextButtonText = "NEXT";

        private bool isSkipButtonVisible = true;

        private int selectedIndex;

        private string clockinButtonText = "CLOCK IN";

        private string breakButtonText = "START BREAK";

        private bool isBreakButtonVisible = false;

        private bool isSignOutButtonVisible = true;

        private string fNameUser = "";

        private bool isClockedIn = false;

        private bool isOnBreak = false;



        #endregion

        Employee empDtl = GlobalServices.employee;
        List<TimeLog> empUserLog = GlobalServices.EmployeeTime;
        List<BreakLog> empUserBreakLog = GlobalServices.EmployeeBreak;

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OnBoardingAnimationViewModel" /> class.
        /// </summary>
        public OnBoardingAnimationViewModel()
        {
            this.SkipCommand = new Command(this.Skip);
            this.NextCommand = new Command(this.Next);
            this.ClockInCommand = new Command(this.ClockIn);
            this.BreakCommand = new Command(this.Break);
            this.SignOutCommand = new Command(this.SignOut);

            

            fNameUser = "Hello, " + empDtl.FirstName;
            

            this.Boardings = new ObservableCollection<Boarding>
            {
                new Boarding()
                {
                    //ImagePath = "ReSchedule.png",
                    Header = fNameUser,
                    //Content = "Drag and drop meetings in order to reschedule them easily.",
                    RotatorItem = new WalkthroughItemPage()
                }
                
            };

            LoadDataClock();
            
            // Set bindingcontext to content view.
            foreach (var boarding in this.Boardings)
            {
                boarding.RotatorItem.BindingContext = boarding;
            }
        }

        async void LoadDataClock()
        {
            if (empUserLog != null)
            {
                TimeLog LoggedInUser = empUserLog.Where(a => a.rocksUserID == empDtl.EmpID).FirstOrDefault();

                if (LoggedInUser != null && LoggedInUser.IsClockedOut != true)
                {
                    this.IsBreakButtonVisible = true;
                    this.ClockInButtonText = "CLOCK OUT";
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

                if (empUserBreakLog != null)
                {
                    BreakLog takeBreak = empUserBreakLog.Where(a => a.TimeId == LoggedInUser.TimeId).FirstOrDefault();

                    if (takeBreak != null && takeBreak.IsTakingABreak != false)
                    {
                        this.BreakButtonText = "END BREAK";
                        this.IsOnBreak = true;
                    }
                    else if (takeBreak != null && takeBreak.IsTakingABreak == false)
                    {
                        this.BreakButtonText = "START BREAK";
                        
                    }
                }
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<Boarding> Boardings
        {
            get
            {
                return this.boardings;
            }

            set
            {
                if (this.boardings == value)
                {
                    return;
                }

                this.boardings = value;
                this.OnPropertyChanged();
            }
        }

        public string NextButtonText
        {
            get
            {
                return this.nextButtonText;
            }

            set
            {
                if (this.nextButtonText == value)
                {
                    return;
                }

                this.nextButtonText = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsSkipButtonVisible
        {
            get
            {
                return this.isSkipButtonVisible;
            }

            set
            {
                if (this.isSkipButtonVisible == value)
                {
                    return;
                }

                this.isSkipButtonVisible = value;
                this.OnPropertyChanged();
            }
        }

        public string ClockInButtonText
        {
            get
            {
                return this.clockinButtonText;
            }

            set
            {
                if (this.clockinButtonText == value)
                {
                    return;
                }

                this.clockinButtonText = value;
                this.OnPropertyChanged();
            }
        }

        public string BreakButtonText
        {
            get
            {
                return this.breakButtonText;
            }

            set
            {
                if (this.breakButtonText == value)
                {
                    return;
                }

                this.breakButtonText = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsSignOutButtonVisible
        {
            get
            {
                return this.isSignOutButtonVisible;
            }

            set
            {
                if (this.isSignOutButtonVisible == value)
                {
                    return;
                }

                this.isSignOutButtonVisible = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsBreakButtonVisible
        {
            get
            {
                return this.isBreakButtonVisible;
            }

            set
            {
                if (this.isBreakButtonVisible == value)
                {
                    return;
                }

                this.isBreakButtonVisible = value;
                this.OnPropertyChanged();
            }
        }

        public string FNameUser
        {
            get
            {
                return this.fNameUser;
            }

            set
            {
                if (this.fNameUser == value)
                {
                    return;
                }

                this.fNameUser = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsClockedIn
        {
            get
            {
                return this.isClockedIn;
            }

            set
            {
                if (this.isClockedIn == value)
                {
                    return;
                }

                this.isClockedIn = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsOnBreak
        {
            get
            {
                return this.isOnBreak;
            }

            set
            {
                if (this.isOnBreak == value)
                {
                    return;
                }

                this.isOnBreak = value;
                this.OnPropertyChanged();
            }
        }


        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                if (this.selectedIndex == value)
                {
                    return;
                }

                this.selectedIndex = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Skip button is clicked.
        /// </summary>
        public ICommand SkipCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Done button is clicked.
        /// </summary>
        public ICommand NextCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Clock In button is clicked.
        /// </summary>
        public ICommand ClockInCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Break button is clicked.
        /// </summary>
        public ICommand BreakCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign out button is clicked.
        /// </summary>
        public ICommand SignOutCommand { get; set; }

        

        #endregion

        #region Methods

        private bool ValidateAndUpdateSelectedIndex(int itemCount)
        {
            if (this.SelectedIndex >= itemCount - 1)
            {
                return true;
            }

            this.SelectedIndex++;
            return false;
        }

        /// <summary>
        /// Invoked when the Skip button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Skip(object obj)
        {
            this.MoveToNextPage();
        }

        /// <summary>
        /// Invoked when the Done button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Next(object obj)
        {
            var itemCount = (obj as SfRotator).ItemsSource.Count();
            if (this.ValidateAndUpdateSelectedIndex(itemCount))
            {
                this.MoveToNextPage();
            }
        }
        private void ClockIn(object obj)
        {
            if(!IsClockedIn)
            {
                var countTimeID = 0;

                if (empUserLog != null)
                    countTimeID = empUserLog.Count;

                var userTimeLog = new TimeLog
                {
                    TimeId = countTimeID + 1,
                    rocksUserID = empDtl.EmpID,
                    TimeIn = DateTime.Now,
                    IsClockedOut = false

                };
                
                List<TimeLog> EmployeeTimeLog = new List<TimeLog>();

                if (empUserLog != null)
                {
                    EmployeeTimeLog = empUserLog;
                }

                EmployeeTimeLog.Add(userTimeLog);

                GlobalServices.EmployeeTime = EmployeeTimeLog;
            }
            this.SignOut();
        }
        private void Break(object obj)
        {

            if (!IsOnBreak)
            {
                TimeLog LoggedInUser = empUserLog.Where(a => a.rocksUserID == empDtl.EmpID).FirstOrDefault();
                var countTimeID = 0;
                var ndx = 0;

                if (empUserLog != null)
                    countTimeID = empUserLog.Count;

                var userBreakLog = new BreakLog
                {
                    TimeId = LoggedInUser.TimeId,
                    BreakId = ndx + 1,
                    BreakIn = DateTime.Now,
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
                TimeLog LoggedInUser = empUserLog.Where(a => a.rocksUserID == empDtl.EmpID).FirstOrDefault();
                BreakLog takeBreak = empUserBreakLog.Where(a => a.TimeId == LoggedInUser.TimeId).FirstOrDefault();

                if (takeBreak != null)
                {
                    takeBreak.BreakOut = DateTime.Now;
                    takeBreak.IsTakingABreak = false;
                }
                this.IsOnBreak = false;
                

            }
            this.SignOut();
        }

        private void MoveToNextPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void SignOut()
        {
            Application.Current.MainPage = new Views.Navigation.NamesListPage();
        }

        #endregion

        
    }
}
