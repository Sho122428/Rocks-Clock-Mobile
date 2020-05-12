using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using RockClockMobile.Custom;
using RockClockMobile.Models;
using RockClockMobile.Models.Onboarding;
using RockClockMobile.Services;
using RockClockMobile.Enums;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Microsoft.AppCenter.Crashes;

namespace RockClockMobile.ViewModels.Onboarding
{
    /// <summary>
    /// ViewModel for on-boarding gradient page with animation.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TimeClockViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<Boarding> boardings;

        private List<string> rocksProjects;

        private string selectedProject = "";
        private int selectedProjectID = 0;

        private bool isProjectButtonVisible = true;

        private int selectedIndex;

        private string clockinButtonText = "Clock In";

        private string breakButtonText = "Start Break";

        private bool isClockOutButtonVisible = false;

        private bool isBreakButtonVisible = false;

        private bool isSignOutButtonVisible = true;

        private string fNameUser = "";

        private bool isLoggedIn = false;

        private bool isOnBreak = false;

        private string clockIn = "--:--";

        private string breakStart = "--:--";

        private string breakStop = "--:--";

        private int timeLogStatus = 0;

        private int brkButtonCSpan = 1;

        #endregion

        RocksUser empDtl = GlobalServices.employee;
        List<TimeLog> empUserLog = GlobalServices.EmployeeTime;
        List<BreakLog> empUserBreakLog = GlobalServices.EmployeeBreak;

        public List<TimeLog> TimeLogs { get; set; }
        public TimeLog LoggedInUser;
        public TimeLogVM timeLogVM;

        #region Constructor
        /// <summary>
        /// Initializes a new instance for the <see cref="TimeClockViewModel" /> class.
        /// </summary>
        public TimeClockViewModel()
        {

            this.SignOutCommand = new Command(this.SignOut);
            this.ClockInCommand = new Command(async () => await EmployeeClockIn());
            this.ClockOutCommand = new Command(async () => await EmployeeClockOut(empDtl.id));


            TimeLogs = new List<TimeLog>();

            LoadRocksProject();

            this.IsLoggedIn = true;


            LoadDataClock();
        }

        private void LoadRocksProject()
        {
            this.RocksProjects = new List<string>();

            int ndxproj = 0;
            foreach (var proj in empDtl.rocksUserProjectMaps)
            {
                if (ndxproj == 0)
                {
                    this.SelectedProject = proj.rocksProject.projectName;
                    this.SelectedProjectID = proj.rocksProjectId;
                    ndxproj += 1;
                }
                RocksProjects.Add(proj.rocksProject.projectName);
            }
        }

        async void LoadDataClock()
        {
            LoggedInUser = new TimeLog();
            await GetTimeLogStatus(empDtl.id);
            await GetTimeLogButtonAccess(empDtl.id);
            await GetEmployeeTimeLog(empDtl.id);

            try
            {
                string clockInTime = string.Empty;
                string breakInTime = string.Empty;
                string currentUser = string.Empty;
                if (timeLogVM.Status.ToString() != "ForClockIn")
                {
                    clockInTime = timeLogVM.TimeIn.Value.ToLocalTime().ToString("h:mm tt");
                    breakInTime = timeLogVM.Start.Value.ToLocalTime().ToString("h:mm tt");
                    currentUser = empDtl.firstName + " clocked in at " + timeLogVM.TimeIn.Value.ToLocalTime().ToString("h:mm tt") + System.Environment.NewLine + " for project " + SelectedProject;

                    if (timeLogVM.Status.ToString() == "HasClockedInData")
                    {
                        this.ClockIn = clockInTime;
                    }
                    if (timeLogVM.Status.ToString() == "HasBreakInData")
                    {
                        this.ClockIn = clockInTime;
                        this.BreakStart = breakInTime;
                    }
                }                
              
                if (UserButtonAccess.CanTimeIn)
                {
                    this.BrkButtonCSpan = 1;
                    this.FNameUser = empDtl.firstName + " is off the clock.";
                }
                if (UserButtonAccess.CanTimeOut)
                {
                    this.IsClockOutButtonVisible = true;
                    this.BrkButtonCSpan = 1;
                    this.IsProjectButtonVisible = false;
                    this.FNameUser = currentUser;        
                }
                if (UserButtonAccess.CanBreakIn)
                {
                    this.BrkButtonCSpan = 1;
                    this.IsBreakButtonVisible = UserButtonAccess.CanBreakIn;
                    this.FNameUser = currentUser;
                }
                if (UserButtonAccess.CanBreakOut)
                {
                    this.BrkButtonCSpan = 40;
                    this.BreakButtonText = "End Break";
                    this.IsBreakButtonVisible = UserButtonAccess.CanBreakOut;
                    this.IsProjectButtonVisible = false;
                    this.FNameUser = currentUser;
                }

                TimeStartLogout();
            }
            catch (Exception ex)
            {
                //var msg = ex.Message;
                Crashes.TrackError(ex);
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
        public int SelectedProjectID
        {
            get
            {
                return this.selectedProjectID;
            }

            set
            {
                if (this.selectedProjectID == value)
                {
                    return;
                }

                this.selectedProjectID = value;
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

        public bool IsClockOutButtonVisible
        {
            get
            {
                return this.isClockOutButtonVisible;
            }

            set
            {
                if (this.isClockOutButtonVisible == value)
                {
                    return;
                }

                this.isClockOutButtonVisible = value;
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

        public bool IsLoggedIn
        {
            get
            {
                return this.isLoggedIn;
            }

            set
            {
                if (this.isLoggedIn == value)
                {
                    return;
                }

                this.isLoggedIn = value;
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

        public int TimeLogStatus
        {
            get
            {
                return this.timeLogStatus;
            }

            set
            {
                if (this.timeLogStatus == value)
                {
                    return;
                }

                this.timeLogStatus = value;
                this.OnPropertyChanged();
            }
        }

        public int BrkButtonCSpan
        {
            get
            {
                return this.brkButtonCSpan;
            }

            set
            {
                if (this.brkButtonCSpan == value)
                {
                    return;
                }

                this.brkButtonCSpan = value;
                this.OnPropertyChanged();
            }
        }



        public string ClockIn
        {
            get
            {
                return this.clockIn;
            }

            set
            {
                if (this.clockIn == value)
                {
                    return;
                }

                this.clockIn = value;
                this.OnPropertyChanged();
            }
        }

        public string BreakStart
        {
            get
            {
                return this.breakStart;
            }

            set
            {
                if (this.breakStart == value)
                {
                    return;
                }

                this.breakStart = value;
                this.OnPropertyChanged();
            }
        }

        public string BreakStop
        {
            get
            {
                return this.breakStop;
            }

            set
            {
                if (this.breakStop == value)
                {
                    return;
                }

                this.breakStop = value;
                this.OnPropertyChanged();
            }
        }

        public ButtonAccess UserButtonAccess { get; set; }

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
        //public ICommand BreakCommand { get; set; }
        public ICommand BreakCommand
        {
            get
            {
                return new Command<object>((x) => AddAndUpdateEmployeeBreakLog(x));
            }
        }

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

        private void TimeStartLogout()
        {

            Device.StartTimer(TimeSpan.FromSeconds(30), () =>
            {
                if (IsLoggedIn)
                {
                    ToastPopup.ToastMessage("You have been inactive. Logging out.", true);
                    this.SignOut();
                }
                return false;
            });
        }

        private void Break(object obj)
        {

            if (!IsOnBreak)
            {
                TimeLog LoggedInUser = empUserLog.Where(a => a.RocksUserId == empDtl.id).FirstOrDefault();
                var countTimeID = 0;
                var ndx = 0;

                if (empUserLog != null)
                    countTimeID = empUserLog.Count;

                var userBreakLog = new BreakLog
                {
                    timeLogId = LoggedInUser.id,
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
                TimeLog LoggedInUser = empUserLog.Where(a => a.RocksUserId == empDtl.id).FirstOrDefault();
                BreakLog takeBreak = empUserBreakLog.Where(a => a.timeLogId == LoggedInUser.id).FirstOrDefault();

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

        private async void SignOut()
        {
            this.IsLoggedIn = false;
            IsBusy = true;
            IsBusyOpacity = .5;
            ToastPopup.ToastMessage("Signing out...",false);
            await Task.Delay(2000);
            IsBusy = false;
            IsBusyOpacity = 1;

            Application.Current.MainPage = new Views.Navigation.NamesListPage();            
        }
        #endregion

        #region API Calls
        /// <summary>
        /// Invoke API calls
        /// </summary>
        /// <returns></returns>

        private async Task GetEmployeeTimeLog(int rocksUserID)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                var items = await TimeLogServices.GetEmployeeTimeLog(rocksUserID);
                LoggedInUser = items;
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                Crashes.TrackError(ex);
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
            IsBusyOpacity = .5;
            try
            {
                TimeLogs.Clear();
                var timelogs = await TimeLogServices.GetEmployeeTimeLogList(true);
                IEnumerable<TimeLog> userTimeLog = timelogs.Where(a => a.user_id == empDtl.id);

                foreach (var tlog in userTimeLog)
                {
                    await Task.Run(() => { TimeLogs.Add(tlog); }).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
                IsBusyOpacity = 1;
            }
        }

        
        private async Task GetTimeLogStatus(int rocksUserID)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsBusyOpacity = .5;
            try
            {
                timeLogVM = await TimeLogServices.GetTimeLogStatus(rocksUserID);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
                IsBusyOpacity = 1;
            }
        }

        private async Task GetTimeLogButtonAccess(int rocksUserID)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsBusyOpacity = .5;
            try
            {
                UserButtonAccess = await TimeLogServices.GetTimeLogButtonAccess(rocksUserID);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
                IsBusyOpacity = 1;
            }
        }

        private async Task AddEmployeeTimeLog()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            IsBusyOpacity = .5;
            ToastPopup.ToastMessage("Clocking In.", true);
            await Task.Delay(3000);

            try
            {
                var countTimeID = 0;

                if (empUserLog != null)
                    countTimeID = empUserLog.Count;

                int projid = 0;

                foreach (var proj in empDtl.rocksUserProjectMaps)
                {
                    if (proj.rocksProject.projectName == SelectedProject)
                    {
                        projid = proj.rocksProjectId;
                    }
                }

                var userTimeLog = new TimeLog
                {
                    RocksUserId = empDtl.id,
                    ProjectID = projid,
                    Start = DateTime.UtcNow,
                    createddt = DateTime.UtcNow
                };
                var isSuccess = await TimeLogServices.AddEmployeeTimeLog(userTimeLog);
                if (isSuccess)
                {
                    ToastPopup.ToastMessage("You have successfully logged in.", false);
                    await Task.Delay(3000);
                    this.SignOut();
                }
                else
                {
                    ToastPopup.ToastMessage("An error occured while attempting to log in.", true);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                Crashes.TrackError(ex);
                ToastPopup.ToastMessage("An error occured while attempting to log in.", true);
            }
            finally
            {
                IsBusy = false;
                IsBusyOpacity = 1;
                this.SignOut();

            }
        }

        private async Task UpdateEmployeeTimeLog(TimeLog tlog)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsBusyOpacity = .5;
            ToastPopup.ToastMessage("Clocking out...", true);
            await Task.Delay(3000);

            try
            {
                tlog.End = DateTime.UtcNow;

                var isSuccess = await TimeLogServices.UpdateEmployeeTimeLog(tlog);

                if (isSuccess)
                {
                    ToastPopup.ToastMessage("Successfully clocked out.", false);
                    await Task.Delay(3000);
                    this.SignOut();
                }
                else
                {
                    ToastPopup.ToastMessage("An error occured saving the record.", true);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
                IsBusyOpacity = 1;
                //ToastPopup.ToastMessage("End Time must be greater than start time.", true);
                this.SignOut();
            }
        }

        private async Task EmployeeClockIn()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsBusyOpacity = .5;
            ToastPopup.ToastMessage("Clocking in...", true);
            await Task.Delay(3000);

            try
            {
                var userTimeLog = new TimeLog
                {
                    RocksUserId = empDtl.id,
                    Start = DateTime.UtcNow,
                    createddt = DateTime.UtcNow
                };

                var isSuccess = await TimeLogServices.ClockIn(this.SelectedProjectID, empDtl.id, userTimeLog);

                if (isSuccess)
                {
                    ToastPopup.ToastMessage("Successfully clocked in.", false);
                    await Task.Delay(3000);
                    this.SignOut();
                }
                else
                {
                    ToastPopup.ToastMessage("An error occured saving the record.", true);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
                IsBusyOpacity = 1;
                //ToastPopup.ToastMessage("End Time must be greater than start time.", true);
                this.SignOut();
            }
        }
        private async Task EmployeeClockOut(int rocksUserID)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsBusyOpacity = .5;
            ToastPopup.ToastMessage("Clocking out...", true);
            await Task.Delay(3000);

            try
            {


                var isSuccess = await TimeLogServices.ClockOut(rocksUserID);

                if (isSuccess)
                {
                    ToastPopup.ToastMessage("Successfully clocked out.", false);
                    await Task.Delay(3000);
                    this.SignOut();
                }
                else
                {
                    ToastPopup.ToastMessage("An error occured saving the record.", true);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
                IsBusyOpacity = 1;
                //ToastPopup.ToastMessage("End Time must be greater than start time.", true);
                this.SignOut();
            }
        }

        //For BreakLogs
        private async Task AddAndUpdateEmployeeBreakLog(object timeClockDtl)
        {
            if (IsBusy)
                return;

            string message = string.Empty;
            string commandText = BreakButtonText.ToLower();
            bool isSuccess = false;


            IsBusy = true;
            IsBusyOpacity = .5;
            ToastPopup.ToastMessage("Logging your break.", true);
            await Task.Delay(3000);

            try
            {
                int timeLogId = LoggedInUser.id;

                if (commandText == "start break")
                {
                    //BreakLog breakLog = new BreakLog
                    //{
                    //    breakIn = DateTime.UtcNow,
                    //    timeLogId = timeLogId,
                    //    modifiednotes = "app testing break log"
                    //};

                    isSuccess = await BreakLogServices.AddEmployeeBreakLog(empDtl.id);
                    message = "Enjoy your break.";
                }
                else
                {
                    //isSuccess = await BreakLogServices.BreakOut(LoggedInUser.id);
                    isSuccess = await BreakLogServices.BreakOut(empDtl.id);

                    if (isSuccess)
                        message = "Welcome back. Enjoy your work.";
                }

                if (isSuccess)
                {

                    ToastPopup.ToastMessage(message, false);
                    await Task.Delay(3000);
                    this.SignOut();
                }
                else
                {
                    ToastPopup.ToastMessage("An error occured while attempting to break.", true);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                ToastPopup.ToastMessage("An error occured while attempting to break.", true);
            }
            finally
            {
                IsBusy = false;
                IsBusyOpacity = 1;
                this.SignOut();
            }
        }

        

        #endregion
    }
}
