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
        public IEnumerable<User> UserList { get; set; }
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
        private void NavigateToNextPage(object selectedItem)
        {
            // Do something
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
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            return User;
        }

        //Get all User from Rocks Clock
        public async Task<IEnumerable<User>> GetUserList()
        {
            IsBusy = true;

            var emp = GlobalServices.employee;

            try
            {
                UserList = await UserServices.GetUserList();
                return UserList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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
                Debug.WriteLine(ex);
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
                IEnumerable<Employee> employeeList = employeeFromAPI.OrderBy(a => a.firstName);
                foreach (var dtl in employeeList)
                {
                    NamesList.Add(dtl);
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

        #endregion
    }
}
