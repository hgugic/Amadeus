using AmadeusScanner.Model.Amadeus;
using AmadeusScanner.Service.Amadeus;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmadeusScanner.Model;
using AmadeusScanner.API.ViewModels;
using AmadeusScanner.Service.Common.Flight;
using AutoMapper;
using AmadeusScanner.Model.Common;
using System.Threading;

namespace AmadeusScanner.API.Controllers
{

    public class FlightController : BaseController
    {
        private readonly IFlightSearchService flightSearchService;
        private readonly IMapper mapper;

        public FlightController(IFlightSearchService flightSearchService, IMapper mapper)
        {
            this.flightSearchService = flightSearchService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> FindFlightsAsync(ItineraryViewModel itinerary)
        { 
            var flights = mapper.Map<IItinerary>(itinerary);
            var result = await flightSearchService.FindFlightsAsync(flights);

            if (result.IsSuccess)
                return Ok(mapper.Map<IEnumerable<FlightViewModel>>(result.Value));
            else
                return BadRequest(result.Error); 
        }
    }
}
