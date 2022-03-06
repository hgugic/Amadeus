using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Common.Airport;
using AmadeusScanner.Repository.Common.Currency;
using AmadeusScanner.Repository.Common.Flight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmadeusScanner.Repository.Common
{
    public interface IUnitOfWork
    {
        IAirportRepository AirportRepository { get; }

        IFlightRepository FlightRepository { get; }

        ICurrencyRepository CurrencyRepository { get; }

        Task<int> CommitAsync();
    }
}
