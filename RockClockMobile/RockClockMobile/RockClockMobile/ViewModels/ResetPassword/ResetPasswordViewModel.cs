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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordViewModel" /> class.
        /// </summary>
        public ResetPasswordViewModel()
        {
            this.SubmitCommand = new Command(this.SubmitClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
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

                if (changePasswordVM.Password.Length < 4)
                {
                    ToastPopup.ToastMessage("Pincode length should be 4 digit numbers.", false);
                }
                else {
                    if (changePasswordVM.Password != changePasswordVM.ConfirmPassword)
                    {
                        ToastPopup.ToastMessage("New password and confirm new password not matched.", false);
                    }
                    else
                    {
                        if (await ChangePassword(changePasswordVM))
                        {
                            ToastPopup.ToastMessage("Pincode successfully updated.", false);
                        }
                        else
                        {
                            ToastPopup.ToastMessage("Error on updating pincode.", false);
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                ToastPopup.ToastMessage(ex.Message,true);
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

        #endregion
    }
}