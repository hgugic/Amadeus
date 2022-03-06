using AmadeusScanner.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Common.Flight
{
    public interface IFlightService
    {
        Task<IEnumerable<IFlight>> GetFlights(IItinerary itinerary);

        Task<IEnumerable<IFlight>> AddFlightsAsync(IEnumerable<IFlight> flights);
    }
}
