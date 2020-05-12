using Microsoft.AppCenter.Crashes;
using RockClockMobile.Custom;
using RockClockMobile.Models;
using RockClockMobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RockClockMobile.ViewModels
{
    public class PincodeViewModel : BaseViewModel
    {
        public User UserList { get; set; }
        private bool isLoggedIn = false;
        public PincodeViewModel()
        {
            this.SignInCommand = new Command(this.SignIn);

            UserList = GlobalServices.User;
            this.IsLoggedIn = true;
            TimeStartLogout();
        }

        #region Commands

        public ICommand SignInCommand { get; set; }

        #endregion

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

        private string pinCode = "1234";
        public string PinCode
        {
            get { return this.pinCode; }
            set
            {
                this.pinCode = value;
                OnPropertyChanged("PinCode");
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

            IsLoading = false;
            Visible = false;
            IsLoadingOpacity = 1;
            Enable = true;
        }

        private void TimeStartLogout()
        {
            Device.StartTimer(TimeSpan.FromSeconds(30), () =>
            {
                if (IsLoggedIn)
                {
                    ToastPopup.ToastMessage("You have been inactive. Redirecting to kiosk home page.", true);
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

        public async Task<User> GetUserList(int userId)
        {
            IsBusy = true;
            Visible = true;
            IsLoading = true;
            IsLoadingOpacity = .5;
            Enable = false;
            await Task.Delay(1000);

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
                IsLoading = false;
                Visible = false;
                IsLoadingOpacity = 1;
                Enable = true;
            }

            return null;
        }

        public async Task<UserLoginM> UserLogin(UserLoginParam userLoginParam)
        {
            IsBusy = true;
            Visible = true;
            IsLoading = true;
            IsLoadingOpacity = .5;
            Enable = false;
            await Task.Delay(1000);

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
                IsLoading = false;
                Visible = false;
                IsLoadingOpacity = 1;
                Enable = true;
            }

            return null;
        }

        private async void SignIn()
        {
            //Sets the flag of automating to redirect to home to false;
            this.IsLoggedIn = false;

            if (string.IsNullOrWhiteSpace(PinCode) || PinCode.Length > 4)
            {
                ToastPopup.ToastMessage("Pin contains whitespaces or incorrect.", false);
                await Task.Delay(2000);
            }
            else 
            { 
                var userId = GlobalServices.ClockingInRocksUserID;
                var userLoginParam = new UserLoginParam
                {
                    RocksUserId = userId,
                    Password = PinCode,
                    Remember = true
                };
                var userLoggedIn = await UserLogin(userLoginParam);

                if (userLoggedIn != null)
                {
                    GlobalServices.employee = userLoggedIn.rocksUser;
                    var userSignedDetails = await GetUserList(userId);

                    if (userSignedDetails.isTempPassword && userSignedDetails != null)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await OnLoadPage();
                            App.Current.MainPage = new Views.ResetPassword.ResetPasswordPage(userId);
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await OnLoadPage();
                            //this.IsLoggedIn = false;
                            //await Navigation.PushModalAsync(new NavigationPage(new Onboarding.OnBoardingAnimationPage()));
                            App.Current.MainPage = new Views.Onboarding.OnBoardingAnimationPage();
                        });
                    }
                }
                else
                {
                    ToastPopup.ToastMessage("Pin is incorrect.", false);
                    await Task.Delay(2000);
                }
            }
            
        }
        #endregion        
    }
}
