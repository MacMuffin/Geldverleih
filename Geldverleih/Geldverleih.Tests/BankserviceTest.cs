using System;
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
                //Expect.Call(() => ausleihRepository.GeldAnKundenAusleihen(factory.CreateAusleihVorgangObject(kunde.Kundennummer, verleihKondition, betrag)));
            }

            _mockRepository.ReplayAll();

            bankmanager.GeldAusleihen(kunde.Kundennummer, verleihKondition, (decimal)5.5);

            Assert.IsNotNull(bankmanager);
        }

        [TestMethod]
        public void GeldEinzahlungVomKundenKannVorgenommenWerden()
        {
            //Dummyobjekte werden mit RhinoMock erstellt
            IAusUndRueckzahlvorgangFactory factory = _mockRepository.StrictMock<IAusUndRueckzahlvorgangFactory>();
            IKundenRepository kundenRepository = _mockRepository.StrictMock<IKundenRepository>();
            IRueckzahlReppository rueckzahlReppository = _mockRepository.StrictMock<IRueckzahlReppository>();
            IAusleihRepository ausleihRepository = _mockRepository.StrictMock<IAusleihRepository>();

            //Das wird gebraucht, um die konkrete implementation vom Bankservice zu testen
            IBankService bankService = new BankService(ausleihRepository, rueckzahlReppository, kundenRepository,
                                                       factory);

            
            Guid vorgangsNummer = Guid.NewGuid();

            //Wenn in Factory die Methode CreateRueckzahlVorgangObject aufgerufen wird, so kommt dieses Objekt als Rückgabe zurück.
            RueckzahlVorgang rueckzahlVorgang = new RueckzahlVorgang { Datum = DateTime.Now, AusleihvorgangNummer = vorgangsNummer, Betrag = 5.5m };


            //Aufnahme wird gestartet. Wir schreiben hier alles, was wir beim Aufruf der Methode GeldEinzahlen vom Bankservice erwarten. Was wird aufgerufen, was soll zurückgegeben werden, etc.
            using (_mockRepository.Record())
            {
                Expect.Call(factory.CreateRueckzahlVorgangObject(vorgangsNummer, 5.5m))
                    .Return(rueckzahlVorgang);
                Expect.Call(() => rueckzahlReppository.KundeZahltGeldEin(rueckzahlVorgang));
            }

            //Es soll alles wiedergegeben werden.
            _mockRepository.ReplayAll();

            //Wiedergabe wurde gestartet
            bankService.GeldEinzahlen(vorgangsNummer, (decimal) 5.5);

            Assert.IsNotNull(bankService);
        }
    }
}
