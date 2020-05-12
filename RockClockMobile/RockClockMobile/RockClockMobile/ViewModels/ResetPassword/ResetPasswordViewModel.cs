using Microsoft.AppCenter.Crashes;
using RockClockMobile.Custom;
using RockClockMobile.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RockClockMobile.ViewModels.ResetPassword
{
    /// <summary>
    /// ViewModel for reset password page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ResetPasswordViewModel : BaseViewModel
    {
        #region Fields
        private string currentPassword;
        private string newPassword;
        private string confirmPassword;
        private bool isLoggedIn = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordViewModel" /> class.
        /// </summary>
        public ResetPasswordViewModel()
        {
            this.SubmitCommand = new Command(this.SubmitClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            IsLoggedIn = true;
            //TimeStartLogout();
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Submit button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Public property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the new password from user in the reset password page.
        /// </summary>
        /// 

        public int UserId { get; set; }
        public bool IsEnable { get; set; }
        public bool IsPasswordUpdated { get; set; }

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
        public string CurrentPassword
        {
            get
            {
                return this.currentPassword;
            }

            set
            {
                if (this.currentPassword == value)
                {
                    return;
                }

                this.currentPassword = value;
                //this.NotifyPropertyChanged();
            }
        }
        public string NewPassword
        {
            get
            {
                return this.newPassword;
            }

            set
            {
                if (this.newPassword == value)
                {
                    return;
                }

                this.newPassword = value;
                //this.NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the new password confirmation from the user in the reset password page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                this.confirmPassword = value;
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

        /// <summary>
        /// Invoked when the Submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SubmitClicked(object obj)
        {
            // Do something
            try {
                ChangePasswordVM changePasswordVM = new ChangePasswordVM();
                changePasswordVM.Password = this.NewPassword;
                changePasswordVM.ConfirmPassword = this.ConfirmPassword;
                changePasswordVM.currentPassword = this.CurrentPassword;
                changePasswordVM.Id = this.UserId;
                changePasswordVM.isWeb = false;
                IsPasswordUpdated = true;
                IsLoggedIn = false;

                if (changePasswordVM.Password.Length < 4)
                {
                    ToastPopup.ToastMessage("Pin length should be 4 digit numbers.", false);
                    IsPasswordUpdated = false;
                }
                else {
                    if (changePasswordVM.Password != changePasswordVM.ConfirmPassword)
                    {
                        ToastPopup.ToastMessage("New pin and confirm new pin not matched.", false);
                        IsPasswordUpdated = false;
                    }
                    else
                    {
                        if (await ChangePassword(changePasswordVM))
                        {
                            ToastPopup.ToastMessage("Pin successfully updated.", false);
                        }
                        else
                        {
                            ToastPopup.ToastMessage("Error on updating pin.", false);
                            IsPasswordUpdated = false;
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                ToastPopup.ToastMessage(ex.Message,true);
                IsPasswordUpdated = false;
            }    
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do something
        }

        private async Task<bool> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            IsBusy = true;

            try
            {
                var dd = await AccountService.ChangePassword(changePasswordVM);
                return dd;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            return false;
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
            ToastPopup.ToastMessage("Signing out...", false);
            await Task.Delay(2000);
            IsBusy = false;
            IsBusyOpacity = 1;

            Application.Current.MainPage = new Views.Navigation.NamesListPage();
        }

        #endregion
    }
}