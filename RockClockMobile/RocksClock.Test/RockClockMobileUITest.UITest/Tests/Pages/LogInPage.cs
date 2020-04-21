using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace RockClockMobileUITest.UITest.Tests.Pages
{
    public class LogInPage : BasePage
    {
        readonly Query emailField;
        readonly Query passwordField;
        readonly Query signInButton;

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("Username")
            //,
            //iOS = x => x.Marked("Username")
        };

        public LogInPage()
        {
            emailField = x => x.Marked("username");
            passwordField = x => x.Marked("password");
            signInButton = x => x.Marked("signin");
        }

        public LogInPage EnterCredentials(string username, string password)
        {
            App.WaitForElement(emailField);
            App.Tap(emailField);
            App.EnterText(username);
            App.DismissKeyboard();

            App.Tap(passwordField);
            App.EnterText(password);
            App.DismissKeyboard();

            App.Screenshot("Credentials Entered");

            return this;
        }
        public LogInPage SignIn()
        {
            App.Tap(signInButton);

            return this;
        }

        public void CheckThereIsNoNavigation()
        {
            App.WaitForElement(emailField);
            App.WaitForElement(passwordField);
            App.WaitForElement(signInButton);
        }
    }
}
