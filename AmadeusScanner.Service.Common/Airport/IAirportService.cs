using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Common.Airport
{
    public interface IAirportService
    {
        Task<Result<IEnumerable<IAirport>>> GetAirportsByPartialIataCodeAsync(string IataCode);

        Task<Result<IAirport>> GetAirportAsync(Guid Id);
    }
}
