using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Geldverleih.Domain;
using Geldverleih.Service;
using Geldverleih.Service.interfaces;
using Geldverleih.UI.models;
using Geldverleih.UI.views;
using log4net;

namespace Geldverleih.UI.presenters
{
    public class KundeDetailPresenter
    {
        private readonly IKundenService _kundenService;
        private readonly IKundeDetailView _kundeDetailView;
        private readonly BankPresenter _bankPresenter;
        private readonly IZinssatzFactory _zinssatzFactory;

        protected static readonly ILog Log = LogManager.GetLogger(typeof(GeldEinzahlenView));

        public void FehlerLoggen(string message)
        {
            log4net.Config.XmlConfigurator.Configure();

            Log.Error(message);
        }

        public KundeDetailPresenter(IKundenService kundenService, IKundeDetailView kundeDetailView, BankPresenter bankPresenter, IZinssatzFactory zinssatzFactory)
        {
            try
            {
                _kundenService = kundenService;
                _kundeDetailView = kundeDetailView;
                _bankPresenter = bankPresenter;
                _zinssatzFactory = zinssatzFactory;
            

                _kundeDetailView.KundeDetailAnsicht = new KundenDetailModel();
                _kundeDetailView.Initialisieren(this);
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        public void KundenSpeichern()
        {
            try
            {
                _kundenService.KundenSpeichern(_kundeDetailView.KundeDetailAnsicht.Kunde);
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        public void NeuerKundeDetailView()
        {
            try
            {
                _kundeDetailView.KundeNochNichtErstelltModus();
                _kundeDetailView.ModalAnsichtLaden();
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        public IList<AusleihVorgang> GetAlleAusleihvorgaengeZumKunden()
        {
            return
                _bankPresenter.GetAlleAusleihvorgaengeByKundenNummer(
                    _kundeDetailView.KundeDetailAnsicht.Kunde.Kundennummer);
        }

        public void KundeBearbeitenDetailView(Kunde kunde)
        {
            try
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
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        public void KundeLeihtGeld(decimal betrag, VerleihKondition kondition)
        {
            try
            {
                _bankPresenter.GeldAusleihen(_kundeDetailView.KundeDetailAnsicht.Kunde, kondition, betrag);
                _kundeDetailView.AusleihUebersichtAktualisieren();
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        public IList<decimal> GetAlleZinssaetze()
        {
            return _zinssatzFactory.GetAlleZinssaetze();
        }

        public void GeldEinzahlenViewLaden(Guid vorgangsNummer)
        {
            try
            {
                GeldEinzahlenView geldEinzahlenView = new GeldEinzahlenView();

                _bankPresenter.EinzahlungsViewFestlegen(geldEinzahlenView);
                geldEinzahlenView.Initialisieren(_bankPresenter, vorgangsNummer);

                geldEinzahlenView.ShowDialog();
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }
    }
}