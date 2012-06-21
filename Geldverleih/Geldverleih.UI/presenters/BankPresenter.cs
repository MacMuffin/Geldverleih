using System;
using System.Collections.Generic;
using System.Linq;
using Geldverleih.Domain;
using Geldverleih.Service.interfaces;
using log4net;

namespace Geldverleih.UI.presenters
{
    public class BankPresenter
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(BankPresenter));


        private readonly IBankService _bankService;

        public BankPresenter(IBankService bankService)
        {
            _bankService = bankService;
        }

        public void GeldEinzahlen(Guid vorgangsNummer, decimal betrag)
        {
            AusleihVorgang ausleihVorgang = _bankService.GetAusleihvorgangByVorgangsnummer(vorgangsNummer);

            decimal zuZahlenderBetrag = ausleihVorgang.Betrag;

            IList<RueckzahlVorgang> alleRueckzahlungenZumVorgang = _bankService.GetAlleRueckzahlvorgaengeByVorgangsNummer(vorgangsNummer);

            alleRueckzahlungenZumVorgang.Select(x => zuZahlenderBetrag -= x.Betrag);



            _bankService.GeldEinzahlen(vorgangsNummer, betrag);
        }

        public EinzahlResult EingezahltenBetragVomZuZahlendenBetragAbziehen(decimal zuZahlenderBetrag, decimal eingezahlterBetrag, Guid vorgangsNummer)
        {
            if (zuZahlenderBetrag == 0.0m)
                return EinzahlResult.EinzahlvorgangFehlerhaft(EinzahlError.VorgangBereitsBezahlt);

            decimal restBetrag = zuZahlenderBetrag - eingezahlterBetrag;

            if (restBetrag.ToString().Contains("-"))
            {
                decimal tatsaechlichZuZahlenderBetrag = eingezahlterBetrag + restBetrag;
                _bankService.GeldEinzahlen(vorgangsNummer, tatsaechlichZuZahlenderBetrag);
                return EinzahlResult.EinzahlungenErfolgreich(restBetrag);
            }

            _bankService.GeldEinzahlen(vorgangsNummer, eingezahlterBetrag);

            return EinzahlResult.EinzahlungenErfolgreich(restBetrag);
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

            Log.Warn("Test");
        }
    }
}