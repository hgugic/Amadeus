using System;
using System.Collections.Generic;
using System.Text;

namespace AmadeusScanner.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ApiGetAttribute : Attribute
    {
        public ApiGetAttribute(string Name, bool Ignore = false)
        {
            this.Name = Name;
            this.Ignore = Ignore;
        }

        public string Name { get; }
        public bool Ignore { get; }
    }
}
