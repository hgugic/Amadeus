using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Common.Amadeus
{
    public interface IAmadeusService
    {
        Task<Result<IEnumerable<IFlight>>> GetFlights(IItinerary itinerary);
    }
}
