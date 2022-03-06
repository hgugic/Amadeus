using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using AmadeusScanner.Repository.Entities;
using AutoMapper;

namespace AmadeusScanner.Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<IAirport, AirportEntity>();
           CreateMap<AirportEntity, IAirport>().ConstructUsing(p => new Model.Airport());
           CreateMap<CurrencyEntity, ICurrency>().ConstructUsing(p => new Model.CurrencyLookup());
           CreateMap<ICurrency, CurrencyEntity>();
            CreateMap<IFlight, FlightEntity>()
                   .ForMember(dest => dest.CurrencyId, opt => opt.MapFrom(s => s.Currency.Id))
                   .ForMember(dest => dest.OriginAirportId, opt => opt.MapFrom(s => s.OriginAirport.Id))
                   .ForMember(dest => dest.DestinationAirportId, opt => opt.MapFrom(s => s.DestinationAirport.Id))
                   .ForMember(dest => dest.DestinationAirport, opt => opt.Ignore())
                   .ForMember(dest => dest.OriginAirport, opt => opt.Ignore())
                   .ForMember(dest => dest.Currency, opt => opt.Ignore());

            CreateMap<FlightEntity, IFlight>().ConstructUsing(p => new Model.Flight()); 

            CreateMap<IItinerary, ItineraryModel>().ReverseMap();

            CreateMap<IFlight, FlightSearchEntity>()
                .ForMember(dest => dest.FlightId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
