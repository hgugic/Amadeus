using AmadeusScanner.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model.Common
{
    public interface IAmadeusItinerary
    {
        [ApiGet("originLocationCode")]
        string OriginAirportIataCode { get; set; }

        [ApiGet("destinationLocationCode")]
        string DestinationAirportIataCode { get; set; }

        [ApiGet("departureDate")]
        DateTime? DepartureDate { get; set; }

        [ApiGet("returnDate")]
        DateTime? ReturnDate { get; set; }

        [ApiGet("adults")]
        int? NumberOfAdultPassengers { get; set; }

        [ApiGet("currencyCode")]
        string Currency { get; set; }
    }
}
