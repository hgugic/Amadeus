using AmadeusScanner.Common.Extensions;
using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using AmadeusScanner.Service.Common.Airport;
using AmadeusScanner.Service.Common.Amadeus;
using AmadeusScanner.Service.Common.Currency;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmadeusScanner.Service.Amadeus
{
    public class AmadeusService : IAmadeusService
    {
        private readonly IMediator mediator;
        private readonly IAirportService airportService;
        private readonly ICurrencyService currencyService;
        private readonly IMapper mapper;

        public AmadeusService(IMediator mediator, IAirportService airportService, ICurrencyService currencyService, IMapper mapper)
        {
            this.mediator = mediator;
            this.airportService = airportService;
            this.currencyService = currencyService;
            this.mapper = mapper;
        }
        public async Task<Result<IEnumerable<IFlight>>> GetFlights(IItinerary itinerary)
        {
            var flightBase = await GetFlightDataAndValidateItineraryAsync(itinerary);

            if (flightBase.IsSuccess)
            {
                var amadeusItinerary = mapper.Map<AmadeusItinerary>(flightBase.Value); //await GetAmadeusItineraryAsync(flightBase);

                try
                {
                    var flights = await mediator.Send(new AmadeusList.Query { Itinerary = amadeusItinerary });

                    var flightList = new List<IFlight>();

                    foreach (var flight in flights.Data)
                    {
                        var model = flightBase.Value.DeepCopy();
                        model.Price = flight.Price.Total;
                        model.PricePerPerson = flight.Price.Total / flightBase.Value.NumberOfPassengers;
                        model.NumberOfTransversDeparture = flight.Itineraries.ElementAt(0).Segments.Count() - 1;
                        model.NumberOfTransversReturn = flight.Itineraries.ElementAt(1).Segments.Count() - 1;
                        flightList.Add(model);
                    }

                    return Result<IEnumerable<IFlight>>.Success(flightList);
                }
                catch (Exception)
                {
                    return Result<IEnumerable<IFlight>>.Failure("Amadeus Service Failure");
                }
            }
            else
                return Result<IEnumerable<IFlight>>.Failure(flightBase.Error);
        }

        private async Task<Result<Model.Flight>> GetFlightDataAndValidateItineraryAsync(IItinerary itinerary)
        {
            var errorList = new List<object>();
            var model = new Model.Flight();

            var originAirport = await airportService.GetAirportAsync(itinerary.OriginAirportId);
            var destinationAirport = await airportService.GetAirportAsync(itinerary.DestinationAirportId);
            var currency = await currencyService.GetCurrencyAsync(itinerary.CurrencyId);

            model.DepartureDate = itinerary.DepartureDate;
            model.ReturnDate = itinerary.ReturnDate;
            model.NumberOfPassengers = itinerary.NumberOfPassengers;

            if (originAirport.IsSuccess)
                model.OriginAirport = originAirport.Value;
            else
                errorList.Add("Unknown Origin Airport");

            if (destinationAirport.IsSuccess)
                model.DestinationAirport = destinationAirport.Value;
            else
                errorList.Add("Unknown Destination Airport");

            if (currency.IsSuccess)
                model.Currency = currency.Value;
            else
                errorList.Add(currency.Error);

            if (model.DepartureDate >= model.ReturnDate)
                errorList.Add("Departure Date is after Return date");

            if (model.DepartureDate <= DateTime.Now.Date)
                errorList.Add("Departure date can not be in the past");

            if (model.ReturnDate <= DateTime.Now.Date)
                errorList.Add("Return date can not be in the past");

            if (errorList.Any())
                return Result<Model.Flight>.Failure(errorList);
            else
                return Result<Model.Flight>.Success(model);
        }

    }
}
