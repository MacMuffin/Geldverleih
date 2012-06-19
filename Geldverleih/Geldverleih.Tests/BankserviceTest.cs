﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Geldverleih.Domain;
using Geldverleih.Repository;
using Geldverleih.Repository.interfaces;
using Geldverleih.Service;
using Geldverleih.Service.interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Geldverleih.Tests
{
    [TestClass]
    public class BankserviceTest
    {

        MockRepository _mockRepository = new MockRepository();

        [TestMethod]
        public void BankserviceKannErstelltWerden()
        {
            IAusleihRepository ausleihRepository = new AusleihRepository();
            IKundenRepository kundenRepository = new KundenRepository();
            IAusUndRueckzahlvorgangFactory factory = new AusUndRueckzahlvorgangFactory();
            IRueckzahlReppository rueckzahlReppository = new RueckzahlRepository();
            IBankService bankmanager = new BankService(ausleihRepository, rueckzahlReppository, kundenRepository, factory);

            Assert.IsNotNull(bankmanager);
        }

        [TestMethod]
        public void AusleihvorgangWirdInsRepositoryGeschrieben()
        {

            IAusleihRepository ausleihRepository = _mockRepository.StrictMock<IAusleihRepository>();
            IBankService bankmanager = _mockRepository.StrictMock<IBankService>();
            IAusUndRueckzahlvorgangFactory factory = _mockRepository.StrictMock<IAusUndRueckzahlvorgangFactory>();

            Kunde kunde = new Kunde();
            VerleihKondition verleihKondition = new VerleihKondition();
            decimal betrag = (decimal) 5.5;

            using (_mockRepository.Record())
            {
                Expect.Call(() => bankmanager.GeldAusleihen(kunde.Kundennummer, verleihKondition, betrag));
                Expect.Call(() => ausleihRepository.GeldAnKundenAusleihen(factory.CreateAusleihVorgangObject(kunde.Kundennummer, verleihKondition, betrag)));
            }

            _mockRepository.ReplayAll();

            bankmanager.GeldAusleihen(kunde.Kundennummer, verleihKondition, (decimal)5.5);

            Assert.IsNotNull(bankmanager);
        }

        [TestMethod]
        public void RueckgabeWirdInsRepositoryGeschrieben()
        {

            IBankService bankmanager = _mockRepository.StrictMock<IBankService>();

            Kunde kunde = new Kunde();
            Guid vorgangsNummer = Guid.NewGuid();


            using (_mockRepository.Record())
            {
                Expect.Call(() => bankmanager.GeldEinzahlen(vorgangsNummer, (decimal) 5.5));
            }

            _mockRepository.ReplayAll();

            bankmanager.GeldEinzahlen(vorgangsNummer, (decimal) 5.5);

            Assert.IsNotNull(bankmanager);
        }
    }
}
