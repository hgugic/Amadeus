using AmadeusScanner.Common.Json;
using AmadeusScanner.Model.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model
{
    public class Flight : BasePoco, IFlight 
    {
        [JsonConverter(typeof(InterfaceConverter<Airport>))]
        public IAirport OriginAirport { get; set; }

        [JsonConverter(typeof(InterfaceConverter<Airport>))]
        public IAirport DestinationAirport { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public int NumberOfPassengers { get; set; }

        public int NumberOfTransversDeparture { get; set; }

        public int NumberOfTransversReturn { get; set; }

        [JsonConverter(typeof(InterfaceConverter<CurrencyLookup>))]
        public ICurrency Currency { get; set; }

        public decimal Price { get; set; }

        public decimal PricePerPerson { get; set; }

    }
}
