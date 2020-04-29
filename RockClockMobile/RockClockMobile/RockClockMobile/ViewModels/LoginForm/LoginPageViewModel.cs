using Microsoft.AppCenter.Crashes;
using RockClockMobile.Models;
using RockClockMobile.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.ViewModels.LoginForm
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        private string password;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel()
        {
            GetEmployeeDetail();
            //this.LoginCommand = new Command(async () => await AdminLoginAccount());
            //this.LoginCommand = new Command(async () => await LoginClicked(x));
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
    }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
               // this.NotifyPropertyChanged();
            }
        }

        public string UserEmail { get; set; }
        public User user { get; set; }
        private void NotifyPropertyChanged()
        {
            throw new NotImplementedException();
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

        public bool CanLogin { get; set; }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        //public ICommand LoginCommand { get; set; }
        public ICommand LoginCommand
        {
            get
            {
                return new Command<object>((x) => LoginClicked(x));
            }
        }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object x)
        {
            this.UserEmail = base.Email;

            var userLogin = new UserLogin
            {
                UserName = this.UserEmail,
                Password = "Fullsc@l3",
                Remember = true
            };

            CanLogin = await AdminUserLogin(userLogin);
        }


        //Get Admin login response
        private async Task<bool> AdminUserLogin(UserLogin userLogin)
        {
            IsBusy = true;
            
            try
            {
                var dd = await AccountService.AdminUserLogin(userLogin);
                return dd;
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

            return false;
        }

        private async Task GetEmployeeDetail()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var empList = await EmployeeServices.GetEmployeeList(true);
                GlobalServices.employeeList = empList;
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

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            var label = obj as Label;
            label.BackgroundColor = Color.FromHex("#70FFFFFF");
            await Task.Delay(100);
            label.BackgroundColor = Color.Transparent;
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }

}