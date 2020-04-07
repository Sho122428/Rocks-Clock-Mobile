using Newtonsoft.Json;
using RockClockMobile.Models;
using RockClockMobile.Services;
using RockClockMobile.ViewModels;
using RockClockMobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RockClockMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {        
        public LoginPage()
        {
            InitializeComponent();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt")
                );
                return true;
            });

            //BindingContext = new LoginViewModel();  
        }

        public async void btnLogin(object sender, EventArgs e)
        {    
            Application.Current.MainPage = new AppShell();
        }

        public void SelectUserEvent(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var user = e.Value;

            //if (user.ToString() == "")
            //{
            //    lvUsers.ItemsSource = loginViewModel.Employees().OrderBy(a => a.FullName);
            //}
            //else {
            //    lvUsers.ItemsSource = loginViewModel.Employees().Where(a => a.FullName == user.ToString());
            //}           
        }

        private async void TappedUser(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //call pincode page
            

            var empSignIn = (Employee)e.ItemData;

            var empDtl = new Employee{
                id = empSignIn.id,
                firstName = empSignIn.firstName,
                lastName = empSignIn.lastName,
                rocksUserProjectMaps = empSignIn.rocksUserProjectMaps
            };

           GlobalServices.employee = empDtl;
           Application.Current.Properties["user_id "] = empDtl.id;
            

            await Navigation.PushModalAsync(new NavigationPage(new PincodePage(null,0)));
        }
    }
}