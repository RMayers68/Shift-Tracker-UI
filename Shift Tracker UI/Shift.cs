using Newtonsoft.Json;
using System;

namespace Shift_Tracker_UI
{

    public class Shift
    {
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public decimal? pay { get; set; }
        public decimal? minutes { get; set; }
        public string? location { get; set; }

        public Shift()
        {

        }

        public Shift(DateTime Start, DateTime End, decimal Pay, decimal Minutes, string Location)
        {
            this.start = Start;
            this.end = End;
            this.pay = Pay;
            this.minutes = Minutes;
            this.location = Location;
        }

        public Shift(Shift shift)
        {
            this.start = shift.start;
            this.end = shift.end;
            this.pay = shift.pay;
            this.minutes = shift.minutes;
            this.location = shift.location;
        }        
    }   
}
