using System;
using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Service.interfaces;
using log4net;

namespace Geldverleih.UI.presenters
{
    public class BankPresenter
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(BankPresenter));


        private readonly IBankService _bankService;

        public BankPresenter(IBankService bankService)
        {
            _bankService = bankService;
        }

        public void GeldEinzahlen(Guid vorgangsNummer, decimal betrag)
        {
            _bankService.GeldEinzahlen(vorgangsNummer, betrag);
        }

        public void GeldAusleihen(Kunde kunde, VerleihKondition kondition, decimal betrag)
        {
            _bankService.GeldAusleihen(kunde.Kundennummer, kondition, betrag);
        }

        public IList<AusleihVorgang> GetAlleAusleihvorgaenge()
        {
            return _bankService.GetAlleAusleihvorgaenge();
        }

        public void TestLogEintrag()
        {
            log4net.Config.XmlConfigurator.Configure();

            log.Warn("Test");
        }
    }
}