using Newtonsoft.Json;
using RockClockMobile.Models;
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
        LoginViewModel loginViewModel = new LoginViewModel();
        List<User> UserList = new List<User>();        
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

            BindingContext = loginViewModel.Users();  

            foreach (var dtl in loginViewModel.Users().OrderBy(a => a.FirstName))
            {
                dtl.FullName = $"{dtl.FirstName} {dtl.LastName}";
                UserList.Add(new User
                {
                    FullName = dtl.FullName
                });           
            }

            userCombo.DataSource = UserList.Select(a => a.FullName);
            lvUsers.ItemsSource = loginViewModel.Users().OrderBy(a => a.FullName);
        }

        public async void btnLogin(object sender, EventArgs e)
        {      

            var httpClient = new HttpClient();
            //var response = await httpClient.GetStringAsync("https://localhost:44387/user");
            //var employee = JsonConvert.DeserializeObject<List<User>>(response);

            //httpClient.BaseAddress = new Uri("https://127.0.0.1:44387/user");
            //var b = httpClient.BaseAddress;
            //var r = await httpClient.GetStringAsync(httpClient.BaseAddress);
            //var c = JsonConvert.DeserializeObject<List<User>>(r);


            Application.Current.MainPage = new AppShell();

        }

        public void SelectUserEvent(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var user = e.Value;

            if (user.ToString() == "")
            {
                lvUsers.ItemsSource = loginViewModel.Users().OrderBy(a => a.FullName);
            }
            else {
                lvUsers.ItemsSource = loginViewModel.Users().Where(a => a.FullName == user.ToString());
            }           
        }

        private async void TappedUser(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //call pincode page
            await Navigation.PushModalAsync(new NavigationPage(new PincodePage()));
        }
    }
}