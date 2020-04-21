using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobileUITest.UITest.Tests.Pages
{
    public class NamesListPage : BasePage
    {
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("home"),
            iOS = x => x.Marked("home")
        };

        public NamesListPage()
        {
            App.WaitForNoElement(x => x.Marked("activityindicator"));
        }
    }
}
