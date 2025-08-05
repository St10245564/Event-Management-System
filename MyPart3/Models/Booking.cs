    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace MyPart3.Models
    {
        public class Booking
        {
            public int Id { get; set; }

            [Display(Name = "Event")]
            public int EventId { get; set; }

            [ForeignKey("EventId")]
            public Event Event { get; set; }

            [Display(Name = "Venue")]
            public int VenueId { get; set; }

            [ForeignKey("VenueId")]
            public Venue Venue { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "Booking Date")]
            public DateTime BookingDate { get; set; }
        }
    }

