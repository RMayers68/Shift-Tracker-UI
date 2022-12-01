using Newtonsoft.Json;

namespace Shift_Tracker_UI
{

    public class Shifts
    {
        public int? shiftId { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public decimal? pay { get; set; }
        public decimal? minutes { get; set; }
        public string? location { get; set; }

        public Shifts()
        {

        }
        public Shifts(int ShiftId, DateTime Start, DateTime End, decimal Pay, decimal Minutes, string Location)
        {
            this.shiftId = ShiftId;
            this.start = Start;
            this.end = End;
            this.pay = Pay;
            this.minutes = Minutes;
            this.location = Location;
        }
        public Shifts(Shifts shift)
        {
            this.shiftId = shift.shiftId;
            this.start = shift.start;
            this.end = shift.end;
            this.pay = shift.pay;
            this.minutes = shift.minutes;
            this.location = shift.location;
        }        
    }   
}
