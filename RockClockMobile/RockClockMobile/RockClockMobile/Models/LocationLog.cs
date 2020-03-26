using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RockClockMobile.Models
{
    public class LocationLog
    {
        public int LocationLogId { get; set; }
        public int TimeLogId { get; set; }
        public string LogType { get; set; }
        public string MacAddress { get; set; }
        public string IPAddress { get; set; }
        public string XCoordinates { get; set; }

        //[StringLength(30)]
        public string YCoordinates { get; set; }
    }
}
