using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class ButtonAccess
    {
        public bool CanTimeIn { get; set; }
        public bool CanBreakIn { get; set; }
        public bool CanBreakOut { get; set; }
        public bool CanTimeOut { get; set; }
    }
}
