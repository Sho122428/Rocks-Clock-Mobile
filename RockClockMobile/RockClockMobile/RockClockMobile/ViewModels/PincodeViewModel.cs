using Microsoft.AppCenter.Crashes;
using RockClockMobile.Custom;
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
        private bool isLoggedIn = false;
        public PincodeViewModel()
        {
            //UserServices employeeServices = new UserServices();
            //UserList = employeeServices.UserList;

            UserList = GlobalServices.User;
            this.IsLoggedIn = true;
            TimeStartLogout();
        }

        #region Properties      
        private string email;     
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

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                if (this.email == value)
                {
                    return;
                }

                this.email = value;
                //this.NotifyPropertyChanged();
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

        #endregion

        #region Methods

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
        public bool ValidatePin(int pin)
        {
            if (pin != 123)
            {
                return false;
            }

            return true;
            
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

        private async void SignOut()
        {
            this.IsLoggedIn = false;
            IsBusy = true;
            IsBusyOpacity = .5;
            await Task.Delay(3000);
            IsBusy = false;
            IsBusyOpacity = 1;

            Application.Current.MainPage = new Views.Navigation.NamesListPage();
        }

        #endregion        
    }
}
