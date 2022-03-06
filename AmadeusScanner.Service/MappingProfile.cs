using AmadeusScanner.Model;
using AmadeusScanner.Model.Common;
using AutoMapper;

namespace AmadeusScanner.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IFlight, AmadeusItinerary>()
                .ForMember(dest => dest.DestinationAirportIataCode, opt => opt.MapFrom(s => s.DestinationAirport.IataCode))
                .ForMember(dest => dest.OriginAirportIataCode, opt => opt.MapFrom(s => s.OriginAirport.IataCode))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(s => s.Currency.Abrv))
                .ForMember(dest => dest.NumberOfAdultPassengers, opt => opt.MapFrom(s => s.NumberOfPassengers));
        }
    }

}
