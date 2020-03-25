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
        //LoginViewModel loginViewModel = new LoginViewModel();
        List<Employee> EmployeeList = new List<Employee>();
        
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

            BindingContext = new LoginViewModel();  

            //foreach (var dtl in loginViewModel.Employees().OrderBy(a => a.FirstName))
            //{
            //    dtl.FullName = $"{dtl.FirstName} {dtl.LastName}";
            //    EmployeeList.Add(new Employee
            //    {
            //        FullName = dtl.FullName
            //    });           
            //}
            
            //userCombo.DataSource = EmployeeList.Select(a => a.FullName);
            //lvUsers.ItemsSource = loginViewModel.Employees().OrderBy(a => a.FullName);
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
                EmpID = empSignIn.EmpID,
                FirstName = empSignIn.FirstName,
                LastName = empSignIn.LastName,
                ProjectId = empSignIn.ProjectId,
                ProjectName = empSignIn.ProjectName
            };

           GlobalServices.employee = empDtl;
           Application.Current.Properties["user_id "] = empDtl.EmpID;
            

            await Navigation.PushModalAsync(new NavigationPage(new PincodePage()));
        }
    }
}