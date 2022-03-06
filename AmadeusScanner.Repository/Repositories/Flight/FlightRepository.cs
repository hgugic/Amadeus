using AmadeusScanner.Common.Hash;
using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Common.Flight;
using AmadeusScanner.Repository.Data;
using AmadeusScanner.Repository.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmadeusScanner.Repository.Flight
{
    public class FlightRepository : Repository<IFlight, FlightEntity>, IFlightRepository
    {
        protected readonly  DataContext context;

        public FlightRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
        }

        public async Task<bool> FlightSearchExistsAsync(IItinerary itinerary)
        {
            var mapped = mapper.Map<ItineraryModel>(itinerary);

            var hash = Hash.GenerateHash(mapped);
            var search = context.FlightSearch.Where(x => x.FlightHash == hash).ToList();

            if (search.Any())
                return await Task.FromResult(true);

            return await Task.FromResult(false);
        }

        public async Task<IEnumerable<IFlight>> GetFlightsByItineraryAsync(IItinerary itinerary)
        {
            var mapped = mapper.Map<ItineraryModel>(itinerary);

            var hash = Hash.GenerateHash(mapped);

            var flight = await context.FlightSearch
                .Include(s => s.Flight)
                    .ThenInclude(f => f.OriginAirport)
                .Include(s => s.Flight)
                    .ThenInclude(f => f.DestinationAirport)
                .Include(s => s.Flight)
                    .ThenInclude(f => f.Currency)
                .Where(flight => flight.FlightHash == hash)
                .Select(x => x.Flight).ToListAsync();


            return await Task.FromResult(mapper.Map<List<IFlight>>(flight));
        }

        public async Task AddFlightSearchAsync(IEnumerable<IFlight> flights, IItinerary itinerary)
        {
            var model = mapper.Map<ItineraryModel>(itinerary);

            var hash = Hash.GenerateHash(model);
            var mapped = mapper.Map<IEnumerable<FlightSearchEntity>>(flights.ToList());
            Initialize(mapped, hash);
            await context.FlightSearch.AddRangeAsync(mapped);

        }

        private void Initialize(IEnumerable<FlightSearchEntity> flights, string hash)
        {
            if (flights.Any())
            {
                foreach (var flight in flights)
                {
                    flight.Id = Guid.NewGuid();
                    flight.FlightHash = hash;
                    flight.DateCreated = DateTime.Now;
                    flight.DateUpdated = DateTime.Now;
                }
            }
            else
            {
                flights.Concat(
                    new List<FlightSearchEntity>() {
                        new FlightSearchEntity()
                        {
                            Id = Guid.NewGuid(),
                            FlightHash = hash,
                            DateCreated = DateTime.Now,
                            DateUpdated = DateTime.Now,
                        }
                    });
            }
        }
    }
}
