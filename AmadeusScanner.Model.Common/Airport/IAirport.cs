using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model.Common
{
    public interface IAirport : IBasePoco
    {
        string Name { get; set; }

        string IataCode { get; set; }
    }
}
