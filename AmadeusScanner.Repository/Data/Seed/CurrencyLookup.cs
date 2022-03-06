using AmadeusScanner.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Repository.Data
{
    public class CurrencyLookup
    {
       public static readonly List<CurrencyEntity> Currencies = new List<CurrencyEntity>()
       {
           new CurrencyEntity() { Id = Guid.NewGuid(), Name = "Euro", Abrv = "EUR", DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow  },
           new CurrencyEntity() { Id = Guid.NewGuid(), Name = "Kuna", Abrv = "HRK", DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow  },
           new CurrencyEntity() { Id = Guid.NewGuid(), Name = "Dollar", Abrv = "USD", DateCreated = DateTime.UtcNow, DateUpdated = DateTime.UtcNow  },
       };
    }
}
