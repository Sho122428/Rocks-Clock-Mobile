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

        private static Stopwatch stopWatch = new Stopwatch();
        private const int defaultTimespan = 20; //In Seconds        

        public App()
        {
            InitializeComponent();

            DependencyService.Register<TimeLogService>();
            DependencyService.Register<RocksUserServices>();
            DependencyService.Register<UserServices>();
            DependencyService.Register<BreakLogService>();
            DependencyService.Register<AccountService>();
            
            
            App.Current.MainPage = new Views.LoginForm.LoginPage();
            
        }       

        protected override void OnStart()
        {
            // On start runs when your application launches from a closed state, 
            //NetworkSecurityPolicy.Instance.IsCleartextTrafficPermitted.Equals(true);
            AppCenter.Start("android=734872ab-ddac-44a0-b6d3-d8bf0a581fcb;" +
                  "uwp={Your UWP App secret here};" +
                  "ios={Your iOS App secret here}",
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
