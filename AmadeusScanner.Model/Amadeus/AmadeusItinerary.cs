using AmadeusScanner.Common.Attributes;
using AmadeusScanner.Model.Common;
using System;

namespace AmadeusScanner.Model
{
    public class AmadeusItinerary : IAmadeusItinerary
    {
        [ApiGet("originLocationCode")]
        public string OriginAirportIataCode { get; set; }

        [ApiGet("destinationLocationCode")]
        public string DestinationAirportIataCode { get; set; }

        [ApiGet("departureDate")]
        public DateTime? DepartureDate { get; set; }

        [ApiGet("returnDate")]
        public DateTime? ReturnDate { get; set; }

        [ApiGet("adults")]
        public int? NumberOfAdultPassengers { get; set; }

        [ApiGet("currencyCode")]
        public string Currency { get; set; }
    }
}
