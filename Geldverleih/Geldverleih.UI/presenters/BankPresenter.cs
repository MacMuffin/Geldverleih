using System;
using System.Collections.Generic;
using Geldverleih.Domain;
using Geldverleih.Service.interfaces;
using Geldverleih.UI.Logik;
using Geldverleih.UI.Properties;
using Geldverleih.UI.views;

namespace Geldverleih.UI.presenters
{
    public class BankPresenter
    {
        private readonly IBankService _bankService;
        private readonly IZinsRechner _zinsRechner;
        private IEinzahlungsView _einzahlungsView;

        public BankPresenter(IBankService bankService, IZinsRechner zinsRechner)
        {
            _bankService = bankService;
            _zinsRechner = zinsRechner;
        }


        public void EinzahlungsViewFestlegen(IEinzahlungsView einzahlungsView)
        {
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
                message = string.Format(Resources.BankPresenter_GeldEingezahlt_VorgangsAbbezahlt_Message,
                                        einzahlResult.Restbetrag);
            else
            {
                if (einzahlResult.EinzahlvorgangErfolgreich)
                    message = string.Format(Resources.BankPresenter_GeldEingezahlt_VorgangBezahltUndRest_Message,
                                            einzahlResult.Restbetrag);
                else
                    switch (einzahlResult.Error)
                    {
                        case EinzahlError.VorgangBereitsBezahlt:
                            message = Resources.BankPresenter_GeldEingezahlt_VorgangIstBereitsKomplettAbbezahlt_Message;
                            break;
                        case EinzahlError.VorgangExistiertNicht:
                            message = Resources.BankPresenter_GeldEingezahlt_Dieser_Vorgang_existiert_nicht_mehr_Message;
                            break;
                    }
            }

            _einzahlungsView.EinzahlungAbgeschlossenResult(message);
        }

        public EinzahlResult EingezahltenBetragVomZuZahlendenBetragAbziehen(decimal zuZahlenderBetrag, decimal eingezahlterBetrag, Guid vorgangsNummer)
        {
            if (IstBetragAbgezahlt(zuZahlenderBetrag))
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

        private bool IstBetragAbgezahlt(decimal zuZahlenderBetrag)
        {
            return zuZahlenderBetrag == 0.0m;
        }

        public void GeldAusleihen(Kunde kunde, VerleihKondition kondition, decimal betrag)
        {
            if (kunde == null)
                throw new ArgumentNullException("kunde", Resources.BankPresenter_GeldAusleihen_Dieser_Kunde_existiert_nicht_Message);

            _bankService.GeldAusleihen(kunde.Kundennummer, kondition, betrag);
        }

        public IList<AusleihVorgang> GetAlleAusleihvorgaenge()
        {
            return _bankService.GetAlleAusleihvorgaenge();
        }

        public IList<AusleihVorgang> GetAlleAusleihvorgaengeByKundenNummer(Guid kundenNummer)
        {
            if (!IstIdGueltig(kundenNummer))
                throw new ArgumentNullException("kundenNummer",
                                                Resources.BankPresenter_GetAlleAusleihvorgaengeByKundenNummer_Ausleihvorgänge_zum_Kunden_können_nicht_ausgelesen_werden__da_die_KundenNummer_ungültig_ist);

            return _bankService.GetAlleAusleihvorgaengeByKundenNummer(kundenNummer);
        }

        public IList<RueckzahlVorgang> GetAlleEingezahltenVorgaengeZurVorgangsNummer(Guid vorgangsNummer)
        {
            if (!IstIdGueltig(vorgangsNummer))
                throw new ArgumentNullException("vorgangsNummer",
                    Resources.BankPresenter_GetAlleEingezahltenVorgaengeZurVorgangsNummer_Eingezahlten_Vorgänge_können_nicht_ausgelesen_werde__da_die_Vorgangsnummer_ungültig_ist);

            return _bankService.GetAlleRueckzahlvorgaengeByVorgangsNummer(vorgangsNummer);
        }

        private bool IstIdGueltig(Guid id)
        {
            return id != Guid.Empty;
        }
    }
}