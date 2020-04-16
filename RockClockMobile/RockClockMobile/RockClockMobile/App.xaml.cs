using RockClockMobile.Services;
using RockClockMobile.Views.Navigation;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace RockClockMobile
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

             

        public App()
        {
            InitializeComponent();

            //if (UseMockDataStore)
            //{
            //    DependencyService.Register<MockDataStore>();
            //    DependencyService.Register<MockDataStoreTimeLog>();
            //}
            //else
            //    DependencyService.Register<AzureDataStore>();
            //MainPage = new LoginPage();
            //MainPage = new AppShell(); 
            DependencyService.Register<TimeLogService>();
            DependencyService.Register<EmployeeServices>();
            DependencyService.Register<UserServices>();
            DependencyService.Register<BreakLogService>();
            DependencyService.Register<UserLoginService>();
            //App.Current.MainPage = new NamesListPage();
            App.Current.MainPage = new Views.LoginForm.LoginPage();
        }       

        protected override void OnStart()
        {
            AppCenter.Start("android=734872ab-ddac-44a0-b6d3-d8bf0a581fcb;" +
                  "uwp={Your UWP App secret here};" +
                  "ios=6584353c-a808-4fa2-8feb-ebe455b27325;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {
            
        }

    }
}
