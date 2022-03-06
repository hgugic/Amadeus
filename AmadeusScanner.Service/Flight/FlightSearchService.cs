using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Common;
using AmadeusScanner.Service.Common.Amadeus;
using AmadeusScanner.Service.Common.Flight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Flight
{
    public class FlightSearchService : IFlightSearchService
    {
        private readonly IFlightService flightService;
        private readonly IAmadeusService amadeusService;
        private readonly IUnitOfWork unitOfWork;

        public FlightSearchService(IFlightService flightService, IAmadeusService amadeusService, IUnitOfWork unitOfWork )
        {
            this.flightService = flightService;
            this.amadeusService = amadeusService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<IFlight>>> FindFlightsAsync(IItinerary itinerary)
        {

            if(await unitOfWork.FlightRepository.FlightSearchExistsAsync(itinerary))
                return Result<IEnumerable<IFlight>>.Success(await flightService.GetFlights(itinerary));

            var amadeusflights = await amadeusService.GetFlights(itinerary);

            if(amadeusflights.IsSuccess)
                await AddFlightSearchAsync(amadeusflights.Value, itinerary);

            return amadeusflights;
        }

        private async Task AddFlightSearchAsync(IEnumerable<IFlight> flights, IItinerary itinerary)
        {
            var flightList = await flightService.AddFlightsAsync(flights);
            await unitOfWork.FlightRepository.AddFlightSearchAsync(flightList, itinerary);
            await unitOfWork.CommitAsync();
        }

        
    }
}
