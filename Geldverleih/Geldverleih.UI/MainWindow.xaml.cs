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
using Geldverleih.UI.presenters;
using Geldverleih.Unity;
using Microsoft.Practices.Unity;

namespace Geldverleih.UI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KundenPresenter _kundenPresenter;

        public MainWindow()
        {
            InitializeComponent();
            GeldverleihUnityContainer geldverleihUnityContainer = new GeldverleihUnityContainer();

            IKundenService kundenService = geldverleihUnityContainer.UnityContainer.Resolve<IKundenService>();
            _kundenPresenter = new KundenPresenter(kundenService);

            KundenUebersichtAktualisieren();

            //IAusleihRepository ausleihRepository = new AusleihRepository();
            //IAusUndRueckzahlvorgangFactory factory = new AusUndRueckzahlvorgangFactory();
            //IRueckzahlReppository rueckzahlReppository = new RueckzahlRepository();
            //IBankService bankService = new BankService(ausleihRepository, rueckzahlReppository, kundenRepository, factory);
            //IBankService bankService = geldverleihUnityContainer.UnityContainer.Resolve<IBankService>();
            //BankPresenter bankPresenter = new BankPresenter(bankService);

            //bankPresenter.GeldAusleihen(kunden.First(), new VerleihKondition(), 12.5m);

            //IList<AusleihVorgang> ausleihVorgaenge = bankPresenter.GetAlleAusleihvorgaenge();

            //bankPresenter.GeldEinzahlen(ausleihVorgaenge.First().VorgangsNummer, 5.6m);
        }

        public void KundenUebersichtAktualisieren()
        {
            IList<Kunde> kunden = _kundenPresenter.AlleKundenAuslesen();

            
            KundenDataGrid.ItemsSource = kunden;
        }

        private void AddKundeButton_Click(object sender, RoutedEventArgs e)
        {
            KundeDetailansicht kundeDetailansicht = new KundeDetailansicht(_kundenPresenter);

            kundeDetailansicht.ShowDialog();

            KundenUebersichtAktualisieren();
        }
    }
}
