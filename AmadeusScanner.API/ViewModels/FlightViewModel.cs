using AmadeusScanner.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmadeusScanner.API.ViewModels
{
    public class FlightViewModel
    {
        public Guid Id { get; set; }

        public IAirport OriginAirport { get; set; }

        public IAirport DestinationAirport { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int NumberOfPassengers { get; set; }

        public int NumberOfTransversDeparture { get; set; }

        public int NumberOfTransversReturn { get; set; }

        public ICurrency Currency { get; set; }

        public decimal Price { get; set; }

    }
}
