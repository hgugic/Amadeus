using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Common;
using AmadeusScanner.Service.Common.Airport;

namespace AmadeusScanner.Service.Airport
{
    public class AirportService : IAirportService
    {
        private readonly IUnitOfWork unitOfWork;

        public AirportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<IAirport>>> GetAirportsByPartialIataCodeAsync(string IataCode)
        {
            if(string.IsNullOrEmpty(IataCode))
                return Result<IEnumerable<IAirport>>.Failure("Iata Code can't be empty");

            var airportList = await unitOfWork.AirportRepository.FindAsync(x => x.IataCode.StartsWith(IataCode.ToUpper()));
            return Result<IEnumerable<IAirport>>.Success(airportList.OrderBy(x => x.IataCode));
        }

        public async Task<Result<IAirport>> GetAirportAsync(Guid id)
        {
            var airport = await unitOfWork.AirportRepository.GetAsync(id);

            if (airport is null)
                return Result<IAirport>.Failure("Unknown airport");
            else
                return Result<IAirport>.Success(airport);
        }

    }
}
