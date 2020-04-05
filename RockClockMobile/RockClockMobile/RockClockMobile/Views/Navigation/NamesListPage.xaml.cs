using RockClockMobile.Models;
using RockClockMobile.Services;
using RockClockMobile.ViewModels;
using RockClockMobile.ViewModels.Navigation;
using System;
using System.Net.Http;
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
        NamesListViewModel userViewModel = new NamesListViewModel();
        public NamesListPage()
        {
            InitializeComponent();
            this.BindingContext = new NamesListViewModel();
            //this.BindingContext = new LoginViewModel();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });

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
            var empSignIn = (Employee)e.ItemData == null ? null : (Employee)e.ItemData;

            var empDtl = new Employee
            {
                EmpID = empSignIn.EmpID,
                FirstName = empSignIn.FirstName,
                LastName = empSignIn.LastName,
                rocksUserId = empSignIn.rocksUserId,
                rocksProjects = empSignIn.rocksProjects
            };

            GlobalServices.employee = empDtl;
            Application.Current.Properties["user_id "] = empDtl.EmpID;
            
            var user = await userViewModel.GetUser();
            string userPassword = user.password;

            App.Current.MainPage = new PincodePage(userPassword);
        }
        private void HeaderTappedEvent(object sender, EventArgs e)
        {
        }
    }
}