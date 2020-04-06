using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Custom
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);

        void Destroy(string message);
    }
}
