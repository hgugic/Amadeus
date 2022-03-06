using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model.Common
{
    public interface IPrice
    {
        decimal Price { get; set; }

        ICurrency Currency { get; set; }
    }
}
