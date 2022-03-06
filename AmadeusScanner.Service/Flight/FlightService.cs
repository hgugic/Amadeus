using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Common;
using AmadeusScanner.Service.Common.Currency;
using AmadeusScanner.Service.Common.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Flight
{
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrencyService currencyService;

        public FlightService(IUnitOfWork unitOfWork, ICurrencyService currencyService)
        {
            this.unitOfWork = unitOfWork;
            this.currencyService = currencyService;
        }

        public async Task<IEnumerable<IFlight>> GetFlights(IItinerary itinerary)
        {
            var flights =  await unitOfWork.FlightRepository.GetFlightsByItineraryAsync(itinerary);
            flights.ToList().ForEach(i => i.Price = Math.Round(i.PricePerPerson * itinerary.NumberOfPassengers,2));
            await currencyService.CurrencyExchangeAsync(flights, await unitOfWork.CurrencyRepository.GetAsync(itinerary.CurrencyId));
            return flights;

        }

        public async Task<IEnumerable<IFlight>> AddFlightsAsync(IEnumerable<IFlight> flights)
        {
            Initialize(flights);
            await unitOfWork.FlightRepository.AddRangeAsync(flights);
            await unitOfWork.CommitAsync();

            return flights;
        }

        private void Initialize(IEnumerable<IFlight> flights)
        {
            foreach (var flight in flights)
            {
                flight.Id = Guid.NewGuid();
                flight.DateCreated = DateTime.UtcNow;
                flight.DateUpdated = DateTime.UtcNow;
            }
        }
    }
}
