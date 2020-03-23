using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.ViewModels
{
    public class TimeLogDetailViewModel : BaseViewModel
    {
        public TimeLog TimeLog { get; set; }

        public TimeLogDetailViewModel(TimeLog timelog = null)
        {
            TimeLog = timelog;
        }
    }
}
