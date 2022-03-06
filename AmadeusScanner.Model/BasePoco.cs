using AmadeusScanner.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Model
{
    public class BasePoco : IBasePoco
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
