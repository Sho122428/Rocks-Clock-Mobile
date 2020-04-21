using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RockClockMobileUITest.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform, bool clearData)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                    .InstalledApp("com.companyname.jannotestapp1")
                    .EnableLocalScreenshots()
                    .StartApp(
                        clearData ? Xamarin.UITest.Configuration.AppDataMode.Clear :
                                    Xamarin.UITest.Configuration.AppDataMode.DoNotClear);
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}