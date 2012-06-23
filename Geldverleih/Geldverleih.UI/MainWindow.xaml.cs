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

        public MainWindow()
        {
            InitializeComponent();
            GeldverleihUnityContainer geldverleihUnityContainer = new GeldverleihUnityContainer();

            _kundenService = geldverleihUnityContainer.UnityContainer.Resolve<IKundenService>();
            _zinssatzFactory = geldverleihUnityContainer.UnityContainer.Resolve<IZinssatzFactory>();
            _kundenUebersichtPresenter = new KundenUebersichtPresenter(_kundenService);
            _bankPresenter = new BankPresenter(geldverleihUnityContainer.UnityContainer.Resolve<IBankService>(), geldverleihUnityContainer.UnityContainer.Resolve<IZinsRechner>(), 
                new GeldEinzahlenView());

            KundenUebersichtAktualisieren();
        }

        public void KundenUebersichtAktualisieren()
        {
            IList<Kunde> kunden = _kundenUebersichtPresenter.AlleKundenAuslesen();

            
            KundenDataGrid.ItemsSource = kunden;
        }

        private void AddKundeButton_Click(object sender, RoutedEventArgs e)
        {
            IKundeDetailView kundeDetailView = new KundeDetailansicht();
            KundeDetailPresenter kundeDetailPresenter = new KundeDetailPresenter(_kundenService, kundeDetailView, _bankPresenter, _zinssatzFactory);

            kundeDetailPresenter.NeuerKundeDetailView();
            KundenUebersichtAktualisieren();
        }

        private void EditKundeButton_Click(object sender, RoutedEventArgs e)
        {
            Kunde ausgewaehlterKunde = (Kunde)KundenDataGrid.SelectedItem;

            IKundeDetailView kundeDetailView = new KundeDetailansicht();
            KundeDetailPresenter kundeDetailPresenter = new KundeDetailPresenter(_kundenService, kundeDetailView, _bankPresenter, _zinssatzFactory);

            kundeDetailPresenter.KundeBearbeitenDetailView(ausgewaehlterKunde);
            KundenUebersichtAktualisieren();
        }
    }
}
