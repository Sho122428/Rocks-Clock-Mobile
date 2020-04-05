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

namespace RockClockMobile.ViewModels.Navigation
{
    /// <summary>
    /// ViewModel for names list page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class NamesListViewModel : BaseViewModel
    {
        #region Fields

        private Command<Employee> itemTappedCommand;
        EmployeeServices employeeServices = new EmployeeServices();       

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="NamesListViewModel"/> class.
        /// </summary>
        public NamesListViewModel()
        {
            NamesList = employeeServices.EmployeeList;
            //this.ClockInCommand = new Command(async () => await AddEmployeeTimeLog());
            //GetEmployee();
        }
        #endregion

        //public ObservableCollection<Employee> NamesList { get; set; }
        #region Properties

        public User User { get; set; }
        //public ObservableCollection<EmpSample> EmpSamples { get; set; }
        //public ObservableCollection<Employee> EmployeeList { get; set; }
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
        [DataMember(Name = "namesListPage")]
        public ObservableCollection<Employee> NamesList { get; set; }
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

        //public async Task<IEnumerable<EmpSample>> GetEmployee()
        //{
        //    IsBusy = true;

        //    try
        //    {
        //        NamesList = await EmployeeServices.GetRocksUsers(true);
        //        return NamesList;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }

        //    return NamesList;
        //}
        public async Task<User> GetUser()
        {
            IsBusy = true;

            var emp = GlobalServices.employee;

            try
            {
                User = await UserServices.GetUser(emp.rocksUserId);
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

        #endregion
    }
}
