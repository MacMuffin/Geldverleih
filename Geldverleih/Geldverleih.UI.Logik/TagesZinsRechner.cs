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
            DateTime startDatum = zeitSpanne.StartDatum.Date;
            int days = GetDays(zeitSpanne.EndDatum, startDatum);
            decimal zuZahlenderBetrag = 0m;

            IList<RueckzahlVorgang> rueckzahlVorgaenge = _bankService.GetAlleRueckzahlvorgaengeByVorgangsNummer(vorgangsNummer);

            if (days == 0)
                betrag = GetBetrag(startDatum, betrag, vorgangsNummer);
            else
            {
                for (int ausleihTag = 1; ausleihTag <= days; ausleihTag++)
                {
                    IEnumerable<RueckzahlVorgang> rueckzahlungenFuerDatum =
                        rueckzahlVorgaenge.Where(
                            rueckzahlVorgang => DatumIstGleich(rueckzahlVorgang, startDatum.AddDays(ausleihTag)));
                    betrag = rueckzahlungenFuerDatum.Aggregate(betrag,
                                                               (current, rueckzahlVorgang) =>
                                                               current - rueckzahlVorgang.Betrag);

                    decimal tagesZinsen = DreisatzAnwenden(betrag, zinsSatz);

                    zuZahlenderBetrag = zuZahlenderBetrag + tagesZinsen;
                }
            }

            zuZahlenderBetrag += betrag;

            if (zuZahlenderBetrag <= 0.0m)
                return 0.0m;


            return zuZahlenderBetrag;
        }

        private decimal GetBetrag(DateTime startDatum, decimal betrag, Guid vorgangsNummer)
        {
            IList<RueckzahlVorgang> rueckzahlVorgaenge = _bankService.GetAlleRueckzahlvorgaengeByVorgangsNummer(vorgangsNummer);
            IEnumerable<RueckzahlVorgang> rueckzahlungenFuerDatum;
            rueckzahlungenFuerDatum = rueckzahlVorgaenge.Where(rueckzahlVorgang => DatumIstGleich(rueckzahlVorgang, startDatum));
            betrag = rueckzahlungenFuerDatum.Aggregate(betrag, (current, rueckzahlVorgang) => current - rueckzahlVorgang.Betrag);
            return betrag;
        }

        private int GetDays(DateTime endDatum, DateTime startDatum)
        {
            TimeSpan timeSpan = endDatum.Date.Subtract(startDatum.Date);
            return timeSpan.Days;
        }

        private bool DatumIstGleich(RueckzahlVorgang rueckzahlVorgang, DateTime startDatum)
        {
            return DateTime.Compare(rueckzahlVorgang.Datum.Date, startDatum.Date) == 0;
        }

        private decimal DreisatzAnwenden(decimal betrag, decimal zinsSatz)
        {
            return betrag*zinsSatz/100;
        }

        public decimal GetEingenommeneZinsenImZeitraum(ZeitSpanne zeitSpanne)
        {
            IList<AusleihVorgang> alleAusleihvorgaenge = _bankService.GetAlleAusleihvorgaenge()
                .Where(ausleihVorgang => ausleihVorgang.Datum.Date <= zeitSpanne.EndDatum.Date).OrderBy(ausleihvorgang => ausleihvorgang.Datum).ToList();

            if (alleAusleihvorgaenge.Count == 0)
                return 0.0m;

            decimal eingenommenZinsen = 0m;
            AusleihVorgang fruehsterAusleihvorgang = alleAusleihvorgaenge.First();
            int days = GetDays(zeitSpanne.EndDatum, fruehsterAusleihvorgang.Datum);

            foreach (AusleihVorgang ausleihVorgang in alleAusleihvorgaenge)
            {
                decimal ausgeliehenerBetrag = ausleihVorgang.Betrag;

                for (int tage = 1; tage <= days; tage++)
                {
                    decimal zuZahlenderBetragFuerTag = GetBetrag(fruehsterAusleihvorgang.Datum.AddDays(tage), ausgeliehenerBetrag, ausleihVorgang.VorgangsNummer);
                    decimal zinsenFuerTag = DreisatzAnwenden(zuZahlenderBetragFuerTag, ausleihVorgang.ZinsSatz);
                    eingenommenZinsen += zinsenFuerTag;
                    ausgeliehenerBetrag = zuZahlenderBetragFuerTag + zinsenFuerTag;
                }
            }

            return eingenommenZinsen;
        }

        public decimal GetAusgezahltesGeldFuerZeitraum(ZeitSpanne zeitSpanne)
        {
            List<AusleihVorgang> ausleihVorgaenge = _bankService.GetAlleAusleihvorgaenge().Where(
                ausleihVorgang => ausleihVorgang.Datum.Date >= zeitSpanne.StartDatum.Date && ausleihVorgang.Datum.Date <= zeitSpanne.EndDatum.Date).ToList();

            return ausleihVorgaenge.Sum(ausleihVorgang => ausleihVorgang.Betrag);
        }

        public decimal GetRueckgezahlteBetraegeFuerZeitraum(ZeitSpanne zeitSpanne)
        {
            List<AusleihVorgang> ausleihVorgaenge = _bankService.GetAlleAusleihvorgaenge().ToList();

            decimal erhaltenesGeld = 0m;


            foreach (AusleihVorgang ausleihVorgang in ausleihVorgaenge)
            {
                IList<RueckzahlVorgang> alleRueckzahlvorgaengeByVorgangsNummer = _bankService.GetAlleRueckzahlvorgaengeByVorgangsNummer(ausleihVorgang.VorgangsNummer)
                    .Where(rueckzahlung => rueckzahlung.Datum.Date >= zeitSpanne.StartDatum.Date && rueckzahlung.Datum.Date <= zeitSpanne.EndDatum.Date).ToList();
                erhaltenesGeld += alleRueckzahlvorgaengeByVorgangsNummer.Sum(rueckzahlung => rueckzahlung.Betrag);
            }

            return erhaltenesGeld;
        }
    }
}