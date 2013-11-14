using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Safricom.Domain
{
    public class Client
    {
        public Client()
        {
            Password = string.Empty;
        }
        public string UserName
        {
            get
            {
                return Name.Substring(0, 2).ToUpper() + Surname.Substring(0, 2).ToUpper() + IdPassport.Substring(0, 2);
            }
        }
        private string _password;
        public string Password 
        {
            get { return _password; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    _password = "DUMMY";
                else
                    _password = value;
            }
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BusinessName { get; set; }
        public string CellPhoneOne { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string IdPassport { get; set; }
        private string _vatNumber = "0";
        public string VatNumber
        {
            get
            {
                return _vatNumber;
            }
            set
            {
                if (value == "")
                    _vatNumber = "0";
                else
                    _vatNumber = value;
            }
        }
        public Address InstallationAddress { get; set; }
        public Address PhysicalAddress { get; set; }
        public Address PostalAddress { get; set; }
    }
}
