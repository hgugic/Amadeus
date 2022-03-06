using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model
{
    public class ItineraryModel
    {
        public Guid OriginAirportId { get; set; }

        public Guid DestinationAirportId { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime? ReturnDate { get; set; }

    }
}
