using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace RockClockMobileUITest.UITest.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public abstract class BaseTestFixture
    {
        protected IApp app => SetupHooks.App;
        protected bool OnAndroid => SetupHooks.Platform == Platform.Android;
        protected bool OniOS => SetupHooks.Platform == Platform.iOS;

        protected BaseTestFixture(Platform platform)
        {
            SetupHooks.Platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            SetupHooks.App = AppInitializer.StartApp(SetupHooks.Platform, true);
        }
    }
}
