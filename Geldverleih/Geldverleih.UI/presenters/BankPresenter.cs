﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Geldverleih.Domain;
using Geldverleih.Service.interfaces;
using Geldverleih.UI.Logik;
using log4net;

namespace Geldverleih.UI.presenters
{
    public class BankPresenter
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(BankPresenter));


        private readonly IBankService _bankService;
        private readonly IZinsRechner _zinsRechner;

        public BankPresenter(IBankService bankService, IZinsRechner zinsRechner)
        {
            _bankService = bankService;
            _zinsRechner = zinsRechner;
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

            EingezahltenBetragVomZuZahlendenBetragAbziehen(zuZahlenderBetrag, betrag, vorgangsNummer);
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

        public IList<AusleihVorgang> GetAlleAusleihvorgaengeByKundenNummer(Guid kundenNummer)
        {
            return _bankService.GetAlleAusleihvorgaengeByKundenNummer(kundenNummer);
        }

        public void TestLogEintrag()
        {
            log4net.Config.XmlConfigurator.Configure();

            Log.Warn("Test");
        }

        public IList<RueckzahlVorgang> GetAlleEingezahltenVorgaengeZurVorgangsNummer(Guid vorgangsNummer)
        {
            return _bankService.GetAlleRueckzahlvorgaengeByVorgangsNummer(vorgangsNummer);
        }
    }
}