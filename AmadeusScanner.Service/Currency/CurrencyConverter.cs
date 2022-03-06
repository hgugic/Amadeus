using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Currency
{
    public class CurrencyConverter
    {
        public class Query : IRequest<decimal>
        {
            public string ExchangeFrom { get; set; }

            public string ExchangeTo { get; set; }
        }

        public class Handler : IRequestHandler<Query, decimal>
        {

            public async Task<decimal> Handle(Query request, CancellationToken cancellationToken)
            {
                // Mock exchange
                return request.ExchangeFrom switch
                {
                    "HRK" => request.ExchangeTo switch
                    {
                        "EUR" => await Task.FromResult(1m / 7.53m),
                        "USD" => await Task.FromResult(1m / 6.77m),
                        _ => 0m,
                    },
                    "EUR" => request.ExchangeTo switch
                    {
                        "HRK" => await Task.FromResult(7.53m),
                        "USD" => await Task.FromResult(1m / 0.9m),
                        _ => 0m,
                    },
                    "USD" => request.ExchangeTo switch
                    {
                        "HRK" => await Task.FromResult(6.77m),
                        "EUR" => await Task.FromResult(0.9m),
                        _ => 0m,
                    },
                    _ => 0m,
                };
            }
        }
    }
}
