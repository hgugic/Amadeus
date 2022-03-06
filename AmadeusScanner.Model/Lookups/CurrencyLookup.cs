using AmadeusScanner.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model
{
    public class CurrencyLookup : BasePoco, ICurrency
    {
        public string Name { get; set; }

        public string Abrv { get; set; }
    }
}
