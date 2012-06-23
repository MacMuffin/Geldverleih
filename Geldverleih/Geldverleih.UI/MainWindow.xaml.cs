using System;
using System.Collections.Generic;
using System.Windows;
using Geldverleih.Domain;
using Geldverleih.Service;
using Geldverleih.Service.interfaces;
using Geldverleih.UI.Logik;
using Geldverleih.UI.presenters;
using Geldverleih.UI.views;
using Geldverleih.Unity;
using log4net;
using Microsoft.Practices.Unity;

namespace Geldverleih.UI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly KundenUebersichtPresenter _kundenUebersichtPresenter;
        private readonly IKundenService _kundenService;
        private readonly BankPresenter _bankPresenter;
        private readonly IZinssatzFactory _zinssatzFactory;


        protected static readonly ILog Log = LogManager.GetLogger(typeof(GeldEinzahlenView));
        private readonly IZinsRechner _zinsRechner;

        public void FehlerLoggen(string message)
        {
            log4net.Config.XmlConfigurator.Configure();

            Log.Error(message);
        }

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                var geldverleihUnityContainer = new GeldverleihUnityContainer();

                _kundenService = geldverleihUnityContainer.UnityContainer.Resolve<IKundenService>();
                _zinssatzFactory = geldverleihUnityContainer.UnityContainer.Resolve<IZinssatzFactory>();
                _kundenUebersichtPresenter = new KundenUebersichtPresenter(_kundenService);
                _zinsRechner = geldverleihUnityContainer.UnityContainer.Resolve<IZinsRechner>();
                _bankPresenter = new BankPresenter(geldverleihUnityContainer.UnityContainer.Resolve<IBankService>(), _zinsRechner);

                KundenUebersichtAktualisieren();
                
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        private void StatistikAktualisieren()
        {
            DateTime? startDatum = EinnahmeVonDatePicker.SelectedDate;
            DateTime? endDatum = EinnahmenBisDatePicker.SelectedDate;

            if (!IstDatumAusgewaehlt(startDatum, endDatum))
            {
                MessageBox.Show(Properties.Resources.MainWindow_StatistikAktualisieren_KeinDatumAusgewaehlt_Message, 
                    Properties.Resources.MainWindow_StatistikAktualisieren_KeinDatumAusgewaehlt_Caption);
                return;
            }

            if (!IstStartdatumKleinerGleichEnddatum())
                MessageBox.Show(Properties.Resources.MainWindow_StatistikAktualisieren_EndUndStartdatumVertauscht_Message, 
                    Properties.Resources.MainWindow_StatistikAktualisieren_EndUndStartdatumVertauscht_Caption);

            ZeitSpanne heute = new ZeitSpanne { StartDatum = (DateTime) startDatum, EndDatum = (DateTime) endDatum };
            ZinsenEinnahmenTextblock.Text =
                _zinsRechner.GetEingenommeneZinsenImZeitraum(heute)
                    .ToString();

            VerliehenesGeldTextblock.Text = _zinsRechner.GetAusgezahltesGeldFuerZeitraum(heute).ToString();

            EinnahmenTextblock.Text = _zinsRechner.GetRueckgezahlteBetraegeFuerZeitraum(heute).ToString();
        }

        private bool IstDatumAusgewaehlt(DateTime? startDatum, DateTime? endDatum)
        {
            return startDatum != null && endDatum != null;
        }

        private bool IstStartdatumKleinerGleichEnddatum()
        {
            return EinnahmeVonDatePicker.SelectedDate.Value.Date <= EinnahmenBisDatePicker.SelectedDate.Value.Date;
        }

        public void KundenUebersichtAktualisieren()
        {
            IList<Kunde> kunden = _kundenUebersichtPresenter.AlleKundenAuslesen();


            KundenDataGrid.ItemsSource = kunden;
        }

        private void AddKundeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IKundeDetailView kundeDetailView = new KundeDetailansicht();
                KundeDetailPresenter kundeDetailPresenter = new KundeDetailPresenter(_kundenService, kundeDetailView, _bankPresenter, _zinssatzFactory);

                kundeDetailPresenter.NeuerKundeDetailView();
                KundenUebersichtAktualisieren();
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        private void EditKundeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Kunde ausgewaehlterKunde = (Kunde)KundenDataGrid.SelectedItem;

                IKundeDetailView kundeDetailView = new KundeDetailansicht();
                var kundeDetailPresenter = new KundeDetailPresenter(_kundenService, kundeDetailView, _bankPresenter, _zinssatzFactory);

                kundeDetailPresenter.KundeBearbeitenDetailView(ausgewaehlterKunde);
                KundenUebersichtAktualisieren();
            }
            catch (Exception exception)
            {
                FehlerLoggen(exception.Message);
            }
        }

        private void AuswertenButton_Click(object sender, RoutedEventArgs e)
        {
            StatistikAktualisieren();
        }
    }
}
