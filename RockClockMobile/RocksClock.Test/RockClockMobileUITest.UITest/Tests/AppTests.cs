using NUnit.Framework;
using RockClockMobileUITest.UITest.Tests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace RockClockMobileUITest.UITest.Tests
{
    
    public class AppTests : BaseTestFixture
    {
        public AppTests(Platform platform) : base(platform)
        {
            SetupHooks.Platform = platform;
        }
        
        // Test cases begin here...
        [Test]
        public void SuccessSignInTest()
        {
            new LogInPage()
                .EnterCredentials(TestSettings.TestUsername, TestSettings.TestPassword)
                .SignIn();

            new NamesListPage();
        }

        [Test]
        public void FailedSignInTest()
        {
            new LogInPage()
                .EnterCredentials(string.Empty, string.Empty)
                .SignIn()
                .CheckThereIsNoNavigation();
        }
    }
}
