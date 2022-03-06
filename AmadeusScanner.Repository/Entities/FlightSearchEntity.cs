using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmadeusScanner.Repository.Entities
{
    [Table("FlightSearch")]
    public class FlightSearchEntity : BaseEntity
    {
        public string FlightHash { get; set; }

        public Guid FlightId { get; set; }

        public FlightEntity Flight { get; set; }
}
}
