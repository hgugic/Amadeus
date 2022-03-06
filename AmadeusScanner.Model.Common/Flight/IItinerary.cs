using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model.Common
{
    public interface IItinerary
    {
        Guid OriginAirportId { get; set; }

        Guid DestinationAirportId { get; set; }

        DateTime DepartureDate { get; set; }

        DateTime ReturnDate { get; set; }

        int NumberOfPassengers { get; set; }

        Guid CurrencyId { get; set; }
    }
}
