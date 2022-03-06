using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmadeusScanner.Repository.Entities
{
    [Table("AirportType")]
    public class AirportTypeEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Abrv { get; set; }

        public ICollection<AirportEntity> Airports { get; set; }
    }
}
