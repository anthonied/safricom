﻿using NUnit.Framework;
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

            Assert.That(client.CustomerCode, Is.EqualTo("PHKE85"));		
        }

        [Test]
        public void Client_GivenEmptyVatNumber_ShouldReturnZero()
        {
            var client = new Client
            {
                VatNumber = ""
            };

            Assert.That(client.VatNumber, Is.EqualTo("0"));			
        }

        [Test]
        public void Client_GivenNoVatNumber_ShouldReturnZero()
        {
            var client = new Client();

            Assert.That(client.VatNumber, Is.EqualTo("0"));
        }

        [Test]
        public void Client_GivenAVatNumber_ShouldReturnThatNumber()
        {
            var client = new Client
            {
                VatNumber = "123"
            };

            Assert.That(client.VatNumber, Is.EqualTo("123"));
        }
    }
}
