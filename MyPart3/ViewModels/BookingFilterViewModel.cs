using MyPart3.Models;

namespace MyPart3.ViewModels
{
    public class BookingFilterViewModel
    {
        public List<Booking> Bookings { get; set; }
        public int? EventTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsAvailable { get; set; }
        public List<EventType> EventTypes { get; set; }
    }
}
