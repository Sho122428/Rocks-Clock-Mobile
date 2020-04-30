using Xamarin.Forms;
using RockClockMobile.Models.Navigation;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;
using RockClockMobile.Models;
using RockClockMobile.Services;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AppCenter.Crashes;

namespace RockClockMobile.ViewModels.Navigation
{
    /// <summary>
    /// ViewModel for names list page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class NamesListViewModel : BaseViewModel
    {
        #region Fields

        private Command<Employee> itemTappedCommand;
        //EmployeeServices employeeServices = new EmployeeServices();
        public ObservableCollection<Employee> NamesList { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="NamesListViewModel"/> class.
        /// </summary>
        public NamesListViewModel()
        {
            NamesList = new ObservableCollection<Employee>();
            GetEmployeeList();
        }
        #endregion

        //public ObservableCollection<Employee> NamesList { get; set; }
        #region Properties
        public User User { get; set; }
        public User UserList { get; set; }
        public ICommand ClockInCommand { get; set; }

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<Employee> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<Employee>(this.NavigateToNextPage));
            }
        }      

        /// <summary>
        /// Gets or sets a collction of value to be displayed in contacts list page.
        /// </summary>

        //public IEnumerable<EmpSample> NamesList { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected from the movies list.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private async void NavigateToNextPage(object selectedItem)
        {
            // Do something
            //await OnLoadPage();
        }

        //Get specific User from Rocks Clock
        public async Task<User> GetUser()
        {
            IsBusy = true;

            var emp = GlobalServices.employee;

            try
            {
                User = await UserServices.GetUser(emp.id);
                return User;
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

            return User;
        }

        //Get all User from Rocks Clock
        public async Task<User> GetUserList(int userId)
        {
            IsBusy = true;

            var emp = GlobalServices.employee;

            try
            {
                UserList = await UserServices.GetUserList(true, userId);
                return UserList;
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

            return null;
        }

        //Adds User from Rocks Clock
        public async Task AddUser(string pin,int userId)
        {
            IsBusy = true;

            var empFromRocks = GlobalServices.employee;

            User userToAdd = new User{
                password = pin,
                attempts = 0,
                isLocked = false,
                isTempPassword = false,
                rocksUserId = empFromRocks.id,
                status = 1,
                isDeleted = false,
                id = userId +1,
                userRole = new UserRole{ 
                    roleId = 2,
                    userId = 0
                }
            };

            try
            {
                var user = await UserServices.AddUser(userToAdd);
                //return User;
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

            //return User;
        }

        //Get all Employee from Rocks
        private async Task GetEmployeeList()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                NamesList.Clear();
                var employeeFromAPI = await EmployeeServices.GetEmployeeList(true);

                //var employeeFromAPI = GlobalServices.employeeList;
                IEnumerable<Employee> employeeList = employeeFromAPI.OrderBy(a => a.firstName);
                GlobalServices.employeeList = employeeList;
                foreach (var dtl in employeeList)
                {
                    NamesList.Add(dtl);
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
            }
        }

        //Get specific Employee from Rocks
        public async Task<Employee> GetEmployeeById(int id)
        {
            IsBusy = true;

            try
            {
                Employee employee = await EmployeeServices.GetEmployeeById(id);
                return employee;
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

            return null;
        }

        public async Task<UserLoginM> UserLogin(UserLoginParam userLoginParam)
        {
            IsBusy = true;

            try
            {
                UserLoginM employee = await AccountService.UserLogin(userLoginParam);
                return employee;
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

            return null;
        }

        //For loading screen
        private bool visible = false;
        private string email;

        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                OnPropertyChanged("Visible");
            }
        }

        private bool isLoading = false;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private double isLoadingOpacity = 1;
        public double IsLoadingOpacity
        {
            get { return isLoadingOpacity; }
            set
            {
                isLoadingOpacity = value;
                OnPropertyChanged("IsLoadingOpacity");
            }
        }

        private bool enable = true;
        public bool Enable
        {
            get { return enable; }
            set
            {
                enable = value;
                OnPropertyChanged("Enable");
            }
        }
        public async Task OnLoadPage()
        {
            Visible = true;
            IsLoading = true;
            IsLoadingOpacity = .5;
            Enable = false;
            await Task.Delay(3000);
            try
            {

            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            //var test = await App.MobileService.GetTable<UserHeader>().Where(a => a.Username == "JANNOTIMOTHYPONO").ToListAsync();


            IsLoading = false;
            Visible = false;
            IsLoadingOpacity = 1;
            Enable = true;
        }

        #endregion
    }
}
