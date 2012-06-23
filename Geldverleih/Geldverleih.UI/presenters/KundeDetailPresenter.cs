using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Geldverleih.Domain;
using Geldverleih.Service;
using Geldverleih.Service.interfaces;
using Geldverleih.UI.models;
using Geldverleih.UI.views;

namespace Geldverleih.UI.presenters
{
    public class KundeDetailPresenter
    {
        private readonly IKundenService _kundenService;
        private readonly IKundeDetailView _kundeDetailView;
        private readonly BankPresenter _bankPresenter;
        private readonly IZinssatzFactory _zinssatzFactory;

        public KundeDetailPresenter(IKundenService kundenService, IKundeDetailView kundeDetailView, BankPresenter bankPresenter, IZinssatzFactory zinssatzFactory)
        {
            _kundenService = kundenService;
            _kundeDetailView = kundeDetailView;
            _bankPresenter = bankPresenter;
            _zinssatzFactory = zinssatzFactory;

            _kundeDetailView.KundeDetailAnsicht = new KundenDetailModel();
            _kundeDetailView.Initialisieren(this);
        }

        public void KundenSpeichern()
        {
            _kundenService.KundenSpeichern(_kundeDetailView.KundeDetailAnsicht.Kunde);
        }

        public void NeuerKundeDetailView()
        {
            _kundeDetailView.KundeNochNichtErstelltModus();
            _kundeDetailView.ModalAnsichtLaden();
        }

        public IList<AusleihVorgang> GetAlleAusleihvorgaengeZumKunden()
        {
            return
                _bankPresenter.GetAlleAusleihvorgaengeByKundenNummer(
                    _kundeDetailView.KundeDetailAnsicht.Kunde.Kundennummer);
        }

        public void KundeBearbeitenDetailView(Kunde kunde)
        {
            if (kunde == null)
            {
                MessageBox.Show("Es wurde kein Kunde zum bearbeiten ausgewaehlt");
                return;
            }

            _kundeDetailView.KundeDetailAnsicht.Kunde = kunde;
            _kundeDetailView.KundeBearbeitenModus();
            _kundeDetailView.ModalAnsichtLaden();
        }

        public void KundeLeihtGeld(decimal betrag, VerleihKondition kondition)
        {
            _bankPresenter.GeldAusleihen(_kundeDetailView.KundeDetailAnsicht.Kunde, kondition, betrag);
            _kundeDetailView.AusleihUebersichtAktualisieren();
        }

        public IList<decimal> GetAlleZinssaetze()
        {
            return _zinssatzFactory.GetAlleZinssaetze();
        }

        public void GeldEinzahlenViewLaden(Guid vorgangsNummer)
        {
            GeldEinzahlenView geldEinzahlenView = new GeldEinzahlenView();

            geldEinzahlenView.Initialisieren(_bankPresenter, vorgangsNummer);

            geldEinzahlenView.ShowDialog();
        }
    }
}