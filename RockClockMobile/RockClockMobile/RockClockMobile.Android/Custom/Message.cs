using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RockClockMobile.Custom;
using RockClockMobile.Droid.Custom;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace RockClockMobile.Droid.Custom
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        public void Destroy(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Dispose();
        }
    }
}