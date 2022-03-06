using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmadeusScanner.Repository.Entities
{
    [Table("Flight")]
    public class FlightEntity : BaseEntity
    {

        public Guid OriginAirportId { get; set; }

        [ForeignKey("OriginAirportId")]
        public AirportEntity OriginAirport { get; set; }

        public Guid DestinationAirportId { get; set; }

        [ForeignKey("DestinationAirportId")]
        public AirportEntity DestinationAirport { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int NumberOfTransversDeparture { get; set; }

        public int NumberOfTransversReturn { get; set; }

        public decimal PricePerPerson { get; set; }

        public Guid CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public CurrencyEntity Currency { get; set; }




    }
}
