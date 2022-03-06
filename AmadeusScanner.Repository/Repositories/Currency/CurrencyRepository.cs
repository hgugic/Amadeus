using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Common.Currency;
using AmadeusScanner.Repository.Data;
using AmadeusScanner.Repository.Entities;
using AutoMapper;

namespace AmadeusScanner.Repository.Repositories.Currency
{
    public class CurrencyRepository : Repository<ICurrency, CurrencyEntity>, ICurrencyRepository
    {
        public CurrencyRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
