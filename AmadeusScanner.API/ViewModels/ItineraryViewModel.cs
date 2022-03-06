using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmadeusScanner.API.ViewModels
{
    public class ItineraryViewModel
    {
        public Guid? OriginAirportId { get; set; }

        public Guid? DestinationAirportId { get; set; }

        public DateTime? DepartureDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public Guid? CurrencyId { get; set; }

        public int? NumberOfPassengers { get; set; }
    }
}
