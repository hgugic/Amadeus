using AmadeusScanner.Repository.Entities;
using System;
using System.Collections.Generic;

namespace AmadeusScanner.Repository.Data
{
    public class AirportTypeLookup
    {
       public static readonly List<AirportTypeEntity> AirportTypes = new List<AirportTypeEntity>()
       {
           new AirportTypeEntity() { Id = Guid.NewGuid(), Name = "Small airport", Abrv = "Small", DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow  },
           new AirportTypeEntity() { Id = Guid.NewGuid(), Name = "Medium airport", Abrv = "Medium", DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow  },
           new AirportTypeEntity() { Id = Guid.NewGuid(), Name = "Large airport", Abrv = "Large", DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow  },
           new AirportTypeEntity() { Id = Guid.NewGuid(), Name = "Other", Abrv = "Other", DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow  }
       };
    }
}
