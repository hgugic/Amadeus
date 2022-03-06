using AmadeusScanner.API.ViewModels;
using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmadeusScanner.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<ItineraryViewModel, IItinerary>().ConstructUsing(p => new Itinerary());
            CreateMap<IAirport, AirportViewModel>();
            CreateMap<ICurrency, CurrencyViewModel>();
            CreateMap<IFlight, FlightViewModel>();
        }
    }
}
