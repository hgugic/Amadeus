using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Repository.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
