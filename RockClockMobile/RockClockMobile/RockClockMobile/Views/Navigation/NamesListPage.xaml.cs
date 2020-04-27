using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using RockClockMobile.Custom;
using RockClockMobile.Models;
using RockClockMobile.Services;
using RockClockMobile.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RockClockMobile.Views.Navigation
{
    /// <summary>
    /// Page showing the list of names with grouping.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NamesListPage
    {
        int tapCount;
        public NamesListPage()
        {
            InitializeComponent();
            this.BindingContext = new NamesListViewModel();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });

            try
            {
                Analytics.TrackEvent("My custom event");

                Crashes.GenerateTestCrash();
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                {
                    { "Category", "Music" },
                    { "Wifi", "On" }
                };
                Crashes.TrackError(exception, properties);
            }

            tapCount = 0;
            //userServices = new UserServices(empDtl.rocksUserId);
        }

     

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>x
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                if (Search.IsVisible)
                {
                    Search.WidthRequest = width;
                }
            }
        }

        /// <summary>
        /// Invoked when search button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            this.SearchButton.IsVisible = false;
            this.Search.IsVisible = true;
            this.Title.IsVisible = false;

            if (this.TitleView != null)
            {
                double opacity;

                // Animating Width of the search box, from 0 to full width when it added to the view.
                var expandAnimation = new Animation(
                    property =>
                    {
                        Search.WidthRequest = property;
                        opacity = property / TitleView.Width;
                        Search.Opacity = opacity;
                    }, 0, TitleView.Width, Easing.Linear);
                expandAnimation.Commit(Search, "Expand", 16, 250, Easing.Linear);
            }

            SearchEntry.Focus();
        }

        /// <summary>
        /// Invoked when back to title button is clicked.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">Event Args</param>
        private void BackToTitle_Clicked(object sender, EventArgs e)
        {
            this.SearchButton.IsVisible = true;
            if (this.TitleView != null)
            {
                double opacity;

                // Animating Width of the search box, from full width to 0 before it removed from view.
                var shrinkAnimation = new Animation(property =>
                {
                    Search.WidthRequest = property;
                    opacity = property / TitleView.Width;
                    Search.Opacity = opacity;
                },
                TitleView.Width, 0, Easing.Linear);
                shrinkAnimation.Commit(Search, "Shrink", 16, 250, Easing.Linear, (p, q) => this.SearchBoxAnimationCompleted());
            }

            SearchEntry.Text = string.Empty;
        }

        /// <summary>
        /// Invokes when search box Animation completed.
        /// </summary>
        private void SearchBoxAnimationCompleted()
        {
            this.Search.IsVisible = false;
            this.Title.IsVisible = true;
        }
        
        private async void TapUserEvent(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {         
            if (tapCount > 1)
            {
                ToastPopup.ToastMessage("Request is loading, please wait.", false);
                await Task.Delay(2000);
                tapCount++;
            }
            else {
                tapCount++;
                NamesListViewModel namesListVM = (NamesListViewModel)this.BindingContext;
                var empSignIn = (Employee)e.ItemData == null ? null : (Employee)e.ItemData;

                var userLoginParam = new UserLoginParam
                {
                    RocksUserId = empSignIn.id,
                    Password = "1234",
                    Remember = true
                };

                //GlobalServices.employee = await namesListVM.GetEmployeeById(empSignIn.id);
                var userLoggedIn = await namesListVM.UserLoginById(userLoginParam);

                if (userLoggedIn != null)
                {
                    GlobalServices.employee = userLoggedIn.rocksUser;
                    Application.Current.Properties["user_id "] = empSignIn.id;

                    //for specific user
                    //var user = await userViewModel.GetUser();

                    //for all user
                    var user = (List<User>)await namesListVM.GetUserList();
                    string userPassword = string.Empty;
                    int lastUserId = 0;
                    User userData = user.Where(a => a.rocksUserId == empSignIn.id).FirstOrDefault();

                    if (userData != null)
                    {
                        userPassword = userData.password;
                    }
                    else
                    {
                        userPassword = "0";
                        lastUserId = user.OrderByDescending(a => a.id).Select(b => b.id).FirstOrDefault();
                    }

                    if (userLoggedIn.rocksUser.isTempPassword)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await namesListVM.OnLoadPage();
                            App.Current.MainPage = new Views.ResetPassword.ResetPasswordPage();
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await namesListVM.OnLoadPage();
                            App.Current.MainPage = new PincodePage(userPassword, lastUserId);
                        });
                    }
                }
                else {
                    ToastPopup.ToastMessage("User not found, please contact the administrator.",false);
                }
            }                       
           
         //App.Current.MainPage = new PincodePage(userPassword,lastUserId);
        }
        private void HeaderTappedEvent(object sender, EventArgs e)
        {
        }
    }
}