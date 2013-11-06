using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Safricom.Domain
{
    public class Client
    {

        public string CustomerCode
        {
            get
            {
                return Name.Substring(0, 3).ToUpper() + Surname.Substring(0, 3).ToUpper() + IdPassport.Substring(0, 3);
            }
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BusinessName { get; set; }
        public string CellPhoneOne { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string IdPassport { get; set; }
        public string VatNumber { get; set; }
        public Address InstallationAddress { get; set; }
        public Address PhysicalAddress { get; set; }
        public Address PostalAddress { get; set; }
    }
}
