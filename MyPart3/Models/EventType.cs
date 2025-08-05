using System.ComponentModel.DataAnnotations;

namespace MyPart3.Models
{
    public class EventType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; // Name of event type

        // Navigation property
        public ICollection<Event> Events { get; set; } = new List<Event>();


        // ✅ Add this to support the Venue <-> EventType relationship
        public ICollection<Venue> Venues { get; set; } = new List<Venue>();
    }
}

