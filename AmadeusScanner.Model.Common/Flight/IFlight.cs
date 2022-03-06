using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model.Common
{
    
    public interface IFlight : IPrice, IBasePoco
    {
        IAirport OriginAirport { get; set; }

        IAirport DestinationAirport { get; set; }

        DateTime DepartureDate { get; set; }

        DateTime ReturnDate { get; set; }

        int NumberOfPassengers { get; set; }

        int NumberOfTransversDeparture { get; set; }

        int NumberOfTransversReturn { get; set; }

        decimal PricePerPerson { get; set; }
    }
}
