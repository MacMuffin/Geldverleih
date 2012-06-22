using System;
using System.Collections.Generic;
using System.Linq;
using Geldverleih.Domain;
using Geldverleih.Service.interfaces;

namespace Geldverleih.UI.Logik
{
    public class TagesZinsRechner : IZinsRechner
    {
        private readonly IBankService _bankService;

        public TagesZinsRechner(IBankService bankService)
        {
            _bankService = bankService;
        }

        public decimal BetragMitZinsenFuerZeitraumBerechnen(decimal betrag, decimal zinsSatz, Guid vorgangsNummer, ZeitSpanne zeitSpanne)
        {
            DateTime startDatum = zeitSpanne.StartDatum;
            TimeSpan timeSpan = zeitSpanne.EndDatum.Subtract(startDatum);
            int days = timeSpan.Days;
            decimal zuZahlenderBetrag = 0m;

            IList<RueckzahlVorgang> rueckzahlVorgaenge = _bankService.GetAlleRueckzahlvorgaengeByVorgangsNummer(vorgangsNummer);

            for (int ausleihTag = 0; ausleihTag <= days; ausleihTag++)
            {
                var ruecklaungenFuerDatum = rueckzahlVorgaenge.Where(rueckzahlVorgang => DatumIstGleich(rueckzahlVorgang, startDatum));
                betrag = ruecklaungenFuerDatum.Aggregate(betrag, (current, rueckzahlVorgang) => current - rueckzahlVorgang.Betrag);

                decimal tagesZinsen = DreisatzAnwenden(betrag, zinsSatz);

                zuZahlenderBetrag = zuZahlenderBetrag + tagesZinsen;
            }

            zuZahlenderBetrag += betrag;

            return zuZahlenderBetrag;
        }

        private bool DatumIstGleich(RueckzahlVorgang rueckzahlVorgang, DateTime startDatum)
        {
            return DateTime.Compare(rueckzahlVorgang.Datum.Date, startDatum.Date) == 0;
        }

        private decimal DreisatzAnwenden(decimal betrag, decimal zinsSatz)
        {
            return betrag*zinsSatz/100;
        }

        public decimal GetEingenommeneZinsenImZeitraum(decimal betrag, decimal zinsSatz, Guid vorgangsNummer,
                                                       ZeitSpanne zeitSpanne)
        {
            throw new NotImplementedException();
        }
    }
}