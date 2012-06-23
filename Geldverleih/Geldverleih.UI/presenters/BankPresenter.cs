using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Geldverleih.Domain;
using Geldverleih.Service.interfaces;
using Geldverleih.UI.Logik;
using Geldverleih.UI.views;
using log4net;

namespace Geldverleih.UI.presenters
{
    public class BankPresenter
    {
        private readonly IBankService _bankService;
        private readonly IZinsRechner _zinsRechner;
        private readonly IEinzahlungsView _einzahlungsView;

        public BankPresenter(IBankService bankService, IZinsRechner zinsRechner, IEinzahlungsView einzahlungsView)
        {
            _bankService = bankService;
            _zinsRechner = zinsRechner;
            _einzahlungsView = einzahlungsView;
        }

        public void GeldEinzahlen(Guid vorgangsNummer, decimal betrag)
        {
            AusleihVorgang ausleihVorgang = _bankService.GetAusleihvorgangByVorgangsnummer(vorgangsNummer);

            decimal zuZahlenderBetrag = _zinsRechner.BetragMitZinsenFuerZeitraumBerechnen(ausleihVorgang.Betrag,
                                                                                 ausleihVorgang.ZinsSatz,
                                                                                 ausleihVorgang.VorgangsNummer,
                                                                                 new ZeitSpanne
                                                                                     {
                                                                                         StartDatum = ausleihVorgang.Datum,
                                                                                         EndDatum = DateTime.Now
                                                                                     });

            EinzahlResult result = EingezahltenBetragVomZuZahlendenBetragAbziehen(zuZahlenderBetrag, betrag, vorgangsNummer);
            GeldEingezahlt(result);
        }

        private void GeldEingezahlt(EinzahlResult einzahlResult)
        {
            string message = "";

            if (einzahlResult.Restbetrag <= 0m && einzahlResult.EinzahlvorgangErfolgreich)
                message = string.Format("Dieser Ausleihvorgang ist komplett abbezahlt! Sie erhalten {0}€ zurück",
                                        einzahlResult.Restbetrag);
            else
            {
                if (einzahlResult.EinzahlvorgangErfolgreich)
                {
                    message = string.Format("Danke für Ihre einzahlungen. Sie haben noch {0}€ zu zahlen.",
                                        einzahlResult.Restbetrag);
                }

                else
                {
                    switch (einzahlResult.Error)
                    {
                        case EinzahlError.VorgangBereitsBezahlt:
                            message = "Dieser Vorgang ist bereits komplett gezahlt!";
                            break;
                        case EinzahlError.VorgangExistiertNicht:
                            message = "Dieser Vorgang existiert nicht mehr!";
                            break;
                    }
                }
            }

            _einzahlungsView.EinzahlungAbgeschlossenResult(message);
        }

        public EinzahlResult EingezahltenBetragVomZuZahlendenBetragAbziehen(decimal zuZahlenderBetrag, decimal eingezahlterBetrag, Guid vorgangsNummer)
        {
            if (zuZahlenderBetrag == 0.0m)
                return EinzahlResult.EinzahlvorgangFehlerhaft(EinzahlError.VorgangBereitsBezahlt);

            decimal restBetrag = zuZahlenderBetrag - eingezahlterBetrag;

            if (restBetrag < 0.0m)
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
            if (kunde == null)
                throw new ArgumentNullException("kunde", "Dieser Kunde existiert nicht.");

            _bankService.GeldAusleihen(kunde.Kundennummer, kondition, betrag);
        }

        public IList<AusleihVorgang> GetAlleAusleihvorgaenge()
        {
            return _bankService.GetAlleAusleihvorgaenge();
        }

        public IList<AusleihVorgang> GetAlleAusleihvorgaengeByKundenNummer(Guid kundenNummer)
        {
            if (kundenNummer == Guid.Empty)
                throw new ArgumentNullException("kundenNummer",
                                                "Ausleihvorgänge zum Kunden können nicht ausgelesen werden, da die KundenNummer ungültig ist.");

            return _bankService.GetAlleAusleihvorgaengeByKundenNummer(kundenNummer);
        }

        public IList<RueckzahlVorgang> GetAlleEingezahltenVorgaengeZurVorgangsNummer(Guid vorgangsNummer)
        {
            if (vorgangsNummer == Guid.Empty)
                throw new ArgumentNullException("vorgangsNummer",
                    "Eingezahlten Vorgänge können nicht ausgelesen werde, da die Vorgangsnummer ungültig ist.");

            return _bankService.GetAlleRueckzahlvorgaengeByVorgangsNummer(vorgangsNummer);
        }
    }
}