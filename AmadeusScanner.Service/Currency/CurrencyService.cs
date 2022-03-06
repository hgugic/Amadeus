using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Common;
using AmadeusScanner.Service.Common.Currency;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;

        public CurrencyService(IUnitOfWork unitOfWork, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
        }

        public async Task CurrencyExchangeAsync(IEnumerable<IPrice> prices, ICurrency convertTo)
        {
            Dictionary<Tuple<Guid, Guid>, decimal> exchange = new Dictionary<Tuple<Guid, Guid>, decimal>();

            foreach (var price in prices)
            {
                if (price.Currency.Id == convertTo.Id)
                    continue;

                var exc = Tuple.Create(price.Currency.Id, convertTo.Id);

                if (exchange.ContainsKey(exc))
                {
                    exchange.TryGetValue(exc, out decimal exchangeRate);
                    price.Currency = convertTo;
                    price.Price = Math.Round(exchangeRate * price.Price,2);

                }
                else
                {
                    var exchangeRate = await mediator.Send(new CurrencyConverter.Query { ExchangeFrom = price.Currency.Abrv, ExchangeTo = convertTo.Abrv });
                    exchange.Add(exc, exchangeRate);
                    price.Currency = convertTo;
                    price.Price = Math.Round(exchangeRate * price.Price,2);
                }    
            }
        }

        public async Task<Result<IEnumerable<ICurrency>>> GetCurrenciesAsync()
        {
            return Result<IEnumerable<ICurrency>>.Success(await unitOfWork.CurrencyRepository.GetAllAsync());
        }

        public async Task<Result<ICurrency>> GetCurrencyAsync(Guid id)
        {
            var currency = await unitOfWork.CurrencyRepository.GetAsync(id);

            if (currency == null)
                return Result<ICurrency>.Failure("Unknown Currency");
            else
                return Result<ICurrency>.Success(currency);

        }
    }
}
