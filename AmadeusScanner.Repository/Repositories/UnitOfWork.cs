using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Common;
using AmadeusScanner.Repository.Common.Airport;
using AmadeusScanner.Repository.Common.Currency;
using AmadeusScanner.Repository.Common.Flight;
using AmadeusScanner.Repository.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmadeusScanner.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;


        public UnitOfWork(DataContext context, 
                          IAirportRepository airportRepository, 
                          IFlightRepository flightRepository,
                          ICurrencyRepository currencyRepository)
        {
            this.context = context;
            AirportRepository = airportRepository;
            FlightRepository = flightRepository;
            CurrencyRepository = currencyRepository;
        }

        public IAirportRepository AirportRepository { get; }

        public IFlightRepository FlightRepository { get; }

        public ICurrencyRepository CurrencyRepository { get; }

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

    }
}
