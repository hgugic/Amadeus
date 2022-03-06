using System.Collections.Generic;

namespace AmadeusScanner.Model.Amadeus
{
    public class AmadeusFlightOffers
    {
        public IEnumerable<Data> Data { get; set; }

        
    }

    public class Data
    {
        public IEnumerable<Itineraries> Itineraries { get; set; }

        public Price Price { get; set; }
    }

    public class Itineraries
    {
        public IEnumerable<Segment> Segments { get; set; }
    }

    public class Segment
    {

    }

    public class Price
    {
        public string Currency { get; set; }
        public decimal Total { get; set; }
    }
}
