using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model.Common
{
    public interface ICurrency : IBasePoco
    {
        string Name { get; set; }

        string Abrv { get; set; }
    }
}
