using AmadeusScanner.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model
{
    public class Airport : BasePoco, IAirport
    {
        public string Name { get; set; }
        public string IataCode { get; set; }

    }
}
