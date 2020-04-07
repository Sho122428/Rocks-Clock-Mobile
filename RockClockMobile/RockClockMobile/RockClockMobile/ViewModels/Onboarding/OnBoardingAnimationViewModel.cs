using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        private List<string> rocksProjects;

        private string selectedProject = "";

        private bool isProjectButtonVisible = true;

        private string nextButtonText = "NEXT";

        private bool isSkipButtonVisible = true;

        private int selectedIndex;

        private string clockinButtonText = "CLOCK IN";

        private string breakButtonText = "Start Break";

        private bool isBreakButtonVisible = false;

        private bool isSignOutButtonVisible = true;

        private string fNameUser = "";

        private bool isClockedIn = false;

        private bool isOnBreak = false;

        private bool isClockOutBtnVisible = false;



        #endregion

        Employee empDtl = GlobalServices.employee;
        List<TimeLog> empUserLog = GlobalServices.EmployeeTime;
        List<BreakLog> empUserBreakLog = GlobalServices.EmployeeBreak;

        public ObservableCollection<TimeLog> TimeLogs { get; set; }

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OnBoardingAnimationViewModel" /> class.
        /// </summary>
        public OnBoardingAnimationViewModel()
        {
            //this.SkipCommand = new Command(this.Skip);
            //this.NextCommand = new Command(this.Next);
            //this.ClockInCommand = new Command(this.ClockIn);
            this.BreakCommand = new Command(this.Break);
            this.SignOutCommand = new Command(this.SignOut);
            
            this.LoadEmployeeListCommand = new Command(async () => await GetEmployeeTimeLogList());

            this.ClockInCommand = new Command(async () => await AddEmployeeTimeLog());
            this.ClockOutCommand = new Command(async () => await UpdateEmployeeTimeLog());
            

            TimeLogs = new ObservableCollection<TimeLog>();
            

            this.RocksProjects = new List<string>();

            foreach (var proj in empDtl.rocksUserProjectMaps)
            {
                RocksProjects.Add(proj.rocksProjectId.ToString());
            }

            LoadDataClock();

            this.Boardings = new ObservableCollection<Boarding>
            {
                new Boarding()
                {
                    //ImagePath = "ReSchedule.png",
                    Header = this.FNameUser,
                    //Content = "Drag and drop meetings in order to reschedule them easily.",
                    RotatorItem = new WalkthroughItemPage()
                }

            };





            // Set bindingcontext to content view.
            foreach (var boarding in this.Boardings)
            {
                boarding.RotatorItem.BindingContext = boarding;
            }
        }

        async void LoadDataClock()
        {
            await GetEmployeeTimeLogList();
            try
            {
                if (TimeLogs != null)
                {
                    TimeLog LoggedInUser = TimeLogs.Where(a => a.rocksUserId == empDtl.id).FirstOrDefault();

                    if (LoggedInUser != null)
                    {
                        this.IsBreakButtonVisible = true;
                        this.ClockInButtonText = "CLOCK OUT";
                        this.IsClockedIn = true;
                        this.FNameUser = empDtl.firstName + " clocked in at " + LoggedInUser.timeIn.ToString("h:mm tt") + System.Environment.NewLine + " for project "; //+ LoggedInUser.projectName;
                        this.ClockInButtonText = "Clock out from "; //+ LoggedInUser.projectName;
                        this.IsProjectButtonVisible = false;


                    }
                    else
                    {
                        this.FNameUser = empDtl.firstName + " is off the clock.";
                        this.ClockInButtonText = "Clock in to " + RocksProjects[0];
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
                        BreakLog takeBreak = empUserBreakLog.Where(a => a.timeLogId == LoggedInUser.timeLogId).FirstOrDefault();

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
                else
                {
                    this.FNameUser = empDtl.firstName + " is off the clock.";
                    this.ClockInButtonText = "Clock in to " + empDtl.rocksUserProjectMaps.Select(a => a.rocksProject).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
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

        public List<string> RocksProjects
        {
            get
            {
                return this.rocksProjects;
            }

            set
            {
                if (this.rocksProjects == value)
                {
                    return;
                }

                this.rocksProjects = value;
                this.OnPropertyChanged();
            }
        }


        public string SelectedProject
        {
            get
            {
                return this.selectedProject;
            }

            set
            {
                if (this.selectedProject == value)
                {
                    return;
                }

                this.selectedProject = value;
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

        public bool IsProjectButtonVisible
        {
            get
            {
                return this.isProjectButtonVisible;
            }

            set
            {
                if (this.isProjectButtonVisible == value)
                {
                    return;
                }

                this.isProjectButtonVisible = value;
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
        public ICommand ClockOutCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Break button is clicked.
        /// </summary>
        public ICommand BreakCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign out button is clicked.
        /// </summary>
        public ICommand SignOutCommand { get; set; }

        public ICommand LoadEmployeeListCommand { get; set; }

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
            if (!IsClockedIn)
            {
                var countTimeID = 0;

                if (empUserLog != null)
                    countTimeID = empUserLog.Count;

                var userTimeLog = new TimeLog
                {
                    timeLogId = countTimeID + 1,
                    rocksUserId = empDtl.id,
                    timeIn = DateTime.Now,
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
                TimeLog LoggedInUser = empUserLog.Where(a => a.rocksUserId == empDtl.id).FirstOrDefault();

                if (LoggedInUser != null)
                {
                    LoggedInUser.timeOut = DateTime.Now;
                    //LoggedInUser.IsClockedOut = true;
                }


            }
            this.SignOut();
        }
        private void Break(object obj)
        {

            if (!IsOnBreak)
            {
                TimeLog LoggedInUser = empUserLog.Where(a => a.rocksUserId == empDtl.id).FirstOrDefault();
                var countTimeID = 0;
                var ndx = 0;

                if (empUserLog != null)
                    countTimeID = empUserLog.Count;

                var userBreakLog = new BreakLog
                {
                    timeLogId = LoggedInUser.timeLogId,
                    id = ndx + 1,
                    breakIn = DateTime.Now,
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
                TimeLog LoggedInUser = empUserLog.Where(a => a.rocksUserId == empDtl.id).FirstOrDefault();
                BreakLog takeBreak = empUserBreakLog.Where(a => a.timeLogId == LoggedInUser.timeLogId).FirstOrDefault();

                if (takeBreak != null)
                {
                    takeBreak.breakOut = DateTime.Now;
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

        #region
        /// <summary>
        /// Invoke API calls
        /// </summary>
        /// <returns></returns>
        private async Task TestingOnly()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                var items = await TimeLogServices.GetEmployeeTimeLog(1);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetEmployeeTimeLog()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                var items = await TimeLogServices.GetEmployeeTimeLog(empDtl.id);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetEmployeeTimeLogList()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                TimeLogs.Clear();
                var timelogs = await TimeLogServices.GetEmployeeTimeLogList(true);
                foreach (var tlog in timelogs)
                {
                    TimeLogs.Add(tlog);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AddEmployeeTimeLog()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var countTimeID = 0;

                if (empUserLog != null)
                    countTimeID = empUserLog.Count;

                var userTimeLog = new TimeLog
                {
                    //timeLogId = countTimeID + 1,
                    //rocksUserId = empDtl.rocksUserId,
                    //timeIn = DateTime.Now,
                    //projectID = 1,
                    //status = 0,
                    //isDeleted = false

                    
                    projectID = 4,
                    rocksUserId= empDtl.id,
                    timeIn= DateTime.UtcNow,
                    timeOut = DateTime.UtcNow,
                    status= 1,
                    isDeleted= false
                    
                };
                var items = await TimeLogServices.AddEmployeeTimeLog(userTimeLog);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                this.SignOut();
            }
        }

        private async Task UpdateEmployeeTimeLog()
        {

        }

        #endregion    
    }
}
