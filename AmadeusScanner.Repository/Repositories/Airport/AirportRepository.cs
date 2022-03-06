using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Common.Airport;
using AmadeusScanner.Repository.Data;
using AmadeusScanner.Repository.Entities;
using AutoMapper;

namespace AmadeusScanner.Repository.Airport
{
    public class AirportRepository : Repository<IAirport, AirportEntity>, IAirportRepository
    {
        public AirportRepository(DataContext context, IMapper mapper) : base (context, mapper)
        {

        }
    }
}
