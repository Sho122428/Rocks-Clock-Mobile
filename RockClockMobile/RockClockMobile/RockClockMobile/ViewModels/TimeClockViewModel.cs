using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RockClockMobile.ViewModels
{
    public class TimeClockViewModel
    {
        public ObservableCollection<TimeClock> Time_Clock { get; set; }
    }
}
