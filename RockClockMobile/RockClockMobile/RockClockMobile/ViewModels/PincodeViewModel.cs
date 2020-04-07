using RockClockMobile.Models;
using RockClockMobile.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace RockClockMobile.ViewModels
{
    public class PincodeViewModel : BaseViewModel
    {
        public User UserList { get; set; }

        public PincodeViewModel()
        {
            //UserServices employeeServices = new UserServices();
            //UserList = employeeServices.UserList;

            UserList = GlobalServices.User;
        }        
        public bool ValidatePin(int pin)
        {
            if (pin != 123)
            {
                return false;
            }

            return true;
        }
    }
}
