using RockClockMobile.Models;
using RockClockMobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
            Device.BeginInvokeOnMainThread(async () =>
            {
                await OnLoadPage();
            });

            UserList = GlobalServices.User;
        }

        #region Properties
        private bool visible = false;

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
        #endregion

        #region Methods

        private async Task OnLoadPage()
        {
            Visible = true;
            IsLoading = true;
            IsLoadingOpacity = .5;
            await Task.Delay(3000);
            try
            {             

            }
            catch (Exception ex)
            {

            }
            //var test = await App.MobileService.GetTable<UserHeader>().Where(a => a.Username == "JANNOTIMOTHYPONO").ToListAsync();


            IsLoading = false;
            Visible = false;
            IsLoadingOpacity = 1;
        }
        public bool ValidatePin(int pin)
        {
            if (pin != 123)
            {
                return false;
            }

            return true;
        }

        #endregion        
    }
}
