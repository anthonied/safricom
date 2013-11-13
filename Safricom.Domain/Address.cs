using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Safricom.Domain
{
    public class Address
    {
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string GPS { get; set; }
        public string PoBox { get; set; }
        public string PostalCode { get; set; }
    }
}
