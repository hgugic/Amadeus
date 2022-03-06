using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Common.Flight
{
    public interface IFlightSearchService
    {
        Task<Result<IEnumerable<IFlight>>> FindFlightsAsync(IItinerary itinerary);
    }
}
