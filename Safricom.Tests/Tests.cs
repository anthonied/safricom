using NUnit.Framework;
using Safricom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Safricom.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Client_GivenANewClient_ShouldCreateUsername()
        {
            var client = new Client
            {
                Name = "Phillip",
                Surname = "Kemp",
                IdPassport = "8505085082089"
            };

            Assert.That(client.CustomerCode, Is.EqualTo("PHIKEM850"));		

            
        }
    }
}
