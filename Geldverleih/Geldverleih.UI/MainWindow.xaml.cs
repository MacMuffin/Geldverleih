using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Geldverleih.Domain;
using Geldverleih.Repository;
using Geldverleih.Repository.interfaces;
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
        private IZinssatzFactory _zinssatzFactory;


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
                GeldverleihUnityContainer geldverleihUnityContainer = new GeldverleihUnityContainer();

                _kundenService = geldverleihUnityContainer.UnityContainer.Resolve<IKundenService>();
                _zinssatzFactory = geldverleihUnityContainer.UnityContainer.Resolve<IZinssatzFactory>();
                _kundenUebersichtPresenter = new KundenUebersichtPresenter(_kundenService);
                _zinsRechner = geldverleihUnityContainer.UnityContainer.Resolve<IZinsRechner>();
                _bankPresenter = new BankPresenter(geldverleihUnityContainer.UnityContainer.Resolve<IBankService>(), _zinsRechner, 
                                                   new GeldEinzahlenView());

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

            if (startDatum == null || endDatum == null)
            {
                MessageBox.Show("Kein Datum ausgewahelt!", "Bitte Datum auswählen.");
                return;
            }

            if (!IstStartdatumKleinerGleichEnddatum())
                MessageBox.Show("Das Startdatum kann nicht später beginnen wie das Enddatum.");

            ZeitSpanne heute = new ZeitSpanne { StartDatum = (DateTime) startDatum, EndDatum = (DateTime) endDatum };
            ZinsenEinnahmenTextblock.Text =
                _zinsRechner.GetEingenommeneZinsenImZeitraum(heute)
                    .ToString();

            VerliehenesGeldTextblock.Text = _zinsRechner.GetAusgezahltesGeldFuerZeitraum(heute).ToString();

            EinnahmenTextblock.Text = _zinsRechner.GetRueckgezahlteBetraegeFuerZeitraum(heute).ToString();
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
                KundeDetailPresenter kundeDetailPresenter = new KundeDetailPresenter(_kundenService, kundeDetailView, _bankPresenter, _zinssatzFactory);

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
