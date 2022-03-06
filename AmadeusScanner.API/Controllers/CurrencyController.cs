using AmadeusScanner.API.ViewModels;
using AmadeusScanner.Service.Common.Currency;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmadeusScanner.API.Controllers
{
    public class CurrencyController : BaseController
    {
        private readonly ICurrencyService currencyService;
        private readonly IMapper mapper;

        public CurrencyController(ICurrencyService currencyService, IMapper mapper)
        {
            this.currencyService = currencyService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var currencies = (await currencyService.GetCurrenciesAsync()).Value.ToList();         

            if (currencies.Any())
                return Ok(mapper.Map<IEnumerable<CurrencyViewModel>>(currencies));
            else
                return NotFound();
        }
    }
}
