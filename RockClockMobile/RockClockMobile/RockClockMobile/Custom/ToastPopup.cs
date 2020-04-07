using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RockClockMobile.Custom
{
    public class ToastPopup
    {
        public static void ToastMessage(string popUpmsg, bool isLongMsg)
        {
            if (isLongMsg)
                DependencyService.Get<IMessage>().LongAlert(popUpmsg);
            else
                DependencyService.Get<IMessage>().ShortAlert(popUpmsg);
        }

        public static void ToastDispose(string popUpmsg)
        {
            DependencyService.Get<IMessage>().Destroy(popUpmsg);
        }
    }
}
