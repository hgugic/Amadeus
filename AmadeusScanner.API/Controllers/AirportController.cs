using AmadeusScanner.API.ViewModels;
using AmadeusScanner.Service.Common.Airport;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmadeusScanner.API.Controllers
{

    public class AirportController : BaseController
    {
        private readonly IAirportService airportService;
        private readonly IMapper mapper;

        public AirportController(IAirportService airportService, IMapper mapper) 
        {
            this.airportService = airportService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> FindAirportByPartialIataCodeAsync(string iataCode)
        {   
            var result = await airportService.GetAirportsByPartialIataCodeAsync(iataCode);

            if (result.IsSuccess)
                return Ok(mapper.Map<IEnumerable<AirportViewModel>>(result.Value.ToList()));
            else
                return BadRequest(result.Error);
        }
    }
}
