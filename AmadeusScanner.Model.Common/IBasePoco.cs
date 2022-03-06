using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model.Common
{
    public interface IBasePoco
    {
        Guid Id { get; set; }

        DateTime DateCreated { get; set; }

        DateTime DateUpdated { get; set; }
    }
}
