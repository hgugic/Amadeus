using AmadeusScanner.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmadeusScanner.Repository.Common.Flight
{
    public interface IFlightRepository : IRepository<IFlight>
    {
        Task<bool> FlightSearchExistsAsync(IItinerary itinerary);
        Task<IEnumerable<IFlight>> GetFlightsByItineraryAsync(IItinerary itinerary);

        Task AddFlightSearchAsync(IEnumerable<IFlight> flights, IItinerary itinerary);
    }
}
