using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
//using RockClockMobile;
//using RockClockMobile.Views;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RocksClock.UI.Test
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            app.Screenshot("Welcome screen.");

            //PincodePage pincodePage = new PincodePage();
            //LoginPage loginPage = new LoginPage();
            
            //app.Tap(c => c.a);

            ////var ff = app.Tap(loginPage.btnLogin);
            //Assert.Equals(results.Any());
        }
    }
}
