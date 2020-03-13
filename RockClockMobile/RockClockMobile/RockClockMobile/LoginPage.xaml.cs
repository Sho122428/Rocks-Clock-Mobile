using Newtonsoft.Json;
using RockClockMobile.Models;
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


        }

        async void btnLogin(object sender, EventArgs e)
        {      

            var httpClient = new HttpClient();
            //var response = await httpClient.GetStringAsync("https://localhost:44387/user");
            //var employee = JsonConvert.DeserializeObject<List<User>>(response);

            httpClient.BaseAddress = new Uri("https://127.0.0.1:44387/user");
            var b = httpClient.BaseAddress;
            var r = await httpClient.GetStringAsync(httpClient.BaseAddress);
            var c = JsonConvert.DeserializeObject<List<User>>(r);


            Application.Current.MainPage = new AppShell();

        }
    }
}