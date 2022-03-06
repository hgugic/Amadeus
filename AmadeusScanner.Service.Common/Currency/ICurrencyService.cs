using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Common.Currency
{
    public interface ICurrencyService
    {
        Task<Result<IEnumerable<ICurrency>>> GetCurrenciesAsync();

        Task<Result<ICurrency>> GetCurrencyAsync(Guid id);

        Task CurrencyExchangeAsync(IEnumerable<IPrice> prices, ICurrency convertTo);
    }
}
