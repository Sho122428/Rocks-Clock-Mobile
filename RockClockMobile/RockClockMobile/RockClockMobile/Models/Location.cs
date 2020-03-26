using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public string XCoordinates { get; set; }
        public string YCoordinates { get; set; }
    }
}
