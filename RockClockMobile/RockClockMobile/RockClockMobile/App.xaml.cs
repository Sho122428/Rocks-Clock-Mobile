using RockClockMobile.Services;
using RockClockMobile.Views.Navigation;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

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
            App.Current.MainPage = new NamesListPage();
            //App.Current.MainPage = new RockClockMobile.Views.LoginForm.LoginPage();
        }       

        protected override void OnStart()
        {
            // On start runs when your application launches from a closed state, 
            //NetworkSecurityPolicy.Instance.IsCleartextTrafficPermitted.Equals(true);
            if (!stopWatch.IsRunning)
            {
                stopWatch.Start();
            }
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                // Logic for logging out if the device is inactive for a period of time.

                if (stopWatch.IsRunning && stopWatch.Elapsed.Seconds >= defaultTimespan)
                {
                    //prepare to perform your data pull here as we have hit the 1 minute mark   

                    // Perform your long running operations here.
                    
                    Device.BeginInvokeOnMainThread(() => {
                        // If you need to do anything with your UI, you need to wrap it in this.


                    });

                    stopWatch.Restart();
                }

                // Always return true as to keep our device timer running.
                return true;
            });
        }

        protected override void OnSleep()
        {
            // Ensure our stopwatch is reset so the elapsed time is 0.
            stopWatch.Reset();
        }

        protected override void OnResume()
        {
            // App enters the foreground so start our stopwatch again.
            stopWatch.Start();
        }
    }
}
