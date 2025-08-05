using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyPart3.Models;

namespace MyPart3.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; } = null!;

        public int EventTypeId { get; set; }
        public EventType EventType { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}