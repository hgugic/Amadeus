using AmadeusScanner.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model
{
    public class Itinerary : IItinerary
    {
        public Guid OriginAirportId { get; set; }

        public Guid DestinationAirportId { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public Guid CurrencyId { get; set; }

        public int NumberOfPassengers { get; set; }
    }
}
