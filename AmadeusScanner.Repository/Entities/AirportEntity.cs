using AmadeusScanner.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmadeusScanner.Repository.Entities
{
    [Table("Airport")]
    public class AirportEntity : BaseEntity
    {
        public string Name { get; set; }

        public string IataCode { get; set; }

        public Guid AirportTypeId { get; set; }

        public AirportTypeEntity AirportType { get; set; }
    }
}
